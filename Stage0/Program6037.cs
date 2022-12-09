
//using System;

namespace Stage0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome6037();
            Welcome3889();
            Console.ReadKey();
        }
        static partial void Welcome3889();
        private static void Welcome6037()
        {
            string? sen2;
            Console.Write("Enter your name: ");
            sen2 = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", sen2);
        }
    }
}


