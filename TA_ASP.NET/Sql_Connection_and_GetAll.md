# Establishing SQL Connection & Displaying Department Data in ASP.NET Core MVC

This guide explains **step by step** how to fetch and display **Department** data using a **SelectAll Stored Procedure** in **ASP.NET Core MVC**. It is written in simple language and is exam‑friendly.

---

## Prerequisite

* SQL Server installed
* ASP.NET Core MVC project created
* `MOM_Department` table available in the database

### Table Structure: `MOM_Department`

| Column Name    | Data Type     | Remarks            |
| -------------- | ------------- | ------------------ |
| DepartmentID   | int           | PK, Auto Increment |
| DepartmentName | nvarchar(100) | Not Null           |
| Created        | datetime      | Default GetDate()  |
| Modified       | datetime      | Not Null           |

---

## Step 1: Create SelectAll Stored Procedure

### Why do we need a Stored Procedure?

A **Stored Procedure (SP)** is a pre-written SQL query stored inside the database.

We use a SelectAll stored procedure because:

* It keeps database logic inside SQL Server
* Improves security (no direct table access)
* Makes code reusable
* Reduces SQL injection risk
* Very commonly used in **college & university projects**

### Stored Procedure Code

```sql
CREATE PROCEDURE [dbo].[PR_MOM_Department_SelectAll]
AS
BEGIN
    SELECT
        [dbo].[MOM_Department].[DepartmentID],
        [dbo].[MOM_Department].[DepartmentName],
        [dbo].[MOM_Department].[Created],
        [dbo].[MOM_Department].[Modified]
    FROM
        [dbo].[MOM_Department]
END
```

This procedure returns **all rows** from the `MOM_Department` table.

---

## Step 2: Add Static Data (Optional but Recommended)

### Why do we add static data?

Static (sample) data is added so that:

* We can verify database connectivity
* We can see output on UI
* It avoids empty tables during demo or exam

Example insert query:

```sql
INSERT INTO MOM_Department (DepartmentName, Modified)
VALUES ('HR', GETDATE()), ('IT', GETDATE()), ('Finance', GETDATE())
```

---

## Step 3: Configure Connection String in `appsettings.json`

### What is a Connection String?

A **Connection String** tells ASP.NET Core:

* Where the SQL Server is located
* Which database to use
* How to authenticate the user

Without a connection string, the application **cannot connect** to the database.

### Why `appsettings.json`?

* Central place for configuration
* Easy to change without touching code
* Supports different environments (Development, Production)

### For Windows Users

```json
"ConnectionStrings": {
  "ConnectionString": "Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=true;"
}
```

### Example

```json
"ConnectionStrings": {
  "ConnectionString": "Data Source=LAPTOP-LBMAFD6U\SQLEXPRESS;Initial Catalog=StudentMaster;Integrated Security=true;"
}
```

### For Mac Users

```json
"ConnectionStrings": {
  "ConnectionString": "Data Source=localhost;Initial Catalog=Practice;User id=SA; password=MyStrongPass123;"
}
```

---

### For Mac Users

```json
"ConnectionStrings": {
  "ConnectionString": "Data Source=localhost;Initial Catalog=Practice;User id=SA; password=MyStrongPass123;"
}
```

---

## Step 4: Configure Controller to Read Connection String

### What is `IConfiguration`?

`IConfiguration` is used to:

* Read values from `appsettings.json`
* Access connection strings securely

### Why Constructor Injection?

ASP.NET Core follows **Dependency Injection**.
Using constructor injection:

* Makes code clean
* Improves testability
* Is the recommended approach

### Controller Code

```csharp
private IConfiguration configuration;

public DepartmentController(IConfiguration _configuration)
{
    configuration = _configuration;
}
```

Now the controller can read database settings.

---

## Step 5: Install Required NuGet Package

### Why do we need `System.Data.SqlClient`?

ASP.NET Core **cannot directly talk to SQL Server**.

This library provides:

* `SqlConnection` → connects to database
* `SqlCommand` → executes stored procedures
* `SqlDataReader` → reads data

### How to Install

1. Go to **Tools → NuGet Package Manager**
2. Click **Manage NuGet Packages for Solution**
3. Search: `System.Data.SqlClient`
4. Install the package

⚠️ Project should NOT be running during installation.

---

## Step 6: Write Logic for SelectAll Action Method

### What happens in this step?

In this step, ASP.NET Core communicates with SQL Server using **ADO.NET**.

Here we:

* Read connection string
* Open SQL connection
* Execute database command
* Fetch data
* Send data to View

---

### Types of `CommandType` in ADO.NET

`CommandType` tells SQL Server **how to execute the command**.

There are **3 possible types**:

#### 1️⃣ CommandType.StoredProcedure (Most Used)

```csharp
command.CommandType = CommandType.StoredProcedure;
```

* Used when calling **stored procedures**
* Secure and reusable
* Recommended for **enterprise & exam projects**

Example:

```sql
PR_MOM_Department_SelectAll
```

---

#### 2️⃣ CommandType.Text

```csharp
command.CommandType = CommandType.Text;
command.CommandText = "SELECT * FROM MOM_Department";
```

* Used for **inline SQL queries**
* Not recommended for large projects
* Higher risk of SQL Injection if not handled properly

---

#### 3️⃣ CommandType.TableDirect (Rarely Used)

* Directly accesses table
* Mostly unsupported in SQL Server
* **Not recommended**

---

### Action Method Code (Stored Procedure Way)

```csharp
public IActionResult Index()
{
    string connectionString = this.configuration.GetConnectionString("ConnectionString");
    SqlConnection connection = new SqlConnection(connectionString);
    connection.Open();

    SqlCommand command = connection.CreateCommand();
    command.CommandType = CommandType.StoredProcedure;
    command.CommandText = "PR_MOM_Department_SelectAll";

    SqlDataReader reader = command.ExecuteReader();
    DataTable table = new DataTable();
    table.Load(reader);

    return View(table);
}
```

---

### Alternative Way: Using `using` Statement (Best Practice)

```csharp
public IActionResult Index()
{
    DataTable table = new DataTable();

    using (SqlConnection connection = new SqlConnection(
           configuration.GetConnectionString("ConnectionString")))
    {
        using (SqlCommand command = new SqlCommand("PR_MOM_Department_SelectAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
        }
    }

    return View(table);
}
```

✔ Automatically closes connection
✔ Prevents memory leaks
✔ Recommended for real projects

---


## Step 7: Create View to Display Department Data

### Why Razor View?

Razor View:

* Displays server-side data
* Uses C# + HTML
* Common in MVC applications

### View Location

```
Views/Department/Index.cshtml
```

### View Code

```csharp
@model System.Data.DataTable
@using System.Data

<table class="table table-bordered">
    <thead>
        <tr>
            <th>Department ID</th>
            <th>Department Name</th>
            <th>Created</th>
            <th>Modified</th>
        </tr>
    </thead>
    <tbody>
        @foreach (DataRow row in Model.Rows)
        {
            <tr>
                <td>@row["DepartmentID"]</td>
                <td>@row["DepartmentName"]</td>
                <td>@row["Created"]</td>
                <td>@row["Modified"]</td>
            </tr>
        }
    </tbody>
</table>
```

### How data reaches the View?

1. Controller executes stored procedure
2. SQL Server returns rows
3. Rows are stored inside `DataTable`
4. `DataTable` is passed to View as `Model`
5. Razor View reads and displays data

---

## Understanding `foreach` Loop and `DataRow`

This part is **very important for exams and viva**.

---

### What is `DataTable`?

* `DataTable` is an **in-memory table**
* It stores data in **rows and columns**, just like a database table
* It belongs to the `System.Data` namespace

Example:

* Columns → DepartmentID, DepartmentName, Created, Modified
* Rows → Actual department records

---

### What is `DataRow`?

* `DataRow` represents **ONE single row** of data in a `DataTable`
* Each `DataRow` contains column values

Think of it like this:

* `DataTable` → Entire table
* `DataRow` → One record (one department)

---

### Why do we use `foreach` loop?

* Because there are **multiple rows** in the DataTable
* `foreach` helps us iterate (loop) through **each record one by one**
* It is simple, readable, and commonly used in Razor views

---

### foreach Loop Syntax Used in View

```csharp
@foreach (DataRow row in Model.Rows)
{
    <tr>
        <td>@row["DepartmentID"]</td>
        <td>@row["DepartmentName"]</td>
        <td>@row["Created"]</td>
        <td>@row["Modified"]</td>
    </tr>
}
```

---



* `Model` → Refers to `DataTable` sent from Controller
* `Model.Rows` → Collection of all rows in DataTable
* `DataRow row` → Represents one department record
* `row["ColumnName"]` → Fetches value of that column


## Final Output

When you browser:

```
/Department/Index
```

All department records appear in tabular format.

---

## Complete Flow (Exam Answer)

1. Create stored procedure in SQL Server
2. Configure connection string
3. Inject `IConfiguration`
4. Install SqlClient library
5. Execute stored procedure using ADO.NET
6. Store result in DataTable
7. Display data using Razor View

---
## Way to Remeber

| Letter | Stands For        | What to Remember           |
| ------ | ----------------- | -------------------------- |
| **SP** | Stored Procedure  | Create SelectAll SP        |
| **C**  | Connection String | Add in `appsettings.json`  |
| **C**  | Configuration     | Use `IConfiguration`       |
| **D**  | Dependency        | Install `SqlClient`        |
| **C**  | Command           | Execute SP                 |
| **R**  | Reader            | Read data into `DataTable` |
| **V**  | View              | Display using Razor        |

SP → appsettings → IConfiguration → SqlClient
→ CommandType.SP → SqlDataReader
→ DataTable → foreach → DataRow

