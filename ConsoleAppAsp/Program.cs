//using System;
//Если мы не используем пространство имен, нам необходимо явно обращаться к его членам.

namespace ConsoleAppAsp
{
    class Program
    {
        static void Main(string[] args)
        {;
            System.Console.WriteLine("What is your name?");
            string name = System.Console.ReadLine();
            System.Console.WriteLine($"Привет, {name}, сегодня {System.DateTime.Now}");
            System.Console.ReadKey();
        }
        
    }
}
