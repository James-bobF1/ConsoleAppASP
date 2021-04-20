using System;
using System.Diagnostics;

namespace ConsoleAppAsp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowTaskManager();
            Random rnd = new Random();
            string[][] arr = new string[rnd.Next(10) == 2 ? rnd.Next(10):4][];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new string[rnd.Next(10) == 2 ? rnd.Next(10) : 4];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = rnd.Next(10) != 2 ? rnd.Next(50).ToString() : new string((Char)rnd.Next(SByte.MaxValue),1);
                }
            }
            try
            {
                Console.WriteLine($"Сумма элементов равна {ArraySum(arr)}"); 
            }
            catch (MyArraySizeException ex)
            {
                if (ex.Dimentions == -1)
                {
                    Console.WriteLine("Размер основного массива не равен 4");
                }
                else
                {
                    Console.WriteLine($"Размер подмассива {ex.Dimentions} массива не равен 4");
                }
            }
            catch (MyArrayDataException ex)
            {
                Console.WriteLine($"Не удалось преобразовать значение в ячейке arr[{ex.Xcoord}, {ex.Ycoord}]='{arr[ex.Xcoord][ex.Ycoord]}' в число");
            }
        }
        
        static void ShowTaskManager()
        {
            Process[] processes = Process.GetProcesses();
            Console.WriteLine("Id             ProcessName                                       PagedMemorySize64(Mb)");
            Console.WriteLine(new string('=',Console.BufferWidth));
            for (int i = 0; i < processes.Length; i++)
            {
                Console.WriteLine($"{processes[i].Id,-15}{processes[i].ProcessName,-50}{processes[i].PagedMemorySize64/1024}");
            }
            Console.WriteLine(new string('=', Console.BufferWidth));
            Console.WriteLine("Input id or name for kill");
            string killProc = Console.ReadLine();
            int procId;
            if (!int.TryParse(killProc, out procId))
            {
                procId = -1;
            }          
            bool find = false;
            for (int i = 0; i < processes.Length; i++)
            {
                if(procId!=-1&&processes[i].Id== procId)
                {
                    find = true;
                    processes[i].Kill();
                    Console.WriteLine($"Process {processes[i].ProcessName} killed successful");
                    break;
                }
                else if(processes[i].ProcessName== killProc)
                {
                    find = true;
                    processes[i].Kill();
                    Console.WriteLine($"Process {killProc} killed successful");
                    break;
                }
            }
            if (!find)
            {
                Console.WriteLine($"Process {killProc} not find");
            }
        }

        static int ArraySum(string[][] arr)
        {
            int lenght = arr.GetLength(0);
            int summ=0;
            if (lenght != 4)
            {
                throw new MyArraySizeException(-1);
            }
            for (int i = 0; i < lenght; i++)
            {
                if (arr[i].GetLength(0) !=4)
                {
                    throw new MyArraySizeException(i);
                }
                for (int j = 0; j < arr[i].GetLength(0); j++)
                {                    
                    int convert;
                    if (!int.TryParse(arr[i][j],out convert))
                    {
                        throw new MyArrayDataException(i, j);
                    }
                    summ += convert;
                }
            }
            return summ;
        }
    }
    class MyArraySizeException : Exception
    {
        public int Dimentions { get;}
        public MyArraySizeException(int dimentions)
        {
            Dimentions = dimentions;
        }

    }
    class MyArrayDataException : Exception
    {
        public int Xcoord { get; }
        public int Ycoord { get; }
        public MyArrayDataException(int x,int y)
        {
            Xcoord = x;
            Ycoord = y;
        }
    }
}
