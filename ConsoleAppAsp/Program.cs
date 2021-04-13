using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace ConsoleAppAsp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetFullName("Ivan", "Ivanov", "Ilich"));
            Console.WriteLine(GetFullName(firstName: "Petr", lastName: "Ivanov", patronymic: "Ilich"));
            Console.WriteLine(GetFullName("Semen", "Ivanov", "Ilich"));

            Console.WriteLine("Введите числа через пробелы");
            Console.WriteLine("Sum= " + SumNumbers(Console.ReadLine()));

            Console.WriteLine("Введите число от 1 до 12");
            repeat: int mounth = Int32.Parse(Console.ReadLine());
            switch (mounth)
            {
                case 1:
                case 2:
                case 12:
                    {
                        Console.WriteLine("Текущий сезон " + GetDescription(Season.Winter));
                        break;
                    }
                case 3:
                case 4:
                case 5:
                    {
                        Console.WriteLine("Текущий сезон " + GetDescription(Season.Spring));
                        break;
                    }
                case 6:
                case 7:
                case 8:
                    {
                        Console.WriteLine("Текущий сезон " + GetDescription(Season.Summer));
                        break;
                    }
                case 9:
                case 10:
                case 11:
                    {
                        Console.WriteLine("Текущий сезон " + GetDescription(Season.Autumn));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Ошибка: введите число от 1 до 12");
                        goto repeat;
                    }
            }

            Console.WriteLine("Введите порядковый номер числа Фибоначчи");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine($"{n}-е число Фибоначчи {Fibonachi(n)}");

            String str1 = " Предложение один Теперь предложение два Предложение три";
            char[] char1 = new char[str1.Length * 3];
            int firstIndex = 0;
            int secondIndex = 0;
            while (firstIndex < str1.Length)
            {
                if (str1[firstIndex] != Char.ToLower(str1[firstIndex]))
                {
                    int j = secondIndex;
                    while (j-- > 0)
                    {
                        if (char1[j] != ' ')
                        {
                            for (int k = secondIndex++; k > j; k--)
                            {
                                char1[k] = char1[k - 1];
                            }
                            char1[j+1] = '.';
                            break;
                        }
                    }
                }
                char1[secondIndex++] = str1[firstIndex++];
            }
            string str2 = new string(char1, 0,secondIndex);
            Console.WriteLine(str2);
        }
        static string GetFullName(string firstName, string lastName, string patronymic)
        {
            return ($"{firstName} {patronymic} {lastName}");
        }

        static Func<string, int> SumNumbers = input => input.Split(' ').Select(Int32.Parse).Sum();


        enum Season
        {
            [Description("Весна")]
            Spring,
            [Description("Лето")]
            Summer,
            [Description("Осень")]
            Autumn,
            [Description("Зима")]
            Winter
        }
        static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();
            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }

        static int Fibonachi(int number)
        {
            if (number < 2)
                return number;
            else
                return Fibonachi(number - 2) + Fibonachi(number - 1);
        }
    }
}
