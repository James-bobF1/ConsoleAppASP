using System;
using System.IO;
using System.Linq;


namespace ConsoleAppAsp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
        }
        static void Task1()
        {
            string someText = Console.ReadLine();
            string filePath = @"E:\Task1.txt";
            File.WriteAllText(filePath, someText);
        }
        static void Task2()
        {
            string filePath = @"E:\Task2.txt";
            File.WriteAllText(filePath, DateTime.Now.TimeOfDay.ToString());
        }
        static void Task3()
        {
            string someText =new string(Console.ReadLine().Where(c=>char.IsDigit(c)).ToArray());
            string filePath = @"E:\Task3.bin";
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                writer.Write(someText);
            }
        }
        static void Task4()
        {
            Employee[] employees = new Employee[5];
            employees[0] =new Employee( "Ivanov Ivan", "Engineer", "ivivan@mailbox.com", "892312312", 30000, 41 );
            employees[1] = new Employee("Petrov Ivan", "Accountant", "ivivan@yandex.ru", "89231012312", 40000, 30 );
            employees[2] =new Employee( "Khlebnikov Dmitry", "Developer", "dmitry.khlebnikov@gmail.com", "8923***4074", 400000, 30);
            employees[3] =new Employee( "Ivanov Oleg", "Director", "ovivan@mailbox.com", "892312312", 120000, 50);
            employees[4] = new Employee("Evdokimov Artem", "Team Lead", "artyom.evdokimov@mail.ru", "89****92101", 600000, 30);
            foreach (Employee person in employees)
            {
                if (person.Age > 40)
                {
                    Console.WriteLine(person.ToString());

                }
            }
        }
    }
}
