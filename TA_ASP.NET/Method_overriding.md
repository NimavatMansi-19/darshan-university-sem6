# 🔁 Method Overriding in C# (Easy Explanation)

Method overriding in C# allows a **child class to provide a new implementation** of a method that is already defined in its **parent class**.

---

## 📌 Why Method Overriding Is Important?
- Achieves **runtime polymorphism**
- Allows child classes to change parent behavior
- Supports **flexibility and reusability**
- Improves real-world modeling using inheritance

---

## 🔹 What Is Method Overriding?

👉 Method overriding happens when:
- A method in the **parent class** is marked as `virtual`
- The **child class** provides its own version using the `override` keyword
- Method name, return type, and parameters must be **same**

---

## 🔑 Keywords Used

- `virtual` → Used in parent class
- `override` → Used in child class

---

## ✅ Basic Example of Method Overriding

```csharp
using System;

class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal makes a sound");
    }
}

class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Dog barks");
    }
}

class Program
{
    static void Main()
    {
        Animal a = new Dog();
        a.Sound();  // Calls Dog's method
    }
}
```

### 🧠 Output:
```
Dog barks
```

---

## 🔍 How It Works (Easy Words)
- Parent class method is marked `virtual`
- Child class method uses `override`
- Method call depends on **object type**, not reference type
- Decision is made at **runtime**

---

## 🧩 Real-Life Example

- Parent: **Vehicle** → `Start()`
- Child: **Car**, **Bike** → Different ways to start

```csharp
class Vehicle
{
    public virtual void Start()
    {
        Console.WriteLine("Vehicle is starting");
    }
}

class Car : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Car starts with key");
    }
}
```

---

## ⚠️ Important Rules of Method Overriding

- Inheritance is **required**
- Method must be `virtual` in parent class
- Method must be `override` in child class
- Method signature must be same
- Cannot override `static` methods

---

## 🔄 Method Overloading vs Method Overriding

| Feature | Method Overloading | Method Overriding |
|-------|-------------------|------------------|
| Class | Same class | Parent & Child |
| Method Name | Same | Same |
| Parameters | Different | Same |
| Polymorphism | Compile-time | Runtime |

---

## 🧠 Easy Way to Remember

- **Overloading** → Same name, different parameters
- **Overriding** → Same method, new behavior

---

✅ *This file is beginner-friendly, exam-ready, and easy to revise.*

