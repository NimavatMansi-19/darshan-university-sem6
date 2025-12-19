# 📘 C# Collection Classes – Complete & Correct Notes

## 🔹 Definition of Collection Classes

**Collection classes in C#** are used to store, manage, and manipulate **groups of data dynamically**. Unlike arrays, collection classes can **grow or shrink at runtime** and provide **built-in methods** for easy data handling.

Namespaces used:
- `System.Collections`
- `System.Collections.Generic`

---

## 🔹 Classification of Collection Classes in C#

Collection classes are classified into **two main types**:

### 1️⃣ Non-Generic Collection Classes
- Store data as `object`
- Not type-safe
- Slower compared to generic collections
- Namespace: `System.Collections`

### 2️⃣ Generic Collection Classes
- Store data of a **specific type**
- Type-safe
- Better performance
- Namespace: `System.Collections.Generic`

---

# 🟦 NON-GENERIC COLLECTION CLASSES

## 1️⃣ ArrayList

### 🔸 Definition
Stores elements dynamically and allows **different data types**.

### 🔸 Real-Life Example
A **shopping bag** where different items can be added or removed.

### 🔸 How to Define (Code)
```csharp
using System.Collections;

ArrayList list = new ArrayList();
list.Add(10);
list.Add("Apple");
list.Add(5.5);
```

### 🔸 Important Methods
- `Add()`
- `Remove()`
- `RemoveAt()`
- `Insert()`
- `Count`
- `Clear()`

---

## 2️⃣ Hashtable

### 🔸 Definition
Stores data in **key–value pairs**, where keys are unique.

### 🔸 Real-Life Example
A **dictionary** where a word (key) has a meaning (value).

### 🔸 How to Define (Code)
```csharp
using System.Collections;

Hashtable ht = new Hashtable();
ht.Add(1, "John");
ht.Add(2, "Emma");
```

### 🔸 Important Methods
- `Add(key, value)`
- `Remove(key)`
- `ContainsKey()`
- `ContainsValue()`
- `Keys`
- `Values`

---

## 3️⃣ Stack (Non-Generic)

### 🔸 Definition
Works on **LIFO (Last In First Out)** principle.

### 🔸 Real-Life Example
A **stack of books**.

### 🔸 How to Define (Code)
```csharp
using System.Collections;

Stack st = new Stack();
st.Push(10);
st.Push(20);
```

### 🔸 Important Methods
- `Push()`
- `Pop()`
- `Peek()`
- `Count`
- `Clear()`

---

## 4️⃣ Queue (Non-Generic)

### 🔸 Definition
Works on **FIFO (First In First Out)** principle.

### 🔸 Real-Life Example
A **queue at a ticket counter**.

### 🔸 How to Define (Code)
```csharp
using System.Collections;

Queue q = new Queue();
q.Enqueue("A");
q.Enqueue("B");
```

### 🔸 Important Methods
- `Enqueue()`
- `Dequeue()`
- `Peek()`
- `Count`
- `Clear()`

---

## 5️⃣ SortedList

### 🔸 Definition
Stores **key–value pairs in sorted order of keys**.

### 🔸 Real-Life Example
A **phone contact list sorted alphabetically**.

### 🔸 How to Define (Code)
```csharp
using System.Collections;

SortedList sl = new SortedList();
sl.Add(1, "A");
sl.Add(3, "C");
sl.Add(2, "B");
```

### 🔸 Important Methods
- `Add(key, value)`
- `Remove(key)`
- `GetKey(index)`
- `GetByIndex(index)`
- `Count`

---

# 🟩 GENERIC COLLECTION CLASSES

> Generic collections use **type parameters `<T>`** and are preferred in real applications.

## 1️⃣ List<T>

### 🔸 Definition
Stores elements of a **specific type** dynamically.

### 🔸 Real-Life Example
A **to-do list application**.

### 🔸 How to Define (Code)
```csharp
using System.Collections.Generic;

List<string> items = new List<string>();
items.Add("Milk");
items.Add("Bread");
```

### 🔸 Important Methods
- `Add()`
- `Remove()`
- `RemoveAt()`
- `Contains()`
- `Count`
- `Clear()`

---

## 2️⃣ Dictionary<TKey, TValue>

### 🔸 Definition
Stores **unique keys with values**.

### 🔸 Real-Life Example
**Roll number → Student name** mapping.

### 🔸 How to Define (Code)
```csharp
using System.Collections.Generic;

Dictionary<int, string> dict = new Dictionary<int, string>();
dict.Add(1, "Alice");
dict.Add(2, "Bob");
```

### 🔸 Important Methods
- `Add(key, value)`
- `Remove(key)`
- `ContainsKey()`
- `ContainsValue()`
- `Keys`
- `Values`

---

## 3️⃣ Stack<T>

### 🔸 Definition
Generic stack that follows **LIFO**.

### 🔸 Real-Life Example
**Undo operation** in applications.

### 🔸 How to Define (Code)
```csharp
using System.Collections.Generic;

Stack<int> stack = new Stack<int>();
stack.Push(100);
stack.Push(200);
```

### 🔸 Important Methods
- `Push()`
- `Pop()`
- `Peek()`
- `Count`

---

## 4️⃣ Queue<T>

### 🔸 Definition
Generic queue that follows **FIFO**.

### 🔸 Real-Life Example
**Customer service queue**.

### 🔸 How to Define (Code)
```csharp
using System.Collections.Generic;

Queue<string> queue = new Queue<string>();
queue.Enqueue("Customer1");
queue.Enqueue("Customer2");
```

### 🔸 Important Methods
- `Enqueue()`
- `Dequeue()`
- `Peek()`
- `Count`

---

## 5️⃣ HashSet<T>

### 🔸 Definition
Stores **only unique elements**.

### 🔸 Real-Life Example
A list of **unique email subscribers**.

### 🔸 How to Define (Code)
```csharp
using System.Collections.Generic;

HashSet<string> emails = new HashSet<string>();
emails.Add("a@gmail.com");
emails.Add("b@gmail.com");
```

### 🔸 Important Methods
- `Add()`
- `Remove()`
- `Contains()`
- `Count`
- `Clear()`

---


