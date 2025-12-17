# 📘 Interface in Object-Oriented Programming (OOP)

## 🔹 What is an Interface?
An **interface** is a blueprint of a class.

👉 It contains **only method declarations**, not implementations.

👉 Classes that implement an interface **must define all its methods**.

---

## 🌍 Real-Life Example

### Example: Payment System
- Different payment methods exist: **Credit Card**, **UPI**, **Cash**
- All payments must have a **Pay()** action

So, we create a **Payment interface** and let different classes implement it.

---

## 🧱 Why Use Interface?
- To achieve **multiple inheritance**
- To ensure **standard behavior**
- To provide **100% abstraction**

---

## 💻 Simple Code Example (C#)

### Interface
```csharp
interface IPayment
{
    void Pay();
}
```

### Class 1 Implementing Interface
```csharp
class CreditCard : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Payment done using Credit Card");
    }
}
```

### Class 2 Implementing Interface
```csharp
class UPI : IPayment
{
    public void Pay()
    {
        Console.WriteLine("Payment done using UPI");
    }
}
```

### Main Method
```csharp
class Program
{
    static void Main()
    {
        IPayment p1 = new CreditCard();
        IPayment p2 = new UPI();

        p1.Pay();
        p2.Pay();
    }
}
```

---

## 🔁 Key Points About Interface
- Interface methods are **public by default**
- Interface cannot have fields
- Interface cannot have constructors
- A class can implement **multiple interfaces**

---

## 🔀 Multiple Inheritance Using Interface

```csharp
interface A
{
    void Show();
}

interface B
{
    void Display();
}

class Test : A, B
{
    public void Show()
    {
        Console.WriteLine("Show method");
    }

    public void Display()
    {
        Console.WriteLine("Display method");
    }
}
```

---

## 📝 Interface vs Class (Simple Table)

| Feature | Class | Interface |
|------|------|-----------|
| Methods | With body | Without body |
| Inheritance | Single | Multiple |
| Objects | Can create | Cannot create |

---

## 🎤 Viva / Oral Exam Questions (with Short Answers)

### 1️⃣ What is an interface?
An interface is a collection of method declarations without implementation.

---

### 2️⃣ Why do we use interface?
To achieve multiple inheritance and abstraction.

---

### 3️⃣ Can we create an object of an interface?
No, interfaces cannot be instantiated.

---

### 4️⃣ Can interface have variables?
No, interface cannot have fields.

---

### 5️⃣ How does a class use an interface?
By using the `:` symbol and implementing all methods.

---

### 6️⃣ Does C# support multiple inheritance?
Yes, using interfaces.

---

### 7️⃣ Are interface methods public?
Yes, by default.

---

### 8️⃣ Difference between abstract class and interface?
Abstract class can have method bodies; interface cannot.

---

