# Establishing SQL Connection & Displaying Department Data



### Table Structure: `MOM_Department`

| Column Name    | Data Type     | Remarks            |
| -------------- | ------------- | ------------------ |
| DepartmentID   | int           | PK, Auto Increment |
| DepartmentName | nvarchar(100) | Not Null           |
| Created        | datetime      | Default GetDate()  |
| Modified       | datetime      | Not Null           |

---

## Step 1: Create SelectAll Stored Procedure

### Why Stored Procedure?

* Keeps database logic inside SQL Server
* Improves security
* Prevents SQL Injection
* Reusable
* Commonly used in exams & real projects

### Stored Procedure Code

```sql
CREATE PROCEDURE [dbo].[PR_MOM_Department_SelectAll]
AS
BEGIN
    SELECT
        DepartmentID,
        DepartmentName,
        Created,
        Modified
    FROM MOM_Department
END
```

---

## Step 2: Insert Sample Data (Optional)

```sql
INSERT INTO MOM_Department (DepartmentName, Modified)
VALUES ('HR', GETDATE()), ('IT', GETDATE()), ('Finance', GETDATE())
```

---

## Step 3: Configure Connection String (`appsettings.json`)

### Windows Authentication

```json
"ConnectionStrings": {
  "ConnectionString": "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DB;Integrated Security=true;"
}
```

### SQL Authentication (Mac/Linux)

```json
"ConnectionStrings": {
  "ConnectionString": "Data Source=localhost;Initial Catalog=Practice;User Id=SA;Password=MyStrongPass123;"
}
```

---

## Step 4: Create Department Model

📁 **Models/DepartmentModel.cs**

```csharp
public class DepartmentModel
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
```

### Why Model?

* Represents one row of data
* Strongly typed
* Cleaner than DataTable
* Industry standard

---

## Step 5: Inject IConfiguration in Controller

```csharp
private IConfiguration configuration;

public DepartmentController(IConfiguration _configuration)
{
    configuration = _configuration;
}
```

---

## Step 6: Install Required NuGet Package

Install **System.Data.SqlClient**

* Tools → NuGet Package Manager
* Manage NuGet Packages
* Search: `System.Data.SqlClient`

---

## Step 7: Write SelectAll Logic

### Understanding `CommandType`

`CommandType` tells SQL Server **how the command should be executed**.

```csharp
command.CommandType = CommandType.StoredProcedure;
```

**Explanation (Exam-friendly):**

* `StoredProcedure` → Used when we are calling a stored procedure name
* It tells SQL Server not to treat the command as a normal SQL query
* It improves security and performance
* Most commonly used in exams and enterprise projects

Other types (just for knowledge):

* `CommandType.Text` → Used for inline SQL queries
* `CommandType.TableDirect` → Rarely used

---

### Understanding `while (reader.Read())` Loop

```csharp
while (reader.Read())
{
    // read data
}
```

**Explanation (Very Important for Viva):**

* `SqlDataReader` reads data **row by row**
* `reader.Read()` moves the cursor to the **next row**
* The loop runs until **all rows are read**
* Each loop iteration represents **one record** from the database

In simple words:

* Stored Procedure returns many rows
* `while` loop reads each row one by one
* Each row is converted into a Model object

---

## Step 7: Write SelectAll Logic

Below are **both versions** of controller code:

---

### 🔹 Version 1: Controller Code (WITHOUT using statement)

```csharp
public IActionResult Index()
{
    List<DepartmentModel> list = new List<DepartmentModel>();

    string connectionString = configuration.GetConnectionString("ConnectionString");
    SqlConnection connection = new SqlConnection(connectionString);
    connection.Open();

    SqlCommand command = connection.CreateCommand();
    command.CommandType = CommandType.StoredProcedure;
    command.CommandText = "PR_MOM_Department_SelectAll";
    SqlDataReader reader = command.ExecuteReader();

    while (reader.Read())
    {
        DepartmentModel model = new DepartmentModel();
        model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
        model.DepartmentName = reader["DepartmentName"].ToString();
        model.Created = Convert.ToDateTime(reader["Created"]);
        model.Modified = Convert.ToDateTime(reader["Modified"]);

        list.Add(model);
    }

    connection.Close();
    return View(list);
}
```

---

### 🔹 Version 2: Controller Code (WITH using statement – Best Practice)

```csharp
public IActionResult Index()
{
    List<DepartmentModel> list = new List<DepartmentModel>();

    using (SqlConnection connection = new SqlConnection(
           configuration.GetConnectionString("ConnectionString")))
    {
        using (SqlCommand command = new SqlCommand("PR_MOM_Department_SelectAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentModel model = new DepartmentModel();
                model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                model.DepartmentName = reader["DepartmentName"].ToString();
                model.Created = Convert.ToDateTime(reader["Created"]);
                model.Modified = Convert.ToDateTime(reader["Modified"]);

                list.Add(model);
            }
        }
    }

    return View(list);
}
```

---

## Step 8: Create Strongly Typed View

📁 **Views/Department/Index.cshtml**

```csharp
@model List<DepartmentModel>

<table class="table table-bordered">
    <thead>
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Created</th>
            <th>Modified</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.DepartmentID</td>
                <td>@item.DepartmentName</td>
                <td>@item.Created</td>
                <td>@item.Modified</td>
            </tr>
        }
    </tbody>
</table>
```

---

## How Data Flows

1. Controller calls Stored Procedure
2. SQL Server returns records
3. `SqlDataReader` reads row by row
4. Each row maps to `DepartmentModel`
5. All models stored in `List<DepartmentModel>`
6. List passed to View
7. Razor View displays data

---


## Memory Trick

**SP → ConnectionString → IConfiguration → SqlClient → Command → Reader → Model → List → View**

---

✅ This approach is **recommended for exams, interviews, and real projects**.
