using System;
using System.Globalization;

namespace ConsoleAppAsp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Task_1 temperature
            string input;
            double minDailyTemperature;
            double maxDailyTemperature;
            Console.WriteLine("Input minimal daily temperature");
            input = Console.ReadLine();
            if (!Double.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out minDailyTemperature))
            {
                Console.WriteLine($"Cannot convert {input} to double");
                return;
            }
            Console.WriteLine("Input maximal daily temperature");
            input = Console.ReadLine();
            if (!Double.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out maxDailyTemperature))
            {
                Console.WriteLine($"Cannot convert {input} to double");
                return;
            }
            double averageTemperature = (minDailyTemperature + maxDailyTemperature) / 2;
            Console.WriteLine($"average daily temperature  {averageTemperature.ToString("{0.##}")}");
            #endregion

            #region Task_2 month
            DateTime DateOfMonth;
            Console.WriteLine("Input current month");
            input = Console.ReadLine();
            try
            {
                DateOfMonth = new DateTime(DateTime.Now.Year, int.Parse(input), DateTime.Now.Day);
                Console.WriteLine($"Current month is {DateOfMonth.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-EN"))}");
            }
            catch
            {
                Console.WriteLine($"Cannot convert {input} to month");
                return;
            }
            #endregion

            #region Task_3 even number
            Console.WriteLine("Input integer");
            input = Console.ReadLine();
            int someInt;
            if(!int.TryParse(input,out someInt))
            {
                Console.WriteLine($"Cannot convert {input} to integer");
                return;
            }
            Console.WriteLine(someInt % 2 == 0 ? $"{input} is even" : $"{input} is not even");
            #endregion

            #region Task_4 receipt
            try
            {
                Console.WriteLine("Input company name");
                string companyname = Console.ReadLine();
                const string KKM = "00075411";//номер фискального регистратора
                int receiptNumper = new Random().Next(10000);// по идее конечно храним значение и увеличиваем при каждой печати
                Console.WriteLine("Input ИНН");
                input = Console.ReadLine();
                long Inn = long.Parse(input);
                const uint EKLZ = 3851495566;//номер "черного ящика" фискального регистратора
                string product1 = "Пельмени сытные";
                double product1Price = 900;
                string product2 = "Вареники творожные";
                double product2Price = 700;
                double total = product1Price + product2Price;
                string verificationCode="00003751# 059705";
                string receipt = @$"{companyname}
Добро пожаловать
ККМ {KKM}     #{receiptNumper}
ИНН {Inn}
      ЭКЛЗ {EKLZ}
{DateTime.Now.ToString("dd.MM.yy HH:mm")} СИС.
 АДМИ
НАИМЕНОВАНИЕ ТОВАРА
  {product1} = {product1Price:N2}
НАИМЕНОВАНИЕ ТОВАРА
  {product2} = {product2Price:N2}


ИТОГ
     {total:N2}
НАЛИЧНЫМИ = 1600.00
* **********************
       {verificationCode}

";
                Console.WriteLine(receipt);
            }
            catch
            {
                Console.WriteLine($"{input} is not valid text");
            }
            
            #endregion
            
            #region Task_5 even number
            if (averageTemperature>0&&(DateOfMonth.Month==1|| DateOfMonth.Month >= 11))
            {
                Console.WriteLine("Дождливая зима");
            }
            #endregion

            #region Task_6 office schedule
            Days office1schedule = Days.Tuesday | Days.Wednesday | Days.Thursday | Days.Friday;
            Days office2schedule = Days.Friday | Days.Monday | Days.Saturday | Days.Sunday | Days.Thursday | Days.Tuesday | Days.Wednesday;
            Console.WriteLine($"office 1 schedule {office1schedule}");
            Console.WriteLine($"office 2 schedule {office2schedule}");
            #endregion
        }

        [Flags]
        public enum Days
        {
            Monday = 0b_0000_0001,  // 1
            Tuesday = 0b_0000_0010,  // 2
            Wednesday = 0b_0000_0100,  // 4
            Thursday = 0b_0000_1000,  // 8
            Friday = 0b_0001_0000,  // 16
            Saturday = 0b_0010_0000,  // 32
            Sunday = 0b_0100_0000,  // 64
        }
    }
}
