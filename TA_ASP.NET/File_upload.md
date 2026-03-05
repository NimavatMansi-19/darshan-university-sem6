# File Upload in Staff Module (Staff Image Upload)

A complete explanation of **how image upload works in ASP.NET Core MVC** using `IFormFile`, `[FromForm]`, and `multipart/form-data`.

---

# 1️⃣ Understanding `[FromForm]`

`[FromForm]` is an attribute used in ASP.NET Core to **bind data coming from an HTML form submission**.

When a form contains **text fields and files**, the browser sends data using:

```
multipart/form-data
```

This format allows sending:

* Text fields
* Dropdown values
* Uploaded files

`[FromForm]` tells ASP.NET Core:

> Read values from form fields and uploaded files and bind them to the model.

### Example

```csharp
[HttpPost]
public IActionResult SaveStaff([FromForm] StaffModel model)
{
    return View();
}
```


---

# 2️⃣ Understanding `[FromBody]`

`[FromBody]` is used when data is sent in the **HTTP request body**, usually in **JSON format**.

It is commonly used in:

* REST APIs
* Mobile applications
* Angular / React API requests

### Example

```csharp
[HttpPost]
public IActionResult SaveStaff([FromBody] StaffModel model)
{
    return Ok();
}
```

Example request:

```json
{
  "StaffName": "John",
  "EmailAddress": "john@gmail.com"
}
```

Here ASP.NET Core converts JSON data into a **C# object**.

---

# 🌍 Real Life Analogy

Think of **sending data like a courier package**.

### `[FromBody]`

Sending a **letter** inside an envelope.

```
Envelope = JSON
Content = Text only
```

No physical items can be included.

---

### `[FromForm]`

Sending a **parcel box**.

Inside the parcel you can include:

* Documents
* Photos
* Products

```
Parcel = multipart/form-data
Items = Text + Files
```

ASP.NET Core opens the parcel and reads everything correctly.

---

# 3️⃣ Difference Between `[FromForm]` and `[FromBody]`

| Feature                       | FromForm            | FromBody         |
| ----------------------------- | ------------------- | ---------------- |
| Reads Form Fields             | ✔ Yes               | ❌ No             |
| Reads Files (IFormFile)       | ✔ Yes               | ❌ No             |
| Content Type                  | multipart/form-data | application/json |
| Used For                      | File Upload         | Web APIs         |
| Supports File + Text Together | ✔ Yes               | ❌ No             |

---

# 4️⃣ Staff Image Upload Implementation

## Step 1 – Staff Model

```csharp
public class StaffModel
{
    public int StaffID { get; set; }

    public string StaffName { get; set; }

    public string EmailAddress { get; set; }

    public IFormFile StaffImage { get; set; }
}
```



# 5️⃣ View Code (StaffAddEdit.cshtml)

⚠ **Important:** `enctype` must be `multipart/form-data`

```html
<form asp-action="Save" method="post" enctype="multipart/form-data">

<input asp-for="StaffName" class="form-control" />

<input asp-for="EmailAddress" class="form-control" />

<input type="file" asp-for="StaffImage" class="form-control" />

<button type="submit" class="btn btn-primary">Save</button>

</form>
```

### Why enctype is required

Without it:

```
StaffImage = NULL
```

Browser will not send the file.

---

# 6️⃣ Controller Code

```csharp
[HttpPost]
 public IActionResult Save([FromForm] StaffModel model)
 {
     ViewBag.DepartmentList = FillDepartmentDropDown();

    ModelState.Remove("StaffImage");

    if (model.StaffImage != null)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.StaffImage.FileName);
        FileStream stream = new FileStream(path, FileMode.Create);
        model.StaffImage.CopyTo(stream);
        stream.Close();
    }
     if (ModelState.IsValid)
     {
         using (SqlConnection connection =
             new SqlConnection(configuration.GetConnectionString("ConnectionString")))
         {
             using (SqlCommand command = new SqlCommand())
             {
                 command.Connection = connection;
                 command.CommandType = CommandType.StoredProcedure;

                 if (model.StaffID == 0)
                 {
                     command.CommandText = "PR_MOM_Staff_Insert";
                 }
                 else
                 {
                     command.CommandText = "PR_MOM_Staff_Update";
                     command.Parameters.AddWithValue("@StaffID", model.StaffID);
                 }

                 command.Parameters.AddWithValue("@StaffName", model.StaffName);
                 command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                 command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                 command.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                 command.Parameters.AddWithValue("@Remarks", model.Remarks);

                 connection.Open();
                 command.ExecuteNonQuery();
             }
         }

         return RedirectToAction("StaffList");
     }

     return View("StaffAddEdit", model);
 }
```

---

# 7️⃣ Upload Workflow

```
User selects image
        ↓
Form submitted
        ↓
Request type = multipart/form-data
        ↓
[FromForm] binds model
        ↓
IFormFile receives uploaded file
        ↓
Controller saves file in wwwroot/uploads
        ↓
Image path stored in database
        ↓
Image displayed in application
```

---

