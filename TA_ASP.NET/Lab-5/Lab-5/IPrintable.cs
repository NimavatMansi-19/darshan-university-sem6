using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    interface IPrintable
    {
        void PrintDetails();
    }
    class Book : IPrintable
    {
        public string Title;
        public string Author;

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
        public void PrintDetails()
        {
            Console.WriteLine("Book Details:");
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
        }
    }
    class Magazine : IPrintable
    {
        public string Name;
        public int IssueNumber;

        public Magazine(string name, int issue)
        {
            Name = name;
            IssueNumber = issue;
        }

        public void PrintDetails()
        {
            Console.WriteLine("Magazine Details:");
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Issue No: " + IssueNumber);
        }
    }
}
