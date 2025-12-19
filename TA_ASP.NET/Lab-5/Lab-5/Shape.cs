using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class Shape
    {
        // Virtual method
        public virtual double Area()
        {
            return 0;
        }
    }
    class Circle : Shape
    {
        public double Radius;

        public Circle(double r)
        {
            Radius = r;
        }

        // Override Area()
        public override double Area()
        {
            return 3.14 * Radius * Radius;
        }
    }
    class Rectangle : Shape
    {
        public double Length;
        public double Breadth;

        public Rectangle(double l, double b)
        {
            Length = l;
            Breadth = b;
        }

        // Override Area()
        public override double Area()
        {
            return Length * Breadth;
        }
    }
}
