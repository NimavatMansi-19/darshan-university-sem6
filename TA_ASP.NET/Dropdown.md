# Implementing Department Drop-Down in ASP.NET Core MVC 

**Prerequisite:** Ensure the Drop-Down Stored Procedure is created in your database.

---

# Step 1: Create Drop-Down Stored Procedure

Below is the SQL stored procedure to fetch Department data for the dropdown:

```sql
CREATE PROCEDURE [dbo].[PR_MOM_Department_SelectForDropDown]
AS
BEGIN
    SELECT
        DepartmentID,
        DepartmentName
    FROM MOM_Department
    ORDER BY DepartmentName
END
```

✔ Only required columns selected
✔ Ordered alphabetically
✔ Used only for dropdown purpose

---

# Step 2: Create Drop-Down Model

Inside `DepartmentModel.cs` create a separate dropdown model.

```csharp
public class DepartmentDropDownModel
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
}
```

✔ Keeps dropdown clean
✔ Industry best practice
✔ No DataTable used

---

# Step 3: Update Staff Model

```csharp
public class StaffModel
{
    public int StaffID { get; set; }
    public int DepartmentID { get; set; }
    public string StaffName { get; set; }
    public string MobileNo { get; set; }
    public string EmailAddress { get; set; }
    public string Remarks { get; set; }

    // Dropdown List
    public List<DepartmentDropDownModel> DepartmentList { get; set; }
}
```

✔ DepartmentList will hold dropdown data.

---

# Step 4: Create Method to Load Department Drop-Down

In `StaffController` create a new method:

```csharp
public List<DepartmentDropDownModel> DepartmentDropDown()
{
    string connectionString = this._configuration.GetConnectionString("ConnectionString");
    SqlConnection connection = new SqlConnection(connectionString);
    connection.Open();

    SqlCommand command = connection.CreateCommand();
    command.CommandType = System.Data.CommandType.StoredProcedure;
    command.CommandText = "PR_MOM_Department_SelectForDropDown";

    SqlDataReader reader = command.ExecuteReader();

    List<DepartmentDropDownModel> departmentList = new List<DepartmentDropDownModel>();

    while (reader.Read())
    {
        DepartmentDropDownModel model = new DepartmentDropDownModel();
        model.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
        model.DepartmentName = reader["DepartmentName"].ToString();

        departmentList.Add(model);
    }

    connection.Close();
    return departmentList;
}
```

⚠️ Do NOT forget `connection.Close()`
⚠️ Column name spelling must match database

---

# Step 5: Call Drop-Down Method in AddEdit (GET)

```csharp
public IActionResult StaffForm()
{
    StaffModel model = new StaffModel();
    model.DepartmentList = DepartmentDropDown();
    return View(model);
}
```

---

# Step 6: Call Drop-Down When ModelState Fails (POST)

```csharp
public IActionResult StaffAddEdit(StaffModel model)
{
    if (ModelState.IsValid)
    {
        return RedirectToAction("StaffList");
    }

    // IMPORTANT: Reload dropdown
    model.DepartmentList = DepartmentDropDown();
    return View("StaffForm", model);
}
```

⚠️ If dropdown not reloaded → It becomes empty after validation error.

---

# Step 7: Add Drop-Down in StaffForm.cshtml

```csharp
@model StaffModel

<select class="form-control"
        asp-for="DepartmentID"
        asp-items="@(new SelectList(Model.DepartmentList, "DepartmentID", "DepartmentName"))">
    <option value="">Select Department</option>
</select>
```

✔ asp-for binds selected value
✔ SelectList binds text & value
✔ Strongly typed (No ViewBag used)

---

# Alternative Method (Using foreach)

```csharp
<select class="form-control" asp-for="DepartmentID">
    <option value="">Select Department</option>
    @foreach (var dept in Model.DepartmentList)
    {
        <option value="@dept.DepartmentID">@dept.DepartmentName</option>
    }
</select>
```

---

# How Drop-Down Works

1️⃣ Controller calls Stored Procedure
2️⃣ SqlDataReader reads rows
3️⃣ Data stored in List<DepartmentDropDownModel>
4️⃣ List assigned to Model.DepartmentList
5️⃣ View binds using SelectList
6️⃣ Selected DepartmentID posted back to controller

---



