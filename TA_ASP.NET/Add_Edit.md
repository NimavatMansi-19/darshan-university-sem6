# Insert & Update Department Using Single Save Method (Based on Given Table)

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

🔹 Created column automatically uses DEFAULT GETDATE()
🔹 Modified must be supplied (NOT NULL)

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

---

# Step 3: Department Model

```csharp
public class DepartmentModel
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
}
```

✔ Created and Modified handled by SQL Server
✔ No need to include them in form

---

# Step 4: Inject IConfiguration

```csharp
private IConfiguration configuration;

public DepartmentController(IConfiguration _configuration)
{
    configuration = _configuration;
}
```

---

# Step 5: AddEdit (GET Method)

```csharp
public IActionResult AddEdit(int id = 0)
{
    DepartmentModel model = new DepartmentModel();

    if (id > 0)
    {
        using (SqlConnection connection = new SqlConnection(
               configuration.GetConnectionString("ConnectionString")))
        {
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
        }
    }

    return View(model);
}
```

---

# Step 6: Single Save Method (POST)

```csharp
[HttpPost]
public IActionResult Save(DepartmentModel model)
{
    if (ModelState.IsValid)
    {
        using (SqlConnection connection = new SqlConnection(
               configuration.GetConnectionString("ConnectionString")))
        {
            using (SqlCommand command = new SqlCommand())
            {
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
            }
        }

        return RedirectToAction("Index");
    }

    return View("AddEdit", model);
}
```

---

# Step 7: AddEdit View

```csharp
@model DepartmentModel

<form asp-action="Save" method="post">

    <input type="hidden" asp-for="DepartmentID" />

    <div>
        <label>Department Name</label>
        <input asp-for="DepartmentName" />
    </div>

    <button type="submit">Save</button>

</form>
```

---

# 🔍 How It Works (Explain to Students)

### Case 1: Insert

* DepartmentID = 0
* Insert SP called
* Created auto-filled
* Modified set using GETDATE()

### Case 2: Update

* DepartmentID > 0
* Update SP called
* Modified updated
* Created remains unchanged

---

# 🧠 Important Table Concepts

✔ IDENTITY(1,1) → Auto Increment
✔ Created has DEFAULT GETDATE()
✔ Modified must always have value
✔ Primary Key decides Insert or Update

---

# 📌 Memory Flow

Form → Save() → IF(ID==0) → Insert SP
Else → Update SP → Modified = GETDATE() → Done

---

