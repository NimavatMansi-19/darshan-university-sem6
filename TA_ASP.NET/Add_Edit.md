# Insert & Update Department Using Single Save Method 

---

# ✅ Given Table Structure

```sql
CREATE TABLE MOM_Department (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);
```

---

# 🎯 Objective

We have:

* Two Stored Procedures (Insert + Update)
* Only ONE controller method:

```csharp
[HttpPost]
public IActionResult Save(DepartmentModel model)
```

Controller decides which SP to call.

---

# Step 1: Insert Stored Procedure

```sql
CREATE PROCEDURE [dbo].[PR_MOM_Department_Insert]
    @DepartmentName NVARCHAR(100)
AS
BEGIN
    INSERT INTO MOM_Department (DepartmentName, Modified)
    VALUES (@DepartmentName, GETDATE())
END
```

⚠️ **Error Point:** If you do not pass Modified column, it will throw error because Modified is NOT NULL.

---

# Step 2: Update Stored Procedure

```sql
CREATE PROCEDURE [dbo].[PR_MOM_Department_Update]
    @DepartmentID INT,
    @DepartmentName NVARCHAR(100)
AS
BEGIN
    UPDATE MOM_Department
    SET DepartmentName = @DepartmentName,
        Modified = GETDATE()
    WHERE DepartmentID = @DepartmentID
END
```

⚠️ **Error Point:** If DepartmentID does not exist, no row will be updated.

---

# Step 3: Department Model

```csharp
public class DepartmentModel
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
}
```

⚠️ **Error Point:** If DepartmentName is NULL, database will throw error because it is NOT NULL.

---

# Step 4: Inject IConfiguration

```csharp
private IConfiguration configuration;

public DepartmentController(IConfiguration _configuration)
{
    configuration = _configuration;
}
```

⚠️ **Error Point:** If connection string name is wrong in appsettings.json, connection will fail.

---

# Step 5: AddEdit (GET Method)

```csharp
public IActionResult AddEdit(int id = 0)
{
    DepartmentModel model = new DepartmentModel();

    if (id > 0)
    {
        string connectionString = configuration.GetConnectionString("ConnectionString");
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand("PR_MOM_Department_SelectByID", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@DepartmentID", id);

        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
            model.DepartmentName = reader["DepartmentName"].ToString();
        }

        connection.Close();
    }

    return View(model);
}
```

⚠️ **Error Points:**

* If connection.Close() is forgotten → connection leak
* If SelectByID SP not created → runtime error
* If reader column name spelling is wrong → exception

---

# Step 6: Single Save Method (POST) – Without using

```csharp
[HttpPost]
public IActionResult Save(DepartmentModel model)
{
    if (ModelState.IsValid)
    {
        string connectionString = configuration.GetConnectionString("ConnectionString");
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;

        if (model.DepartmentID == 0)
        {
            // INSERT
            command.CommandText = "PR_MOM_Department_Insert";
            command.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
        }
        else
        {
            // UPDATE
            command.CommandText = "PR_MOM_Department_Update";
            command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
            command.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
        }

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();

        return RedirectToAction("Index");
    }

    return View("AddEdit", model);
}
```

---

# 🚨 Important Error-Causing Points (Very Important for Students)

### 1️⃣ If connection.Open() is not called

→ ExecuteNonQuery() will throw error.

### 2️⃣ If connection.Close() is not called

→ Database connection remains open (Performance issue).

### 3️⃣ If parameter name mismatches SP

Example: `@DeptName` instead of `@DepartmentName`
→ SQL error will occur.

### 4️⃣ If DepartmentID hidden field removed from form

→ Update will behave like Insert.

### 5️⃣ If ModelState.IsValid not checked

→ Invalid or empty data may go to database.

### 6️⃣ If Modified column not handled in SP

→ Insert will fail because column is NOT NULL.

---

# 🧠 How It Works

Form → Save()
IF (DepartmentID == 0) → Call Insert SP
ELSE → Call Update SP
ExecuteNonQuery()
Redirect to Index

---


