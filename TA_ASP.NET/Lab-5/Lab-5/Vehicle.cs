using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class Vehicle
    {
        public void ShowVehicle()
        {
            Console.WriteLine("This is a Vehicle");
        }
    }
    class Car : Vehicle
    {
        public void ShowCar()
        {
            Console.WriteLine("This is a Car");
        }
    }
    class ElectricCar : Car
    {
        public void ShowElectricCar()
        {
            Console.WriteLine("This is an Electric Car");
        }
    }
}
