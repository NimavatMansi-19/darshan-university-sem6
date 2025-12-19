using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Single Inheritance means one child class inherits from one parent class.

            //Dog d = new Dog();
            //d.Eat();
            //d.Bark(); 



            // Multilevel inheritance allows access to all parent class
            // methods through the lowest child class object.

            //ElectricCar ec = new ElectricCar();
            //ec.ShowVehicle();      
            //ec.ShowCar();          
            //ec.ShowElectricCar();  



            // Polymorphism allows a base class reference to call different
            // implementations of a method depending on the object type.

            //Shape s1 = new Circle(5);        
            //Shape s2 = new Rectangle(4, 6); 
            //Console.WriteLine("Area of Circle: " + s1.Area());
            //Console.WriteLine("Area of Rectangle: " + s2.Area());



            // Abstraction hides implementation details by using an abstract class
            // and allows method calls through a base class reference using overridden
            // methods in derived classes.

            Fan a1 = new Fan();
            Light a2 = new Light();
            a1.TurnOn();
            a2.TurnOn();



            // An interface defines a contract that implementing classes
            // must follow by providing method definitions.

            //IPrintable p1 = new Book("C# Basics", "Abc");
            //IPrintable p2 = new Magazine("Tech World", 25);
            //p1.PrintDetails();   
            //Console.WriteLine();
            //p2.PrintDetails();  



            // C# supports multiple inheritance through interfaces by
            // allowing a class to implement more than one interface.

            //Robot r = new Robot();
            //r.Move();
            //r.MakeSound();



            // Abstraction allows defining payment behavior in a
            // base class while derived classes provide specific payment implementations.

            //try
            //{
            //    Console.Write("Enter payment amount: ");
            //    double amt = Convert.ToDouble(Console.ReadLine());

            //    Console.Write("Choose Payment Method (1-Credit Card, 2-UPI): ");
            //    int choice = Convert.ToInt32(Console.ReadLine());

            //    Payment payment;

            //    if (choice == 1)
            //        payment = new CreditCardPayment(amt);
            //    else
            //        payment = new UPIPayment(amt);

            //    payment.MakePayment(); // Polymorphism
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //}




            // Polymorphism allows different bonus calculations
            // to be executed using a common base class reference.

            //Employee e1 = new Manager("Abc", 50000);
            //Employee e2 = new Developer("Xyz", 40000);
            //Console.WriteLine(e1.Name + " Bonus: " + e1.CalculateBonus());
            //Console.WriteLine(e2.Name + " Bonus: " + e2.CalculateBonus());




            // Interfaces allow different vehicle types to be treated
            // uniformly while providing their own rent calculation logic.

            //List<IRentable> rentals = new List<IRentable>();
            //rentals.Add(new car("Honda City", 3));
            //rentals.Add(new Bike("Royal Enfield", 2));

            //foreach (IRentable r in rentals)
            //{
            //    r.DisplayDetails(); // Polymorphism
            //}


        }
    }
}
