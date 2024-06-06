using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.TestingGameOfLife
{
    [Project("Testing Game Of Life", 1630965600, 1631052000, "Minesweeper from game of life")]
    class Program : IProject
    {
        private static int SizeY;
        private static int SizeX;
        private static bool[,] Field;
        private static int Speed;
        private static readonly Random R = new Random();

        public void Run()
        {
            StartLoop();
            EditorLoop();
            PlayLoop();
        }

        private static void StartLoop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Graphics.Boxify("Give me a size (x,y,speed)", true, false));
                string[] rawSize = Console.ReadLine().Split(',');

                if (rawSize.Length != 3 || !Int32.TryParse(rawSize[0], out SizeX) || !Int32.TryParse(rawSize[1], out SizeY) || !Int32.TryParse(rawSize[2], out Speed) || SizeX > 220 || SizeY > 110)
                    continue;

                Field = new bool[SizeY, SizeX];
                for (int y = 0; y < SizeY; y++)
                    for (int x = 0; x < SizeX; x++)
                        Field[y, x] = false;
                break;
            }
        }

        private static void EditorLoop()
        {
            int cursorY = 0;
            int cursorX = 0;
            bool breakin = false;

            while (!breakin)
            {
                Console.Clear();

                string fieldPrint = "";

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        if (x == cursorX && y == cursorY)
                        {
                            if (Field[y, x])
                                fieldPrint += "1";
                            else
                                fieldPrint += "0";
                        }
                        else
                        {
                            if (Field[y, x])
                                fieldPrint += "█";
                            else
                                fieldPrint += "·";
                        }
                    }
                    fieldPrint += "\n";
                }

                Console.WriteLine(Graphics.Boxify(fieldPrint.Remove(fieldPrint.LastIndexOf('\n'), 1), false, false));

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        cursorY = --cursorY > -1 ? cursorY : 0;
                        continue;

                    case ConsoleKey.DownArrow:
                        cursorY = ++cursorY < SizeY ? cursorY : SizeY - 1;
                        continue;

                    case ConsoleKey.LeftArrow:
                        cursorX = --cursorX > -1 ? cursorX : 0;
                        continue;

                    case ConsoleKey.RightArrow:
                        cursorX = ++cursorX < SizeX ? cursorX : SizeX - 1;
                        continue;

                    case ConsoleKey.Enter:
                        Field[cursorY, cursorX] = !Field[cursorY, cursorX];
                        continue;

                    case ConsoleKey.Spacebar:
                        Field[cursorY, cursorX] = !Field[cursorY, cursorX];
                        continue;

                    case ConsoleKey.R:
                        for (int y = 0; y < SizeY; y++)
                            for (int x = 0; x < SizeX; x++)
                                Field[y, x] = R.Next(0, 2) % 2 == 0;
                        breakin = true;
                        continue;

                    case ConsoleKey.Escape:
                        breakin = true;
                        continue;

                    case ConsoleKey.E:
                        Insert(cursorY, cursorX, "0011100-0100010-1000001-1000001-1100011");
                        continue;
                }
            }
        }

        private static void PlayLoop() //WARNING: FOR LOOP NESTING
        {
            while (true)
            {
                Console.Clear();
                string printString = "";

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        if (Field[y, x])
                            printString += "█";
                        else
                            printString += "·";
                    }
                    printString += "\n";
                }

                Console.Write(Graphics.Boxify(printString.Remove(printString.LastIndexOf('\n'), 1), false, false));
                Thread.Sleep(Speed);

                bool[,] copy = new bool[SizeY, SizeX];

                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        int alivecount = 0;

                        for (int _y = y - 1; _y < y + 2; _y++)
                        {
                            if (_y < 0 || _y >= SizeY)
                                continue;

                            for (int _x = x - 1; _x < x + 2; _x++)
                            {
                                if (_x < 0 || _x >= SizeX || (_y == y && _x == x))
                                    continue;

                                if (Field[_y, _x])
                                    alivecount++;
                            }
                        }

                        if (Field[y, x])
                        {
                            if (alivecount == 2 || alivecount == 3)
                                copy[y, x] = true;
                            else
                                copy[y, x] = false;
                        }
                        else
                        {
                            if (alivecount == 3)
                                copy[y, x] = true;
                            else
                                copy[y, x] = false;
                        }
                    }
                }

                Field = copy;
            }
        }

        static void Insert(int cordY, int cordX, string text)
        {
            string[] insertText = text.Split('-');
            int indexY = 0;
            int indexX = 0;

            for (int y = cordY; y < cordY + insertText.Length; y++, indexY++)
            {
                if (y >= SizeY)
                    return;

                for (int x = cordX; x < cordX + insertText[0].Length; x++, indexX++)
                {
                    if (x >= SizeX)
                        continue;

                    Field[y, x] = insertText[indexY][indexX] == '0' ? false : true;
                }
                indexX = 0;
            }
        }
    }
}
