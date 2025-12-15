# 🔐 Access Modifiers in C# (Easy Explanation)

Access modifiers in C# define **where a class, method, or variable can be accessed from**. They help in **data security and proper code structure**.

---

## 📌 Why Access Modifiers Are Important?
- Protect data from unauthorized access
- Control visibility of members
- Improve code maintainability
- Support encapsulation (OOP concept)

---

## 🔹 Types of Access Modifiers in C#

1. `public`
2. `private`
3. `protected`
4. `internal`
5. `protected internal`

---

## 1️⃣ Public Access Modifier

### 👉 Definition:
Members declared as `public` can be accessed **from anywhere**.

### ✅ Example:
```csharp
using System;

class Student
{
    public string name = "Mansi";
}

class Program
{
    static void Main()
    {
        Student s = new Student();
        Console.WriteLine(s.name); // Accessible
    }
}
```

📌 **Use case:** When data or methods need to be available globally.

---

## 2️⃣ Private Access Modifier

### 👉 Definition:
Members declared as `private` can be accessed **only within the same class**.

### ✅ Example:
```csharp
class Student
{
    private int marks = 85;

    public void ShowMarks()
    {
        Console.WriteLine(marks); // Accessible inside class
    }
}
```

❌ `marks` cannot be accessed directly from outside the class.

📌 **Use case:** To hide sensitive data.

---

## 3️⃣ Protected Access Modifier

### 👉 Definition:
Members declared as `protected` can be accessed **within the same class or derived (child) class**.

### ✅ Example:
```csharp
class Person
{
    protected string name = "Mansi";
}

class Student : Person
{
    public void Display()
    {
        Console.WriteLine(name); // Accessible in child class
    }
}
```

📌 **Use case:** When data should be shared with child classes.

---

## 4️⃣ Internal Access Modifier

### 👉 Definition:
Members declared as `internal` can be accessed **within the same assembly (project)**.

### ✅ Example:
```csharp
internal class College
{
    internal string collegeName = "ABC College";
}
```

📌 **Use case:** When members should be shared within the same project only.

---



## 📊 Quick Summary Table

| Access Modifier | Same Class | Same Project | Child Class | Anywhere |
|-----------------|-----------|--------------|-------------|----------|
| public          | ✅        | ✅           | ✅          | ✅       |
| private         | ✅        | ❌           | ❌          | ❌       |
| protected       | ✅        | ❌           | ✅          | ❌       |
| internal        | ✅        | ✅           | ❌          | ❌       |


---

## 🧠 Easy Way to Remember
- **Public** → Everywhere
- **Private** → Same class only
- **Protected** → Parent + Child
- **Internal** → Same project

---


