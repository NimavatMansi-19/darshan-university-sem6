
# ASP.NET MVC – ViewData, ViewBag, and TempData (With Proper Examples)

## Introduction

ASP.NET MVC provides multiple ways to pass data between controllers and views.
The most commonly used mechanisms are:

- ViewData
- ViewBag
- TempData

This document explains **what they are, when to use them, and proper practical examples**, especially focusing on **TempData**, which is important for redirects.

---

## 1. ViewData

### Description
- Dictionary-based data passing
- Works only for the current request
- Data lost after view is rendered

### Example

**Controller**
```csharp
ViewData["Name"] = "Rahul";
ViewData["Age"] = 20;
```

**View**
```cshtml
<p>Name: @ViewData["Name"]</p>
<p>Age: @ViewData["Age"]</p>
```

### Use When
- Passing small key-value data
- Single request scenario

---

## 2. ViewBag

### Description
- Dynamic wrapper over ViewData
- Cleaner syntax
- Same request lifetime

### Example

**Controller**
```csharp
ViewBag.City = "Ahmedabad";
ViewBag.State = "Gujarat";
```

**View**
```cshtml
<p>City: @ViewBag.City</p>
<p>State: @ViewBag.State</p>
```

### Use When
- UI display data
- Quick development

---

## 3. TempData (Most Important)

### What is TempData?
- Used to pass data **between actions**
- Persists for **one redirect**
- Uses Session internally

---

## Example 1: TempData Between Two Controller Actions

### Scenario
Save data → Redirect → Show success message

### Controller
```csharp
public class StudentController : Controller
{
    public IActionResult Save()
    {
        TempData["SuccessMessage"] = "Student record saved successfully!";
        return RedirectToAction("Result");
    }

    public IActionResult Result()
    {
        return View();
    }
}
```

### View (`Result.cshtml`)
```cshtml
<h2>Result Page</h2>

@if (TempData["SuccessMessage"] != null)
{
    <p style="color:green;">
        @TempData["SuccessMessage"]
    </p>
}
```

---

## Example 2: TempData from Controller to View

### Controller
```csharp
public IActionResult Index()
{
    TempData["WelcomeMessage"] = "Welcome to ASP.NET MVC!";
    return View();
}
```

### View
```cshtml
<p>@TempData["WelcomeMessage"]</p>
```

---

## Example 3: TempData with Keep()

### Controller
```csharp
public IActionResult Step1()
{
    TempData["Info"] = "This data will persist!";
    return RedirectToAction("Step2");
}

public IActionResult Step2()
{
    TempData.Keep("Info");
    return RedirectToAction("Step3");
}

public IActionResult Step3()
{
    return View();
}
```

### View (`Step3.cshtml`)
```cshtml
<p>@TempData["Info"]</p>
```

---

## TempData Lifecycle Diagram

```
Action 1
  |
  |-- TempData["msg"] set
  |
Redirect
  |
Action 2
  |
Read TempData
  |
View
```

---

## Comparison Table

| Feature | ViewData | ViewBag | TempData |
|------|--------|--------|---------|
| Type | Dictionary | Dynamic | Dictionary |
| Lifetime | Current request | Current request | One redirect |
| Redirect Support | ❌ | ❌ | ✅ |
| Uses Session | ❌ | ❌ | ✅ |

---

## Which One to Use When?

- **ViewData** → Simple key-value, same request
- **ViewBag** → Clean syntax for UI data
- **TempData** → Redirects, success/error messages

---

## Key Exam Points

- TempData survives redirects
- ViewBag/ViewData do not
- TempData is ideal for flash messages
- TempData is cleared after read unless `Keep()` is used

---

## Conclusion

Correct usage of ViewData, ViewBag, and TempData ensures clean and maintainable ASP.NET MVC applications.
