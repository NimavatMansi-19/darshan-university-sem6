# 📘 Inheritance in Object-Oriented Programming (OOP)

## 🔹 What is Inheritance?
Inheritance means **one class gets properties and methods of another class**.

👉 In simple words: **child uses parent’s features**.

---

## 🌍 Real-Life Example

### Example: Vehicle
- A **Vehicle** has speed and can move
- A **Car** is a Vehicle
- A **Bike** is also a Vehicle

So instead of writing speed and move logic again, **Car and Bike inherit it** from Vehicle.

---

## 🧱 Basic Structure

- **Parent class** → Base / Super class
- **Child class** → Derived / Sub class

---

## 💻 Simple Code Example (C#)

### Parent Class
```csharp
class Vehicle
{
    public void Move()
    {
        Console.WriteLine("Vehicle is moving");
    }
}
```

### Child Class 1
```csharp
class Car : Vehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving");
    }
}
```

### Child Class 2
```csharp
class Bike : Vehicle
{
    public void Ride()
    {
        Console.WriteLine("Bike is riding");
    }
}
```

### Main Method
```csharp
class Program
{
    static void Main()
    {
        Car c = new Car();
        c.Move();   // inherited method
        c.Drive();

        Bike b = new Bike();
        b.Move();   // inherited method
        b.Ride();
    }
}
```

---

## 🔁 Types of Inheritance

### 1️⃣ Single Inheritance
One child inherits from one parent.

```csharp
class Animal { }
class Dog : Animal { }
```

---

### 2️⃣ Multilevel Inheritance
Child inherits from another child.

```csharp
class Animal { }
class Mammal : Animal { }
class Dog : Mammal { }
```

---

### 3️⃣ Hierarchical Inheritance
Multiple children inherit from one parent.

```csharp
class Animal { }
class Dog : Animal { }
class Cat : Animal { }
```

---

### ❌ Multiple Inheritance (Not supported in C#)
One class cannot inherit from multiple classes.

```csharp
// NOT allowed in C#
class A { }
class B { }
class C : A, B { }
```

---

## ✅ Advantages of Inheritance
- Code reusability
- Easy maintenance
- Better organization

---

## 📝 Summary
- Inheritance allows **reuse of code**
- Uses `:` symbol in C#
- Supports single, multilevel, hierarchical

---

✨ Easy to remember: **IS-A relationship**
- Car IS A Vehicle
- Dog IS AN Animal

---

## 🎤 Viva / Oral Exam Questions (with Short Answers)

### 1️⃣ What is inheritance?
Inheritance is a process where one class acquires properties and methods of another class.

---

### 2️⃣ Why do we use inheritance?
To reuse code and reduce duplication.

---

### 3️⃣ What is a parent class?
A class whose properties are inherited by another class.

---

### 4️⃣ What is a child class?
A class that inherits properties and methods from a parent class.

---

### 5️⃣ What keyword is used for inheritance in C#?
The `:` (colon) symbol.

---

### 6️⃣ Give a real-life example of inheritance.
Car is a Vehicle, Dog is an Animal.

---

### 7️⃣ What is single inheritance?
One child class inherits from one parent class.

---

### 8️⃣ What is multilevel inheritance?
A class inherits from another derived class.

---

### 9️⃣ What is hierarchical inheritance?
Multiple child classes inherit from one parent class.

---

### 🔟 Does C# support multiple inheritance?
No, C# does not support multiple inheritance using classes.

---

### 1️⃣1️⃣ What is the advantage of inheritance?
Code reusability and easy maintenance.

---

### 1️⃣2️⃣ What relationship does inheritance represent?
IS-A relationship.

---

