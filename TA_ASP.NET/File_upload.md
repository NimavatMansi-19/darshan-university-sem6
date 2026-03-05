# File Upload in Staff Module (Staff Image Upload)

---

# 🧾 1️⃣ Why We Need [FromForm] Instead of [FromBody]

When uploading a file (like Staff Image), the data is sent from browser using:

```
multipart/form-data
```

This format is used specifically for:

* Uploading files
* Sending form fields + files together

---

## 🔹 What Does [FromForm] Do?

`[FromForm]` tells ASP.NET Core:

👉 "Read data from form submission (multipart/form-data)"
👉 "Bind form fields and uploaded files properly"

Example:

```csharp
[HttpPost]
public IActionResult SaveStaff([FromForm] StaffModel model)
```

Here:

* Text fields (StaffName, Email etc.)
* File field (IFormFile StaffImage)

Both are read correctly.

---

## ❌ Why Not [FromBody]?

`[FromBody]` reads data from raw request body.

It is mainly used for:

* JSON APIs
* application/json content type

Example:

```csharp
[HttpPost]
public IActionResult SaveStaff([FromBody] StaffModel model)
```

Problem:

* JSON format cannot send file binary data properly
* File will be NULL
* Upload will fail

---

# 🔎 Difference Between [FromForm] and [FromBody]

| Feature                       | FromForm            | FromBody         |
| ----------------------------- | ------------------- | ---------------- |
| Reads Form Fields             | ✔ Yes               | ❌ No             |
| Reads Files (IFormFile)       | ✔ Yes               | ❌ No             |
| Content Type                  | multipart/form-data | application/json |
| Used For                      | File Upload         | Web APIs         |
| Supports File + Text Together | ✔ Yes               | ❌ No             |

---

# 🧑‍💻 Example: Staff Image Upload Implementation

## Step 1 – Update Staff Model

```csharp
public class StaffModel
{
    public int StaffID { get; set; }
    public string StaffName { get; set; }
    public string EmailAddress { get; set; }

    public IFormFile StaffImage { get; set; }
    public string ImagePath { get; set; }
}
```

---

## Step 2 – Add Form in View (StaffAddEdit.cshtml)

⚠️ Important: enctype must be multipart/form-data

```html
<form asp-action="SaveStaff" method="post" enctype="multipart/form-data">

    <input asp-for="StaffName" class="form-control" />
    <input asp-for="EmailAddress" class="form-control" />

    <input type="file" asp-for="StaffImage" class="form-control" />

    <button type="submit" class="btn btn-primary">Save</button>

</form>
```

If `enctype` is missing → File will be NULL.

---

## Step 3 – Controller Code

```csharp
[HttpPost]
public IActionResult SaveStaff([FromForm] StaffModel model)
{
    if (model.StaffImage != null)
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.StaffImage.FileName);
        string filePath = Path.Combine(folderPath, fileName);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            model.StaffImage.CopyTo(stream);
        }

        model.ImagePath = "/images/" + fileName;
    }

    // Save model.ImagePath to database here

    return RedirectToAction("Index");
}
```

---



# 🔁 Complete Upload Workflow

Form Submit
→ multipart/form-data
→ [FromForm] binds data
→ IFormFile receives file
→ File saved to wwwroot/images
→ Path stored in database
→ Image displayed later using <img src="@Model.ImagePath" />

---

