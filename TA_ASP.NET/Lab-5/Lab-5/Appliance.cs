using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    abstract class Appliance
    {
        // Abstract method
        public abstract void TurnOn();
    }
    class Fan : Appliance
    {
        public override void TurnOn()
        {
            Console.WriteLine("Fan is turned ON");
        }
    }
    class Light : Appliance
    {
        public override void TurnOn()
        {
            Console.WriteLine("Light is turned ON");
        }
    }
}
