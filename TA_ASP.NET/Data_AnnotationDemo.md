# Data Annotations Demo – ASP.NET Core MVC

This demo explains **Data Annotations** using a simple **Employee Registration** example, exactly aligned with your PPT and exam syllabus.

---

## 1. Demo Scenario (Real-Life Use Case)

We are creating an **Employee Registration Form** where:

* First Name is required
* Last Name is required
* Mobile number must be 10 digits
* Email must be in valid format
* Validation messages should appear automatically

This demo shows:

* Model with Data Annotations
* Controller with server-side validation
* View with client-side validation

---

## 2. Step 1: Create Model (EmployeeModel.cs)

```csharp
using System.ComponentModel.DataAnnotations;

public class EmployeeModel
{
    [Required(ErrorMessage = "First Name is Required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is Required")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Enter Mobile Number")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number must be 10 digits")]
    public string MobileNo { get; set; }

    [Required(ErrorMessage = "Enter Valid Email")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
}
```

### 🔍 Explanation

* `[Required]` → field cannot be empty
* `[StringLength]` → restricts mobile number length
* `[EmailAddress]` → validates email format
* `[Display]` → changes label name in UI

---

## 3. Step 2: Create Controller (EmployeeController.cs)

```csharp
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EmployeeModel model)
    {
        if (ModelState.IsValid)
        {
            // Data is valid – save to database (demo purpose)
            ViewBag.Message = "Employee Registered Successfully";
            return View("Success");
        }
        return View(model);
    }
}
```

### 🔍 Explanation

* `ModelState.IsValid` checks all Data Annotations
* If validation fails, errors are returned to the view
* Server-side validation cannot be bypassed

---

## 4. Step 3: Create View (Create.cshtml)

```html
@model EmployeeModel

<h3>Employee Registration</h3>

<form asp-action="Create" method="post">

    <div class="form-group">
        <label>First Name</label>
        <input asp-for="FirstName" class="form-control" />
        <span asp-validation-for="FirstName" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label>Last Name</label>
        <input asp-for="LastName" class="form-control" />
        <span asp-validation-for="LastName" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label>Mobile No</label>
        <input asp-for="MobileNo" class="form-control" />
        <span asp-validation-for="MobileNo" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label>Email</label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>

    <button type="submit" class="btn btn-primary">Save</button>

</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

## 5. Step 4: Client-Side Validation Setup

Ensure these scripts are included:

```html
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```

This enables **automatic client-side validation** using Data Annotations.

---

## 6. Demo Output (Expected Behavior)

| User Action              | Result                |
| ------------------------ | --------------------- |
| Submit empty form        | Required field errors |
| Enter invalid email      | Email format error    |
| Enter mobile < 10 digits | Length error          |
| Enter valid data         | Success page          |

---

## 7. Key Learning from Demo

* Data Annotations control validation logic
* Same rules work on client & server
* No manual validation code needed
* Clean, readable, and maintainable code

---

## 8. Exam-Oriented Conclusion

> In ASP.NET Core MVC, Data Annotations are used to apply validation rules on model properties. These rules are automatically enforced during model binding and checked using ModelState.IsValid on the server side, while client-side validation is handled using jQuery unobtrusive validation.

---

✅ **This demo is sufficient for practical exams, viva, and interviews.**
<script src="~/lib/jquery/dist/jquery.min.js"></script>
   <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
   <script src="~/js/site.js" asp-append-version="true"></script>
   @await RenderSectionAsync("Scripts", required: false)

   <script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
   <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
