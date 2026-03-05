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

ASP.NET Core will bind:

| Form Field   | Model Property     |
| ------------ | ------------------ |
| StaffName    | model.StaffName    |
| EmailAddress | model.EmailAddress |
| StaffImage   | model.StaffImage   |

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

# 3️⃣ Why `[FromBody]` Cannot Upload Files

`[FromBody]` expects:

```
application/json
```

JSON is **text-based**, so it cannot properly transfer:

* Images
* Videos
* PDFs

If you try uploading a file using `[FromBody]`:

```
StaffImage = null
```

The upload will fail.

---

# 4️⃣ Why `[FromForm]` is Required for File Upload

File uploads require:

```
multipart/form-data
```

This request format supports:

✔ text fields
✔ binary files
✔ multiple values

Example:

```csharp
[HttpPost]
public IActionResult Save([FromForm] StaffModel model)
```

Now ASP.NET Core correctly reads:

* `model.StaffName`
* `model.EmailAddress`
* `model.StaffImage`

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

# 5️⃣ Difference Between `[FromForm]` and `[FromBody]`

| Feature                       | FromForm            | FromBody         |
| ----------------------------- | ------------------- | ---------------- |
| Reads Form Fields             | ✔ Yes               | ❌ No             |
| Reads Files (IFormFile)       | ✔ Yes               | ❌ No             |
| Content Type                  | multipart/form-data | application/json |
| Used For                      | File Upload         | Web APIs         |
| Supports File + Text Together | ✔ Yes               | ❌ No             |

---

# 6️⃣ Staff Image Upload Implementation

## Step 1 – Staff Model

```csharp
public class StaffModel
{
    public int StaffID { get; set; }

    public string StaffName { get; set; }

    public string EmailAddress { get; set; }

    public IFormFile StaffImage { get; set; }

    [NotMapped]
    public string? ImagePath { get; set; }
}
```



# 7️⃣ View Code (StaffAddEdit.cshtml)

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

# 8️⃣ Controller Code

```csharp
[HttpPost]
public IActionResult Save([FromForm] StaffModel model)
{
    if (model.StaffImage != null && model.StaffImage.Length > 0)
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fileName = Path.GetFileName(model.StaffImage.FileName);

        string filePath = Path.Combine(folderPath, fileName);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            model.StaffImage.CopyTo(stream);
        }
    }

    return RedirectToAction("StaffList");
}
```

---

# 9️⃣ Upload Workflow

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

