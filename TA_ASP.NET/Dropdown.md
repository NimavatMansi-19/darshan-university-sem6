# Department Drop-Down in Staff Table (Using Stored Procedure & Same Code Pattern as Images)

This documentation is created exactly based on the provided images.
It uses:

✔ Stored Procedure
✔ SelectListItem
✔ ViewBag.DepartmentList
✔ FillDepartmentDropDown() method
✔ Same structure as shown in screenshots

---

# Step 1: Create Stored Procedure

```sql
CREATE PROCEDURE [dbo].[PR_MOM_Department_DDL]
AS
BEGIN
    SELECT DepartmentID, DepartmentName
    FROM MOM_Department
    ORDER BY DepartmentName
END
```

✔ Used only for DropDown
✔ Returns DepartmentID & DepartmentName



# Step 2: StaffController Code

## AddEdit Action (GET)

```csharp
public IActionResult AddEdit()
{
    ViewBag.DepartmentList = FillDepartmentDropDown();
    return View("StaffAddEdit");
}
```

✔ Loads dropdown before returning view

⚠️ ERROR POINT:
If FillDepartmentDropDown() is not called → DropDown will be empty.

---

# Step 3: FillDepartmentDropDown Method

```csharp
public List<SelectListItem> FillDepartmentDropDown()
{
    List<SelectListItem> deptList = new List<SelectListItem>();

    SqlConnection sqlConnection = new SqlConnection(
        _configuration.GetConnectionString("DefaultConnection"));

    SqlCommand sqlCommand = sqlConnection.CreateCommand();
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
    sqlCommand.CommandText = "PR_MOM_Department_DDL";

    sqlConnection.Open();

    SqlDataReader reader = sqlCommand.ExecuteReader();

    while (reader.Read())
    {
        deptList.Add(new SelectListItem(
            reader["DepartmentName"].ToString(),
            reader["DepartmentID"].ToString()));
    }

    reader.Close();
    sqlConnection.Close();

    return deptList;
}
```

✔ Uses Stored Procedure
✔ Uses SqlDataReader
✔ Uses SelectListItem
✔ Returns List<SelectListItem>

---


# Step 4: StaffAddEdit.cshtml View

```csharp
@model Staff

<h2>Staff Add Edit</h2>
<hr />

<form>

    <input type="hidden" asp-for="StaffID" />

    <div class="row mb-3">
        <label asp-for="StaffName" class="form-label">Staff Name</label>
        <input asp-for="StaffName" class="form-control" placeholder="Enter Staff Name" />
    </div>

    <div class="row mb-3">
        <div class="col-md-4">
            <label asp-for="DepartmentID" class="form-label">Select Department</label>
            <select asp-for="DepartmentID"
                    class="form-control"
                    asp-items="ViewBag.DepartmentList">
                <option value="" selected disabled>-- Select Department --</option>
            </select>
        </div>
    </div>

</form>
```

✔ asp-for binds DepartmentID
✔ asp-items binds ViewBag.DepartmentList
✔ Selected DepartmentID will post back automatically

---

# How Complete Flow Works

1️⃣ Page loads AddEdit()
2️⃣ FillDepartmentDropDown() executes SP
3️⃣ Data comes using SqlDataReader
4️⃣ List<SelectListItem> created
5️⃣ Stored in ViewBag.DepartmentList
6️⃣ View binds dropdown using asp-items
7️⃣ Selected value posted to DepartmentID

---

