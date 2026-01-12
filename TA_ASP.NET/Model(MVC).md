# Model in ASP.NET Core (MVC)

## 1. What is a Model in ASP.NET Core?

In **ASP.NET Core MVC**, a **Model** represents the **data and business logic** of the application.

In simple words:

> A **Model** is a C# class that holds data, rules, and logic related to the application.

It acts as a bridge between:

* **Database** 🗄️
* **Controller** 🎮
* **View** 🖥️

---

## 2. Role of Model in MVC Architecture

MVC stands for:

* **M – Model** → Data & business rules
* **V – View** → User Interface
* **C – Controller** → Handles user requests

### Flow:

1. User sends a request
2. Controller processes the request
3. Controller uses the Model to get or save data
4. Controller sends data to the View
5. View displays data to the user

---

## 3. Real-Life Example (Easy)

### Scenario: Student Management System

Think of a **college register**:

* Student Name
* Roll Number
* Semester
* SPI

This information is stored and managed by a **Model**.

---

## 4. Creating a Model in ASP.NET Core

### Step 1: Create Models Folder

Inside your project, create a folder named:

```
Models
```

---

### Step 2: Create a Model Class

Create a file named **Student.cs**

```csharp
using System.ComponentModel.DataAnnotations;

namespace YourProjectName.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 8)]
        public int Semester { get; set; }

        [Range(0.0, 10.0)]
        public double SPI { get; set; }
    }
}
```

---

## 5. Explanation of Model Code

| Property | Meaning                   |
| -------- | ------------------------- |
| Id       | Unique student ID         |
| Name     | Student name              |
| Semester | Current semester          |
| SPI      | Student performance index |

### Data Annotations Used

* **[Required]** → Field must not be empty
* **[Range]** → Value must be within given range

These help in **validation**.

---

## 6. Using Model in Controller

```csharp
using Microsoft.AspNetCore.Mvc;
using YourProjectName.Models;

public class StudentController : Controller
{
    public IActionResult Details()
    {
        Student student = new Student()
        {
            Id = 1,
            Name = "Mansi",
            Semester = 5,
            SPI = 8.7
        };

        return View(student);
    }
}
```

---

## 7. Passing Model to View

### View File: Details.cshtml

```razor
@model YourProjectName.Models.Student

<h2>Student Details</h2>

<p><b>Name:</b> @Model.Name</p>
<p><b>Semester:</b> @Model.Semester</p>
<p><b>SPI:</b> @Model.SPI</p>
```

---

## 8. Steps to Create a Model in ASP.NET Core MVC

This section explains **step-by-step how to create and use a Model in ASP.NET Core MVC**, without going deep into EF Core.

---

### Step 1: Create a Models Folder

In your ASP.NET Core MVC project:

* Right-click on the project
* Select **Add → New Folder**
* Name it **Models**

This folder is used to store all model classes.

---

### Step 2: Add a Model Class

Inside the **Models** folder:

* Right-click → Add → Class
* Name the class (example: `Student.cs`)

```csharp
using System.ComponentModel.DataAnnotations;

namespace YourProjectName.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Semester { get; set; }

        public double SPI { get; set; }
    }
}
```

---

### Step 3: Add Validation Rules (Optional but Important)

Validation ensures correct data input from the user.

Common attributes:

* `[Required]` → Field must not be empty
* `[Range]` → Value must be within a range
* `[StringLength]` → Limit text length

These validations are automatically checked when data is submitted from a form.

---

### Step 4: Use Model in Controller (Detailed Explanation)

In this step, the **Controller uses the Model** to prepare data and send it to the View.

The controller acts as a **middle layer** between the Model and the View.

---

### What Happens in This Step?

1. Controller **creates an object** of the Model class
2. Data is **assigned to model properties**
3. Model object is **passed to the View**

---

### Example Controller Code

```csharp
using Microsoft.AspNetCore.Mvc;
using YourProjectName.Models;

public class StudentController : Controller
{
    public IActionResult Details()
    {
        // 1. Create object of Model
        Student student = new Student();

        // 2. Assign values to Model properties
        student.Id = 1;
        student.Name = "Mansi";
        student.Semester = 5;
        student.SPI = 8.7;

        // 3. Pass Model object to View
        return View(student);
    }
}
```

---

### Line-by-Line Explanation

* `Student student = new Student();`
  → Creates an object of the **Student Model**

* `student.Name = "Mansi";`
  → Stores data inside the model

* `return View(student);`
  → Sends the model to the View so it can be displayed

---

### Why Controller Uses Model?

* To keep **data logic separate** from UI
* To pass **structured data** to the View
* To maintain **MVC architecture rules**

---

### Real-Life Analogy

* **Model** → Filled student form
* **Controller** → Office clerk checking the form
* **View** → Notice board showing details

---

### Important Interview Point ⭐

> "The controller creates or receives the model, processes it, and passes it to the view."

---

### Step 5: Use Model in View

In the Razor view (`Details.cshtml`), strongly type the model:

```razor
@model YourProjectName.Models.Student

<h2>Student Details</h2>
<p>Name: @Model.Name</p>
<p>Semester: @Model.Semester</p>
<p>SPI: @Model.SPI</p>
```

---

### Step 6: Model Binding (Automatic Feature)

ASP.NET Core automatically:

* Takes input values from forms
* Converts them into model properties
* Sends the model to controller actions

This is called **Model Binding**.

---

### Step 7: Model Validation Check in Controller

```csharp
[HttpPost]
public IActionResult Create(Student student)
{
    if (ModelState.IsValid)
    {
        // Process data
        return RedirectToAction("Details");
    }

    return View(student);
}
```

---

### Real-Life Analogy

| Real Life              | MVC        |
| ---------------------- | ---------- |
| Form filled by student | Model      |
| Office staff           | Controller |
| Notice board           | View       |

---

### Key Points for Exams & Interviews

* Model is a **C# class**
* Stored inside **Models folder**
* Used to transfer data between **Controller and View**
* Supports **validation** and **binding**
* Core part of **MVC architecture**

---

---

## 9. Types of Models in ASP.NET Core

### 1. Domain Model

* Represents database entities
* Used with EF Core

### 2. View Model

* Used only for Views
* Combines multiple models

```csharp
public class StudentViewModel
{
    public string Name { get; set; }
    public double SPI { get; set; }
    public string Result { get; set; }
}
```

### 3. DTO (Data Transfer Object)

* Used for APIs
* Transfers only required data

---

## 10. Real-Life Analogy

| Real Life        | ASP.NET Core |
| ---------------- | ------------ |
| Student Register | Model        |
| Teacher          | Controller   |
| Report Card      | View         |

---

## 11. Why Models Are Important?

✅ Keeps code organized
✅ Separates business logic
✅ Easy validation
✅ Reusable across application
✅ Works with databases and APIs

---

## 12. Summary

* Model represents **data + rules**
* Written as **C# classes**
* Used by **Controller and View**
* Supports **validation and database mapping**
* Core part of **MVC architecture**

---

