using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Animal is eating");
        }
    }
    internal class Dog : Animal  
    {
        public void Bark()
        {
            Console.WriteLine("Dog is barking");
        }
    }
}
