# ASP.NET Core – HTML Helper & Custom Tag Helper
---
# 1️⃣ What is an HTML Helper?

**HTML Helpers** are server-side methods used inside Razor views to generate HTML elements dynamically.

They are written using the **`@Html` syntax** and help create HTML controls that bind directly to model properties.

Instead of manually writing HTML input fields, HTML Helpers automatically manage:

* Model binding
* Input names and values
* Validation integration

---

# 2️⃣ Names of Common HTML Helpers

ASP.NET Core provides many built-in HTML Helpers.

| HTML Helper                   | Purpose                                |
| ----------------------------- | -------------------------------------- |
| `Html.BeginForm()`            | Creates a form                         |
| `Html.EndForm()`              | Ends a form                            |
| `Html.TextBox()`              | Creates a textbox                      |
| `Html.TextBoxFor()`           | Creates textbox bound to model         |
| `Html.Password()`             | Creates password field                 |
| `Html.PasswordFor()`          | Creates password field bound to model  |
| `Html.TextArea()`             | Creates textarea                       |
| `Html.TextAreaFor()`          | Creates textarea bound to model        |
| `Html.DropDownList()`         | Creates dropdown                       |
| `Html.DropDownListFor()`      | Creates dropdown bound to model        |
| `Html.Label()`                | Creates label                          |
| `Html.LabelFor()`             | Creates label bound to model           |
| `Html.Hidden()`               | Creates hidden field                   |
| `Html.HiddenFor()`            | Creates hidden field bound to model    |
| `Html.ValidationMessage()`    | Displays validation message            |
| `Html.ValidationMessageFor()` | Displays validation for model property |

---

# 3️⃣ Example Syntax

Example of HTML Helper:

```
@Html.TextBox("Name")
```
Generated HTML:
```
<input type="text" name="Name">
```

## 1️⃣ Without Validation Attributes

### Model

```csharp
public class User
{
    public string Name { get; set; }
}
```

### View

```csharp
@model User
@Html.TextBoxFor(m => m.Name)
```

### Generated HTML

```html
<input type="text" name="Name" id="Name">
```

Since there are **no validation rules**, ASP.NET MVC only generates:
- `name`
- `id`

---

## 2️⃣ With Validation Attributes

### Model

```csharp
using System.ComponentModel.DataAnnotations;

public class User
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}
```

### View

```csharp
@model User
@Html.TextBoxFor(m => m.Name)
```

### Generated HTML

```html
<input type="text"
       name="Name"
       id="Name"
       data-val="true"
       data-val-required="Name is required">
```

---

## What Are `data-val` Attributes?

```
data-val="true"
data-val-required="Name is required"
```

These attributes are generated from **DataAnnotations validation attributes**.

They are used by **jQuery Unobtrusive Validation** for **client-side validation**.

---


# 4️⃣ Example – HTML Helper in a Form

## Step 1 – Create Model

```csharp
public class User
{
    [Required]
    public string Name { get; set; }
     [Required]
    public string Email { get; set; }
}
```

---

## Step 2 – Create Controller

```csharp
public class UserController : Controller
{
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User model)
    {
        return View();
    }
}
```

---

## Step 3 – Razor View

```html
@model User

<h2>Create User</h2>

@using (Html.BeginForm())
{
    <div>
        <label>Name</label>
        @Html.TextBoxFor(m => m.Name)
    </div>

    <div>
        <label>Email</label>
        @Html.TextBoxFor(m => m.Email)
    </div>

    <br/>

    <button type="submit">Submit</button>
}
```

### Explanation

* `Html.BeginForm()` creates the `<form>` element.
* `Html.TextBoxFor()` generates input fields connected to model properties.
* When the form is submitted, ASP.NET Core automatically binds the values to the **User model**.

---

# 5️⃣ What is a Tag Helper?

A **Tag Helper** allows server-side code to run within HTML elements.

It looks like normal HTML but performs **server-side processing** before rendering the final output.

Example:

```html
<input asp-for="Email" />
```

ASP.NET Core converts it into:

```html
<input type="text" name="Email" value="">
```

Tag Helpers make Razor code **cleaner and easier to read**.

---
# ASP.NET Core Built-in Tag Helpers List

Below is a list of commonly used **built-in Tag Helpers in ASP.NET Core**.

| Tag Helper                    | Main Attribute(s)                           | Purpose                                           |
| ----------------------------- | ------------------------------------------- | ------------------------------------------------- |
| Anchor Tag Helper             | `asp-controller`, `asp-action`, `asp-route` | Generates hyperlinks based on routing             |
| Form Tag Helper               | `asp-controller`, `asp-action`, `asp-route` | Generates forms that submit to controller actions |
| Input Tag Helper              | `asp-for`                                   | Generates input fields bound to model properties  |
| Label Tag Helper              | `asp-for`                                   | Generates label for model properties              |
| Textarea Tag Helper           | `asp-for`                                   | Generates textarea fields                         |
| Select Tag Helper             | `asp-for`, `asp-items`                      | Generates dropdown lists                          |
| Option Tag Helper             | `value`                                     | Defines options inside a dropdown                 |
| Validation Message Tag Helper | `asp-validation-for`                        | Displays validation message for a field           |
| Validation Summary Tag Helper | `asp-validation-summary`                    | Displays summary of validation errors             |



# 6️⃣ What is a Custom Tag Helper?

A Custom Tag Helper is a developer-created component that allows custom HTML tags or attributes to generate dynamic HTML.

Tag Helpers make Razor views:

Cleaner

More readable

Reusable

---

# 7️⃣ Example – Custom Image Tag Helper

Suppose we want to write a simple custom tag like this:
```
<cute image-link="images/cat.png" alternative-text="Cute Cat"></cute>
```
This custom tag will automatically generate an image (img) element.


# 8️⃣ Creating the Custom Tag Helper

Create a folder in the project called:

```
TagHelpers
```

---

# 9️⃣CuteTagHelper Class
```
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelpers.TagHelpers
{
    [HtmlTargetElement("cute")]
    public class CuteTagHelper : TagHelper
    {
        public string ImageLink { get; set; }
        public string AlternativeText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.StartTagOnly;

            output.Attributes.SetAttribute("src", ImageLink);
            output.Attributes.SetAttribute("alt", AlternativeText);
        }
    }
}
```
---

# 🔟 Enable Tag Helpers

Open **_ViewImports.cshtml** and add:

```html
@addTagHelper *, YourProjectName
```

This registers the custom Tag Helper in the project.

---

# 1️⃣1️⃣ Using the Custom Tag Helper

Now we can use the custom attribute inside a Razor view.

```html
<cute image-link="/images/dog.jpg" alternative-text="Cute Dog"></cute>
```

---

# 1️⃣2️⃣ Output Generated

ASP.NET Core converts it into:

```html
<img src="/images/dog.jpg" alt="Cute Dog">
```

The Tag Helper automatically modifies the input element.

---



