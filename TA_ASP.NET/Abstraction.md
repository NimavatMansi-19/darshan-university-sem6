# 📘 Abstraction in Object-Oriented Programming (OOP)

## 🔹 What is Abstraction?
**Abstraction** means **hiding implementation details** and showing **only essential features** to the user.

👉 In simple words: **WHAT to do, not HOW to do**.

---

## 🌍 Real-Life Example

### Example: ATM Machine
- You insert card
- Enter PIN
- Withdraw money

You **don’t know** how ATM verifies PIN or counts cash internally.
That hidden logic is **abstraction**.

---

## 🧱 Why Do We Need Abstraction?
- Reduces complexity
- Improves security
- Makes code easy to maintain
- Focuses on essential behavior

---

## 🛠 How is Abstraction Achieved in C#?

C# provides abstraction using:
1️⃣ **Abstract Class**  
2️⃣ **Interface**

---

## 1️⃣ Abstraction Using Abstract Class

### 🔸 Abstract Class
- Can have **abstract methods** (no body)
- Can have **normal methods** (with body)
- Cannot create object of abstract class

### Example Code

```csharp
abstract class Shape
{
    public abstract void Draw();   // abstract method

    public void Info()
    {
        Console.WriteLine("This is a shape");
    }
}
```

### Derived Class

```csharp
class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing Circle");
    }
}
```

### Main Method

```csharp
class Program
{
    static void Main()
    {
        Shape s = new Circle();
        s.Info();
        s.Draw();
    }
}
```

---

## 2️⃣ Abstraction Using Interface

### Interface

```csharp
interface IVehicle
{
    void Start();
}
```

### Implementing Class

```csharp
class Car : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Car started");
    }
}
```

---

## 🔁 Key Differences: Abstract Class vs Interface

| Feature | Abstract Class | Interface |
|------|---------------|-----------|
| Methods | Abstract + Normal | Only abstract |
| Fields | Allowed | Not allowed |
| Multiple Inheritance | Not allowed | Allowed |
| Constructor | Yes | No |

---

## 🧠 Abstraction vs Encapsulation (Simple)

| Abstraction | Encapsulation |
|------------|---------------|
| Hides implementation | Hides data |
| Focuses on design | Focuses on security |
| Achieved using abstract & interface | Achieved using access modifiers |

---

## 🎤 Viva / Oral Exam Questions (with Short Answers)

### 1️⃣ What is abstraction?
Hiding implementation details and showing only essential features.

---

### 2️⃣ Why is abstraction used?
To reduce complexity and improve security.

---

### 3️⃣ How is abstraction achieved in C#?
Using abstract classes and interfaces.

---

### 4️⃣ Can we create object of abstract class?
No, we cannot.

---

### 5️⃣ Can abstract class have normal methods?
Yes.

---

### 6️⃣ What is the difference between abstraction and encapsulation?
Abstraction hides logic, encapsulation hides data.

---

### 7️⃣ Give a real-life example of abstraction.
ATM machine, mobile phone.

---

### 8️⃣ Can an abstract class implement an interface?
Yes.

---

### 9️⃣ Can interface have implementation?
No (except default methods in newer versions).

---

### 🔟 Which is faster: abstract class or interface?
Abstract class (generally).

---


