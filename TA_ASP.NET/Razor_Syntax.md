# Razor Syntax in ASP.NET MVC / ASP.NET Core

---

## 📌 Introduction to Razor

**Razor** is a markup syntax used in **ASP.NET MVC** and **ASP.NET Core** to create dynamic web pages. It allows developers to combine **HTML** and **C# code** in a clean and readable way.

- Razor files use the **`.cshtml`** extension
- Razor is not a programming language, it is a **view engine**

---

## 🎯 Why Use Razor?

- Easy integration of C# with HTML
- Clean and readable syntax
- Faster page rendering
- Supports layouts, partial views, and data passing
- Widely used in real-world ASP.NET applications

---

## 🧠 Basic Razor Syntax Rules

### 1️⃣ `@` Symbol
The `@` symbol is used to switch from HTML to C#.

```cshtml
<h1>@DateTime.Now</h1>
```

---

### 2️⃣ Razor Code Block `@{ }`
Used to write multiple lines of C# code.

```cshtml
@{
    int x = 10;
    int y = 20;
    int result = x + y;
}

<p>Result: @result</p>
```

---

### 3️⃣ Razor Expression
Used to print a single value.

```cshtml
<p>Day: @DateTime.Now.DayOfWeek</p>
```

---

### 4️⃣ Conditional Statements

```cshtml
@{
    int marks = 60;
}

@if (marks >= 50)
{
    <p>Result: Pass</p>
}
else
{
    <p>Result: Fail</p>
}
```

---

### 5️⃣ Loops in Razor

```cshtml
@for (int i = 1; i <= 3; i++)
{
    <p>Number: @i</p>
}
```

---

## 🧮 Example: Printing Table of 5 Using Razor

### 📄 View File: `Table.cshtml`

```cshtml
@{
    ViewData["Title"] = "Table of 5";
}

<h2>Table of 5</h2>

<table border="1" cellpadding="5">
    <tr>
        <th>Expression</th>
        <th>Result</th>
    </tr>

    @for (int i = 1; i <= 10; i++)
    {
        <tr>
            <td>5 x @i</td>
            <td>@(5 * i)</td>
        </tr>
    }
</table>
```

---

## 🔍 Explanation of Table Example

- `@for` loop runs from 1 to 10
- HTML table rows are generated dynamically
- `@(5 * i)` is used to evaluate expressions
- Razor handles switching between HTML and C# automatically

---

## ⚠️ Important Points to Remember

- Use `@` before C# code
- Use `@{ }` for multiple statements
- Use `@( )` for calculations
- Razor allows HTML inside loops and conditions

---

## 🎓 Exam / Viva Points

- Razor is a view engine
- `.cshtml` files contain HTML and C#
- Razor improves code readability
- Used in ASP.NET MVC and ASP.NET Core

---

## ✅ Conclusion

Razor syntax makes it easy to build dynamic and interactive web pages in ASP.NET. With simple rules and powerful features, Razor is beginner-friendly and widely used in professional web development.

This example of printing a table clearly shows how Razor mixes HTML with C# logic.

---
