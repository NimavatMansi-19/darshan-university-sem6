using System;

namespace Lab_5
{
    abstract class Payment
    {
        public double Amount;

        public abstract void MakePayment();
    }

    class CreditCardPayment : Payment
    {
        public CreditCardPayment(double amount)
        {
            Amount = amount;
        }

        public override void MakePayment()
        {
            Console.WriteLine("Payment of " + Amount + " made using Credit Card.");
        }
    }

    class UPIPayment : Payment
    {
        public UPIPayment(double amount)
        {
            Amount = amount;
        }

        public override void MakePayment()
        {
            Console.WriteLine("Payment of " + Amount + " made using UPI.");
        }
    }
}
