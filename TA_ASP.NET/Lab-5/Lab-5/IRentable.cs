using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    interface IRentable
    {
        double CalculateRent();
        void DisplayDetails();

    }
    class car : IRentable
    {
        public string Model;
        public int Days;
        public double RatePerDay = 1500; // Car rate

        public car(string model, int days)
        {
            Model = model;
            Days = days;
        }

        public double CalculateRent()
        {
            return Days * RatePerDay;
        }

        public void DisplayDetails()
        {
            Console.WriteLine("Car Model: " + Model);
            Console.WriteLine("Days Rented: " + Days);
            Console.WriteLine("Total Rent: " + CalculateRent());
            Console.WriteLine();
        }
    }
    class Bike : IRentable
    {
        public string Model;
        public int Days;
        public double RatePerDay = 500; // Bike rate

        public Bike(string model, int days)
        {
            Model = model;
            Days = days;
        }

        public double CalculateRent()
        {
            return Days * RatePerDay;
        }

        public void DisplayDetails()
        {
            Console.WriteLine("Bike Model: " + Model);
            Console.WriteLine("Days Rented: " + Days);
            Console.WriteLine("Total Rent: " + CalculateRent());
            Console.WriteLine();
        }
    }

}
