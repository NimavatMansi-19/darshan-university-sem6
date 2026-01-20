# Data Annotations in ASP.NET Core MVC

> 

---

## 1. Introduction to Data Annotations

**Data Annotations** in ASP.NET Core MVC are **attributes** applied on **model properties** to:

* Validate user input
* Display validation messages
* Control UI rendering
* Enforce business rules at model level

They are part of the namespace:

```csharp
using System.ComponentModel.DataAnnotations;
```

Data Annotations work for both:

* **Client-side validation** (using jQuery)
* **Server-side validation** (using ModelState)

---

## 2. Why Validation is Required

Validation ensures:

* Correct and meaningful data
* Prevention of dirty / noisy data
* Proper data analysis in future
* Better user experience with error messages

### Example Validation Messages

* The First Name field is required
* Please enter valid Email Address
* Enter Age between 1 to 110
* Incorrect URL, enter valid URL

These are called **user-defined validation messages**.

---

## 3. Types of Validation

### 3.1 Client-Side Validation

* Happens in browser
* Faster feedback
* Requires JavaScript

Required libraries:

* jQuery
* jQuery.Validate
* jQuery.Validate.Unobtrusive

```html
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```

---

### 3.2 Server-Side Validation

* Happens on server
* More secure
* Cannot be bypassed

Uses **ModelState.IsValid**

```csharp
if (ModelState.IsValid)
{
    // Process data
}
else
{
    // Return validation errors
}
```

---

## 4. Common Data Annotation Attributes

### 4.1 [Required]

Ensures the field is not empty.

```csharp
[Required]
public string Name { get; set; }
```

With display name:

```csharp
[Required]
[Display(Name = "First Name")]
public string Name { get; set; }
```

With custom error message:

```csharp
[Required(ErrorMessage = "Please Enter Name")]
public string Name { get; set; }
```

---

### 4.2 [StringLength]

Defines maximum and minimum string length.

```csharp
[StringLength(10)]
public string Mobile { get; set; }
```

With minimum length:

```csharp
[StringLength(10, MinimumLength = 3, ErrorMessage = "Minimum 3 characters are required")]
public string Mobile { get; set; }
```

---

### 4.3 [Range]

Used for numeric values.

```csharp
[Range(1, 5)]
public double Rating { get; set; }
```

With custom error message:

```csharp
[Range(1, 5, ErrorMessage = "Enter Range Between 1 to 5")]
public double Rating { get; set; }
```

Possible Errors:

* Empty value → Required error
* Characters → Must be number
* Out of range → Range error

---

### 4.4 [RegularExpression]

Used for custom pattern validation.

**Aadhar Card Example**

```csharp
[RegularExpression("^[2-9]{1}[0-9]{3}\\s[0-9]{4}\\s[0-9]{4}$",
 ErrorMessage = "Invalid Aadhar Card No")]
public string AadharCard { get; set; }
```

#### Regex Explanation

* `^` → Start of string
* `[2-9]{1}` → First digit between 2–9
* `[0-9]{3}` → Next 3 digits
* `\\s` → Space
* `[0-9]{4}` → Next 4 digits
* `\\s` → Space
* `[0-9]{4}` → Last 4 digits
* `$` → End of string

---

### 4.5 [MinLength] and [MaxLength]

```csharp
[MinLength(4)]
public string LastName { get; set; }
```

With custom message:

```csharp
[MinLength(4, ErrorMessage = "Minimum 4 characters required")]
public string LastName { get; set; }
```

```csharp
[MaxLength(4, ErrorMessage = "Maximum 4 characters required")]
public string LastName { get; set; }
```

> Note: `MaxLength` restricts further input after limit.

---

### 4.6 [Compare]

Used to compare two fields.

```csharp
public string Password { get; set; }

[Compare("Password", ErrorMessage = "Password does not match")]
public string ConfirmPassword { get; set; }
```

---

### 4.7 [EmailAddress]

```csharp
[EmailAddress]
public string Email { get; set; }
```

With custom message:

```csharp
[EmailAddress(ErrorMessage = "Please Enter Valid Email")]
public string Email { get; set; }
```

---

## 5. Complete Employee Model Example

```csharp
public class EmployeeModel
{
    [Required(ErrorMessage = "First Name is Required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(10)]
    public string MobileNo { get; set; }

    [Required(ErrorMessage = "Enter Valid Email")]
    [EmailAddress]
    public string Email { get; set; }
}
```

---

## 6. Displaying Validation Messages in View

```html
<input asp-for="FirstName" class="form-control" />
<span asp-validation-for="FirstName" class="text-danger"></span>
```

---

## 7. Summary

* Data Annotations define validation rules on models
* They work on both client and server side
* Reduce controller code
* Improve data quality
* Essential for ASP.NET Core MVC applications

---

