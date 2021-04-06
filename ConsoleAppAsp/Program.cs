using System;

namespace ConsoleAppAsp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1диаганальный вывод массива
            Random rnd = new Random();
            int[,] array = new int[5, 5];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rnd.Next(10);
                }
            }
            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(i == j ? array[i, j].ToString() : " ");
                }
                Console.WriteLine();
            }
            #endregion

            #region 2phone directory
            string[,] phoneDirectory = { { "Ivanov", "Petrov", "Sidorov", "Vetrov", "Soroka" }, { "Ivanov@mail.ru", "Petrov@mail.ru", "Sidorov@mail.ru", "Vetrov@gmail.com", "Soroka@gmail.com" } };
            for (int i = 0; i < phoneDirectory.GetLength(0); i++)
            {
                for (int j = 0; j < phoneDirectory.GetLength(1); j++)
                {
                    Console.Write(phoneDirectory[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("input contact");
            string searchStr = Console.ReadLine();
            bool find = false;
            for (int j = 0; j < phoneDirectory.GetLength(1); j++)
            {
                if (phoneDirectory[0, j] == searchStr)
                {
                    find = true;
                    Console.WriteLine($"{phoneDirectory[0, j]} {phoneDirectory[1, j]}");
                    break;
                }
            }
            if (!find)
            {
                Console.WriteLine("Contact not find");
            }

            #endregion

            #region 3mirror string
            Console.WriteLine("Input text");
            string input = Console.ReadLine();
            for (int i = input.Length - 1; i >= 0; i--)
            {
                Console.Write(input[i]);
            }
            Console.WriteLine();
            #endregion

            #region 4Sea Battle
            char[,] Sea = new char[10, 10];
            for (int i = 0; i < Sea.GetLength(1); i++)
            {
                for (int j = 0; j < Sea.GetLength(1); j++)
                {
                    Sea[i, j] = 'O';
                }
            }
            int tail = 0;
            char[] DoubleDeckShip = { 'X', 'X' };
            char[] TripleDeckShip = { 'X', 'X', 'X' };
            char[] FourDeckShip = { 'X', 'X', 'X', 'X' };

            foreach (char X in FourDeckShip)
            {
                Sea[tail / Sea.GetLength(1), tail++ % Sea.GetLength(1)] = X;
            }
            tail++;
            for (int i = 1; i <= 2; i++)
            {
                if (tail + TripleDeckShip.GetLength(0) > Sea.GetLength(1))
                {
                    tail = 20 + (tail / 10) * 10;
                }
                foreach (char X in TripleDeckShip)
                {
                    Sea[tail / Sea.GetLength(1), tail++ % Sea.GetLength(1)] = X;
                }
            }
            tail++;
            for (int i = 1; i <= 3; i++)
            {
                if (tail + DoubleDeckShip.GetLength(0) > Sea.GetLength(1))
                {
                    tail = 20 + (tail / 10) * 10;
                }
                foreach (char X in DoubleDeckShip)
                {
                    Sea[tail / Sea.GetLength(1), tail++ % Sea.GetLength(1)] = X;
                }
            }
            int count = 4;
            Random rand = new Random();
            while (count > 0)
            {
                int i = rand.Next(Sea.GetLength(1));
                int j = rand.Next(Sea.GetLength(1));
                bool hasX = false;
                if (Sea[i, j] == 'O')
                {
                    for (int k = -1; k < 1; k++)
                    {
                        for (int l = -1; l < 1; l++)
                        {
                            if (i + k < 0 || i + k > Sea.GetLength(1) || j + l < 0 || j + l > Sea.GetLength(1))
                            {
                                continue;
                            }
                            if (Sea[i + k, j + l] == 'X')
                            {
                                hasX = true;
                                break;
                            }
                        }
                        if (hasX)
                        {
                            break;
                        }
                    }
                    if (!hasX)
                    {
                        count--;
                        Sea[i, j] = 'X';
                    }
                }
            }
            for (int i = 0; i < Sea.GetLength(1); i++)
            {
                for (int j = 0; j < Sea.GetLength(1); j++)
                {
                    Console.Write(Sea[i, j] + " ");
                }
                Console.WriteLine();
            }

            #endregion
            #region 5shiftArray
            int[] arr = new int[100];
            Random randomm = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = randomm.Next(100);
            }
            Console.WriteLine("input n");
            int shift;
            while (!int.TryParse(Console.ReadLine(), out shift))
            {
                Console.WriteLine("Cannot convert to int");
            }
            Console.WriteLine("start array:");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            if (Math.Abs(shift) >= arr.Length)
            {
                shift = shift % arr.Length;
            }
            if (shift > 0)
            {
                for (int i = 0; i < shift; i++)
                {
                    int tmp = arr[arr.Length - 1];
                    for (int j = arr.Length - 1; j > 0; j--)
                    {
                        arr[j] = arr[j - 1];
                    }
                    arr[0] = tmp;
                }
            }
            else if (shift < 0)
            {
                for (int i = shift; i < 0; i++)
                {
                    int tmp = arr[0];
                    for (int j = 1; j < arr.Length; j++)
                    {
                        arr[j - 1] = arr[j];
                    }
                    arr[arr.Length - 1] = tmp;
                }
            }
            Console.WriteLine("finish array:");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            #endregion
        }
    }

}
