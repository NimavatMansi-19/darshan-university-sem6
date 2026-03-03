# Search in List Page (Using IFormCollection)

---

# 🧾 1️⃣ What is IFormCollection?

`IFormCollection` is used to read form values directly from the request when a form is submitted.

When user submits a form like this:

```html
<input type="text" name="SearchText" />
```

The value can be accessed inside controller using:

```csharp
public IActionResult Index(IFormCollection formData)
{
    string searchText = formData["SearchText"].ToString();
}
```

## ✅ Why Use IFormCollection Here?

* Lab instruction says: **Do NOT create separate search model**
* So we directly read form field by name
* Keeps implementation simple
* No extra class required

---

# 🔎 2️⃣ Search Implementation Code

## 🔹 Modified Stored Procedure

```sql
ALTER PROCEDURE [dbo].[sp_GetAllDepartments]
    @SearchText VARCHAR(255) = NULL
AS
BEGIN
    SELECT DepartmentId, DepartmentName
    FROM Department
    WHERE (@SearchText IS NULL 
           OR DepartmentName LIKE '%' + @SearchText + '%')
END
```

✔ If @SearchText is NULL → returns all records
✔ If @SearchText has value → filters using LIKE

---

## 🔹 Controller Code

### GET – Load All Records

```csharp
[HttpGet]
public IActionResult Index()
{
    List<Department> list = GetDepartments(null);
    return View(list);
}
```

### POST – Search Using IFormCollection

```csharp
[HttpPost]
public IActionResult Index(IFormCollection formData)
{
    string searchText = formData["SearchText"].ToString();

    if (string.IsNullOrWhiteSpace(searchText))
        searchText = null;

    ViewBag.SearchText = searchText;

    List<Department> list = GetDepartments(searchText);
    return View(list);
}
```

✔ No new model created
✔ SearchText read directly from form
✔ ViewBag used only to retain textbox value

---

# 🔁 3️⃣ Helper Method (Common Method)

Helper method is created to avoid writing database code twice.
Both GET and POST call the same method.

```csharp
private List<Department> GetDepartments(string searchText)
{
    List<Department> list = new List<Department>();

    SqlConnection con = new SqlConnection(
        "Your_Connection_String_Here");

    SqlCommand cmd = new SqlCommand();
    cmd.Connection = con;
    cmd.CommandText = "sp_GetAllDepartments";
    cmd.CommandType = CommandType.StoredProcedure;

    if (searchText != null)
        cmd.Parameters.AddWithValue("@SearchText", searchText);
    else
        cmd.Parameters.AddWithValue("@SearchText", DBNull.Value);

    con.Open();

    SqlDataReader reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        Department d = new Department();
        d.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
        d.DepartmentName = reader["DepartmentName"].ToString();
        list.Add(d);
    }

    reader.Close();
    con.Close();

    return list;
}
```
# 4.View
```
@model IEnumerable<P_Mom.Models.DepartmentModel>

@{
    ViewData["Title"] = "Department List";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

@* Display Messages *@
<span class="text-danger">@TempData["ErrorMessage"]</span>
<span class="text-success">@TempData["SuccessMessage"]</span>

<!-- Custom CSS -->
<link rel="stylesheet" href="~/css/department-list.css" />

<div class="container">

    <!-- Page Header -->
    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <h4 class="main-title">Departments</h4>
            <p class="sub-title">Manage organization departments</p>
        </div>
        <a asp-controller="Department"
               asp-action="DepartmentAddEdit"
               class="btn btn-primary-custom">
            <i class="bi bi-plus-circle"></i> Add Department
        </a>
    </div>

    <!-- Card -->
    <div class="card custom-card">

        <div class="card-header-custom">
            <i class="bi bi-diagram-3-fill"></i>
            Department List
        </div>

        <!-- 🔍 SEARCH FORM -->
        <div class="card-body pb-0">
            <form asp-action="DepartmentList" method="post" class="mb-3">
                <div class="row g-2 align-items-center">

                    <div class="col-md-4">
                        <input type="text"
                               name="SearchText"
                               value="@ViewBag.SearchText"
                               class="form-control"
                               placeholder="Search Department Name..." />
                    </div>

                    <div class="col-auto">
                        <button type="submit" class="btn btn-primary">
                            <i class="bi bi-search"></i> Search
                        </button>

                        <a asp-action="DepartmentList"
                               class="btn btn-secondary">
                            Reset
                        </a>
                    </div>

                </div>
            </form>
        </div>

        <div class="table-responsive">
            <table class="table table-hover align-middle mb-0">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Department Name</th>
                        <th>Created Date</th>
                        <th>Last Modified</th>
                        <th class="text-end">Actions</th>
                    </tr>
                </thead>

                <tbody>

                    @if (Model != null && Model.Any())
                    {
                        foreach (var item in Model)
                        {
                            <tr>
                                <td>@item.DepartmentID</td>
                                <td>@item.DepartmentName</td>
                                <td>@(item.Created?.ToString("yyyy-MM-dd") ?? "")</td>
                                <td>@(item.Modified?.ToString("yyyy-MM-dd") ?? "")</td>

                                <td class="text-end">
                                    <div class="action-btn-group">

                                        <!-- View -->
                                        <a href="#" class="action-btn view">
                                            <i class="bi bi-eye"></i>
                                        </a>

                                        <!-- Edit -->
                                        <a asp-controller="Department"
                                               asp-action="DepartmentAddEdit"
                                               asp-route-DepartmentID="@item.DepartmentID"
                                               class="action-btn edit">
                                            <i class="bi bi-pencil"></i>
                                        </a>

                                        <!-- Delete -->
                                        <a asp-controller="Department"
                                               asp-action="DepartmentDelete"
                                               asp-route-DepartmentID="@item.DepartmentID"
                                               class="action-btn delete"
                                               onclick="return confirmDelete();">
                                            <i class="bi bi-trash"></i>
                                        </a>

                                    </div>
                                </td>
                            </tr>
                        }
                    }
                    else
                    {
                        <tr>
                            <td colspan="5" class="text-center p-4">
                                No Departments Found
                            </td>
                        </tr>
                    }

                </tbody>
            </table>
        </div>

    </div>

</div>

<script>
    function confirmDelete() {
        return confirm("Are you sure you want to delete this department?");
    }
</script>
```
## 🔎 Why Helper Method is Important?

* Prevents duplicate code
* GET → calls with NULL (all records)
* POST → calls with search value (filtered records)
* Keeps controller clean and maintainable

---


Form Submit → IFormCollection → searchText → Helper Method → Stored Procedure → View

---

