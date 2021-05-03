using System;

namespace ConsoleAppAsp
{
    class Cross
    {
        static int SIZE_X = 5;
        static int SIZE_Y = 5;
        static int Win_Size = 4;
        static int countFields = SIZE_X * SIZE_Y;

        static char[,] field = new char[SIZE_Y, SIZE_X];
        static int[,] playerFields = new int[SIZE_Y, SIZE_X];
        static int[,] AIFields = new int[SIZE_Y, SIZE_X];

        static char PLAYER_DOT = 'X';
        const int PLAYER = -1;
        static char AI_DOT = 'O';
        const int AI = -2;
        static readonly char EMPTY_DOT = '.';
        static int moves = 0;

        static Random random = new Random();

        private static void InitField()
        {
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    field[i, j] = EMPTY_DOT;
                    playerFields[i, j] = Field_Hash(i, j, 0, ref playerFields);
                    AIFields[i, j] = Field_Hash(i, j, 0, ref AIFields);
                }
            }
        }

        private static void PrintField(int y, int x)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 3 * SIZE_X));
            for (int i = 0; i < SIZE_Y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < SIZE_X; j++)
                {
                    if (i == y && j == x)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(field[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write(field[i, j] + "|");
                    }
                }/*
                Console.Write("\t");
                for (int j = 0; j < SIZE_X; j++)
                {
                    if (playerFields[i, j] + AIFields[i, j]>0)
                    {
                        int tmp = playerFields[i, j] + AIFields[i, j];
                        Console.Write((tmp).ToString("000"));
                        Console.Write("|");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" "+(playerFields[i, j]==AI?AI_DOT:PLAYER_DOT) + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }
                }*/
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 3 * SIZE_X));
        }

        private static void SetSym(int y, int x, bool playerMove)
        {
            field[y, x] = playerMove ? PLAYER_DOT : AI_DOT;
            playerFields[y, x] = AIFields[y, x] = playerMove ? PLAYER : AI;
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    if (playerFields[i, j] > 0)
                    {
                        playerFields[i, j] = Field_Hash(i, j, PLAYER, ref playerFields);
                        AIFields[i, j] = Field_Hash(i, j, AI, ref AIFields);
                    }
                }
            }
            moves++;
        }

        private static int Field_Hash(int y, int x, int player, ref int[,] arr)
        {
            int result = 0;
            if (Math.Min(SIZE_Y - 1 - y, SIZE_X - 1 - x) + Math.Min(y + 1, x + 1) >= Win_Size)//check diagonal
            {
                result += LineCount(y - Math.Min(Win_Size - 1, Math.Min(y, x)), x - Math.Min(Win_Size - 1, Math.Min(y, x)), y + Math.Min(Win_Size - 1, Math.Min(SIZE_Y - 1 - y, SIZE_X - 1 - x)), x + Math.Min(Win_Size - 1, Math.Min(SIZE_Y - 1 - y, SIZE_X - 1 - x)), player, ref arr);
            }
            if (Math.Min(y, SIZE_X - 1 - x) + Math.Min(SIZE_Y - y, x + 1) >= Win_Size)//check reverse_diagonal
            {
                result += LineCount(y + Math.Min(Win_Size - 1, Math.Min(SIZE_Y - 1 - y, x)), x - Math.Min(Win_Size - 1, Math.Min(SIZE_Y - 1 - y, x)), y - Math.Min(Win_Size - 1, Math.Min(SIZE_X - 1 - x, y)), x + Math.Min(Win_Size - 1, Math.Min(SIZE_X - 1 - x, y)), player, ref arr);
            }
            if (SIZE_X >= Win_Size)//check horizontal
            {
                result += LineCount(y, Math.Max(0, x + 1 - Win_Size), y, Math.Min(x + Win_Size - 1, SIZE_X - 1), player, ref arr);
            }
            if (SIZE_Y >= Win_Size)//check Vertikal
            {
                result += LineCount(Math.Max(0, y + 1 - Win_Size), x, Math.Min(y + Win_Size - 1, SIZE_Y - 1), x, player, ref arr);
            }
            return result;
        }

        private static int LineCount(int y_left, int x_left, int y_right, int x_right, int player, ref int[,] arr)
        {
            int range = 0;
            int repeater = 0;
            int y_step;
            int y_coord = y_left;
            if (y_right > y_left)
            {
                y_step = 1;
            }
            else if (y_right == y_left)
            {
                y_step = 0;
            }
            else
            {
                y_step = -1;
            }
            int x_coord = x_left;
            int x_step = x_right > x_left ? 1 : 0;
            while (y_coord >= Math.Min(y_left, y_right) && y_coord <= Math.Max(y_left, y_right) && x_coord <= x_right)
            {
                if (arr[y_coord, x_coord] == player || arr[y_coord, x_coord] > 0)
                {
                    range++;
                    if (arr[y_coord, x_coord] == player)
                    {
                        repeater += 10;
                    }
                }
                else
                {
                    range = repeater = 0;
                }
                y_coord += y_step;
                x_coord += x_step;
            }
            if (range >= Win_Size)
            {
                return range - Win_Size + 1 + repeater;
            }
            return 0;
        }

        private static void FindEmpty(bool horizontal, bool increment, ref int x, ref int y)
        {
            int i = y, j = x;
            while (true)
            {
                if (horizontal)
                {
                    if (increment)
                    {
                        i++;
                        if (i > SIZE_Y - 1)
                        {
                            i = 0;
                            j++;
                            if (j > SIZE_X - 1)
                            {
                                j = 0;
                            }
                        }
                    }
                    else
                    {
                        i--;
                        if (i < 0)
                        {
                            i = SIZE_Y - 1;
                            j--;
                            if (j < 0)
                            {
                                j = SIZE_X - 1;
                            }
                        }
                    }
                }
                else
                {
                    if (increment)
                    {
                        j++;
                        if (j > SIZE_X - 1)
                        {
                            j = 0;
                            i++;
                            if (i > SIZE_Y - 1)
                            {
                                i = 0;
                            }
                        }
                    }
                    else
                    {
                        j--;
                        if (j < 0)
                        {
                            j = SIZE_X - 1;
                            i--;
                            if (i < 0)
                            {
                                i = SIZE_Y - 1;
                            }
                        }
                    }
                }
                if (playerFields[i, j] >= 0)
                {
                    x = j;
                    y = i;
                    break;
                }
            }
        }

        private static void playerMove(ref int y_coord, ref int x_coord)
        {
            int x = 0, y = -1;
            bool brek = true;
            FindEmpty(true, true, ref x, ref y);
            Console.WriteLine("Ваш ход на поле");
            Console.WriteLine("Передвигайте стрелочками, выбирайте пробелом");
            Console.SetCursorPosition(2 * x + 1, y + 1);
            while (brek)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo button = Console.ReadKey();
                    switch (button.Key)
                    {
                        case ConsoleKey.Spacebar:
                            x = (Console.CursorLeft - 1) / 2;
                            y = (Console.CursorTop - 1);
                            brek = false;
                            break;
                        case ConsoleKey.LeftArrow:
                            FindEmpty(false, false, ref x, ref y);
                            break;
                        case ConsoleKey.RightArrow:
                            FindEmpty(false, true, ref x, ref y);
                            break;
                        case ConsoleKey.DownArrow:
                            FindEmpty(true, true, ref x, ref y);
                            break;
                        case ConsoleKey.UpArrow:
                            FindEmpty(true, false, ref x, ref y);
                            break;
                    }
                    PrintField(y_coord, x_coord);
                    Console.WriteLine("Ваш ход на поле");
                    Console.WriteLine("Передвигайте стрелочками, выбирайте пробелом");
                    Console.SetCursorPosition(2 * x + 1, y + 1);
                }
            }
            SetSym(y, x, true);
            x_coord = x;
            y_coord = y;
        }

        private static void AiMove(out int y_coord, out int x_coord)
        {
            Console.ReadKey();
            int[,] maxValues = new int[2, SIZE_X * SIZE_Y];
            int max = 0, count = 0, tmp;
            for (int i = 0; i < SIZE_Y; i++)
            {
                for (int j = 0; j < SIZE_X; j++)
                {
                    if (playerFields[i, j] > AIFields[i, j])
                    {
                        tmp = playerFields[i, j] + AIFields[i, j];
                    }
                    else
                    {
                        tmp = playerFields[i, j] + AIFields[i, j];
                    }
                    if (tmp >= max && playerFields[i, j] >= 0 && AIFields[i, j] >= 0)//
                    {
                        if (tmp > max)
                        {
                            count = 0;
                            max = tmp;
                        }
                        maxValues[0, count] = i;
                        maxValues[1, count++] = j;
                    }
                }
            }
            tmp = random.Next(count);
            y_coord = maxValues[0, tmp];
            x_coord = maxValues[1, tmp];
            SetSym(y_coord, x_coord, false);
        }

        private static bool CheckWin(int y, int x)
        {
            int line_Lenght = 1;
            int y_step = -1, x_step = -1, y_coord = y + y_step, x_coord = x + x_step;

            while (y_step < 1)
            {
                while (y_coord >= 0 && y_coord < SIZE_Y && x_coord >= 0 && x_coord < SIZE_X && field[y_coord, x_coord] == field[y, x])
                {
                    line_Lenght++;
                    y_coord += y_step;
                    x_coord += x_step;
                }
                x_step = -x_step;
                y_step = -y_step;
                y_coord = y + y_step;
                x_coord = x + x_step;
                while (y_coord >= 0 && y_coord < SIZE_Y && x_coord >= 0 && x_coord < SIZE_X && field[y_coord, x_coord] == field[y, x])
                {
                    line_Lenght++;
                    y_coord += y_step;
                    x_coord += x_step;
                }
                if (line_Lenght >= Win_Size) return true;

                line_Lenght = 1;
                if (x_step > -1)
                {
                    x_step = 1 - x_step;
                    y_step = -1;
                }
                else
                {
                    y_step = 1 - y_step;
                    x_step = 1;
                }
                y_coord = y + y_step;
                x_coord = x + x_step;
            }
            return false;
        }

        static void Main(string[] args)
        {
            int x = -1, y = -1;
            bool first = random.Next(2)==1?true:false;
            InitField();
            PrintField(y, x);
            do
            {
                if (first)
                {
                    playerMove(ref y, ref x);
                    PrintField(y, x);
                    if (CheckWin(y, x))
                    {
                        Console.WriteLine("Вы выиграли");
                        break;
                    }
                    else if (moves >= countFields) break;
                }
                first = true;
                Console.WriteLine("Ход Компа на поле");
                AiMove(out y, out x);
                PrintField(y, x);
                if (CheckWin(y, x))
                {
                    Console.WriteLine("Выиграли Комп");
                    break;
                }
                else if (moves >= countFields) break;
            } while (true);
            Console.WriteLine("!Конец игры!");
        }
    }
}
