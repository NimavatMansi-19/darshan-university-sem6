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

## 🔎 Why Helper Method is Important?

* Prevents duplicate code
* GET → calls with NULL (all records)
* POST → calls with search value (filtered records)
* Keeps controller clean and maintainable

---


Form Submit → IFormCollection → searchText → Helper Method → Stored Procedure → View

---

**End of Document ✅**
