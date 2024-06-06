using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.ConsoleDefense
{
    static class Graphics
    {
        private static char[,] Display;
        private static Int2D Size;

        public static void Start(Int2D size)
        {
            Size = size;

            Display = new char[size.Y, size.X];

            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                    Display[y, x] = ' ';
        }
        public static void Start(int y, int x)
        {
            Start(new Int2D(y, x));
        }

        //add boxes
        public static void Draw()
        {
            string output = "";
            int lastrow = Size.Y - 1;

            for (int y = 0; y < Size.Y - 1; y++)
            {
                for (int x = 0; x < Size.X; x++)
                    output += Display[y, x];
                output += '\n';
            }

            for (int x = 0; x < Size.X; x++)
                output += Display[lastrow, x];

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(output);
        }

        public static void DrawCursor(Int2D cursor)
        {
            string firstline = "";
            string secondline = "";
            char cursorline = ' ';

            ref string currentline = ref firstline;
            int lastrow = Size.Y - 1;

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    if (x == cursor.X && y == cursor.Y)
                    {
                        cursorline = Display[y, x];
                        currentline = ref secondline;
                    }
                    else
                        currentline += Display[y, x];
                }

                if (y != lastrow)
                    currentline += '\n';
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(firstline);
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = consoleColor;
            Console.Write(cursorline);
            Console.ForegroundColor = consoleColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(secondline);
        }
        public static void DrawCursor(int y, int x)
        {
            DrawCursor(new Int2D(y, x));
        }

        public static void Clear(Int2D start, Int2D end)
        {
            Int2D startpos = start;
            Int2D endpos = end;

            for (int y = startpos.Y; y <= endpos.Y; y++)
                for (int x = startpos.X; x <= endpos.X; x++)
                    Display[y, x] = ' ';
        }
        public static void Clear(int starty, int startx, int endy, int endx)
        {
            Clear(new Int2D(starty, startx), new Int2D(endy, endx));
        }
        public static void Clear(Int2D start)
        {
            Clear(start, new Int2D(Size.Y - 1, Size.X - 1));
        }
        public static void Clear(int y, int x)
        {
            Clear(new Int2D(y, x), new Int2D(Size.Y - 1, Size.X - 1));
        }
        public static void Clear()
        {
            Clear(new Int2D(0, 0), new Int2D(Size.Y - 1, Size.X - 1));
        }

        public static void Add(string[] input, Int2D position)
        {
            int maxlength = Parsing.MaxLength(input);

            for (int y = 0; y < input.Length; y++)
            {
                if (y + position.Y >= Size.Y)
                    return;

                for (int x = 0; x < maxlength; x++)
                {
                    if (x >= input[y].Length || x + position.X >= Size.X)
                        break;

                    Display[y + position.Y, x + position.X] = input[y][x];
                }
            }
        }
        public static void Add(string[] input, int y, int x)
        {
            Add(input, new Int2D(y, x));
        }
        public static void Add(string input, Int2D position)
        {
            Add(Parsing.StringToArray(input), position);
        }
        public static void Add(string input, int y, int x)
        {
            Add(Parsing.StringToArray(input), new Int2D(y, x));
        }

        public static string[] Boxify(string[] array)
        {
            string[] output = new string[array.Length + 2];
            int maxlength = Parsing.MaxLength(array);

            int lastline = array.Length + 1;
            output[0] = "╔";
            output[lastline] = "╚";
            for (int i = 0; i < maxlength; i++)
            {
                output[0] += "═";
                output[lastline] += "═";
            }
            output[0] += "╗";
            output[lastline] += "╝";

            for (int i = 0; i < array.Length; i++)
            {
                string spacing = "";
                for (int a = 0; a < maxlength - array[i].Length; a++)
                    spacing += ' ';

                output[i + 1] = '║' + array[i] + spacing + '║';
            }

            return output;
        }
        public static string[] Boxify(string input)
        {
            return Boxify(Parsing.StringToArray(input));
        }
    }
}
