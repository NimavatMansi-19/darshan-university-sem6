using System;

namespace Lab_5
{
    abstract class Employee
    {
        public string Name;
        public double Salary;

        // No constructor here
        public abstract double CalculateBonus();
    }

    class Manager : Employee
    {
        public Manager(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }

        public override double CalculateBonus()
        {
            return Salary * 0.20;   // 20% bonus
        }
    }

    class Developer : Employee
    {
        public Developer(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }

        public override double CalculateBonus()
        {
            return Salary * 0.10;   // 10% bonus
        }
    }
}
