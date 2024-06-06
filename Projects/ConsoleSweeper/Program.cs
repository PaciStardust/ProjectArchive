namespace ProjectArchive.Projects.ConsoleSweeper
{
    [Project("Console Sweeper", 1631138400, 1632088800, "A minesweeper game in console")]
    internal class Program : IProject
    {
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Graphics.Boxify(Graphics.Unify("CONSOLESWEEPER", false, 0), false, false));
                Console.WriteLine(Graphics.Boxify("- GAME MODES -\n\n1 - Standard\n2 - Custom\n3 - Tower\n4 - Help", true, false));
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        MenuStandard();
                        continue;

                    case ConsoleKey.D2:
                        MenuCustom();
                        continue;

                    case ConsoleKey.D3:
                        MenuTower();
                        continue;

                    case ConsoleKey.D4:
                        MenuHelp();
                        continue;
                }
            }
        }

        static void MenuStandard()
        {
            string[] sizenames = new string[] { "TINY", "SMALL", "MEDIUM", "LARGE", "MEGA", "GIGA", "ULTRA", "ULTRA+", "ULTRA++", "MAXED" };
            int[] sizesX = new int[] { 10, 16, 16, 24, 32, 48, 64, 96, 160, 220 };
            int[] sizesY = new int[] { 10, 10, 16, 20, 24, 32, 32, 64, 96, 110 };

            string[] difficultynames = new string[] { "WOOD", "STONE", "COPPER", "IRON", "BRONZE", "SILVER", "GOLD", "PLATINUM", "CRYSTAL", "DIAMOND", "DIAMOND+", "DIAMOND++", "HELLSTONE" };
            int[] difficulty = new int[] { 10, 15, 20, 24, 28, 32, 36, 40, 43, 46, 50, 55, 60 };

            int cursorY = 0;
            int sizeval = 0;
            int diffval = 0;

            bool goBack = false;

            while (true)
            {
                Console.Clear();

                string[] linetext = new string[] { ">", " " };
                if (cursorY != 0)
                    linetext = new string[] { " ", ">" };

                Console.WriteLine(Graphics.Boxify($"     - DEFAULT FIELD -     \n\nPlease select a difficulty:\n\n{linetext[0]}Size: {sizenames[sizeval]} ({sizesX[sizeval]}x{sizesY[sizeval]})\n{linetext[1]}Mines: {difficultynames[diffval]} ({difficulty[diffval]}%)", true, false));

                ConsoleKey input = Console.ReadKey().Key;

                if (input == ConsoleKey.Enter || input == ConsoleKey.Spacebar)
                    break;
                else if (input == ConsoleKey.Escape)
                {
                    goBack = true;
                    break;
                }
                else if (input == ConsoleKey.W || input == ConsoleKey.UpArrow || input == ConsoleKey.S || input == ConsoleKey.DownArrow)
                    cursorY = cursorY == 1 ? 0 : 1;
                else if (input == ConsoleKey.D || input == ConsoleKey.RightArrow)
                {
                    if (cursorY == 0)
                        sizeval = Math.Min(++sizeval, sizenames.Length - 1);
                    else
                        diffval = Math.Min(++diffval, difficultynames.Length - 1);
                }
                else if (input == ConsoleKey.A || input == ConsoleKey.LeftArrow)
                {
                    if (cursorY == 0)
                        sizeval = Math.Max(--sizeval, 0);
                    else
                        diffval = Math.Max(--diffval, 0);
                }
            }

            if (goBack)
                return;

            while (true)
            {
                Minefield field = new Minefield(sizenames[sizeval] + " " + difficultynames[diffval], sizesX[sizeval], sizesY[sizeval], difficulty[diffval]);

                if (field.Play())
                    return;
                else
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(Graphics.Boxify("Do you want to retry? (Y/N)", true, false));

                        ConsoleKey input = Console.ReadKey().Key;

                        if (input == ConsoleKey.Y)
                            break;
                        else if (input == ConsoleKey.N || input == ConsoleKey.Escape)
                            return;
                    }
                }
            }
        }

        static void MenuCustom()
        {
            int sizeX;
            int sizeY;
            int difficulty;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(Graphics.Boxify("         - CUSTOM FIELD -         \nPick a size and mine ratio (x,y,%)", true, false));
                string[] rawInput = Console.ReadLine().Split(',');

                if (rawInput.Length == 0)
                    return;
                else if (rawInput.Length != 3 || !Int32.TryParse(rawInput[0], out sizeX) || !Int32.TryParse(rawInput[1], out sizeY) || !Int32.TryParse(rawInput[2], out difficulty) || sizeX > 220 || sizeY > 110 || difficulty > 97)
                    continue;
                break;
            }

            while (true)
            {
                Minefield field = new Minefield($"CUSTOM {sizeX}x{sizeY} {difficulty}%", sizeX, sizeY, difficulty);

                if (field.Play())
                    return;
                else
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(Graphics.Boxify("Do you want to retry? (Y/N)", true, false));

                        ConsoleKey input = Console.ReadKey().Key;

                        if (input == ConsoleKey.Y)
                            break;
                        else if (input == ConsoleKey.N || input == ConsoleKey.Escape)
                            return;
                    }
                }
            }
        }

        static void MenuTower()
        {
            int currentLevel = 0;
            bool gameOver = false;

            while (true)
            {
                Console.Clear();

                int difficulty = 12 + currentLevel / 2 * 2;
                int sizeX = 8 + (currentLevel + 1) / 2 * 2;
                int sizeY = 8 + (currentLevel + 1) / 2 * 1;

                Console.WriteLine(Graphics.Boxify($"  - THE TOWER -  \n\nFloor: {currentLevel + 1}\n\nSize: {sizeX}x{sizeY}\nMines: {difficulty}%\n\nContinue? (Y/N)", true, false));

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        Minefield field = new Minefield($"TOWER FLOOR {currentLevel + 1}", sizeX, sizeY, difficulty);
                        if (!field.Play())
                            gameOver = true;
                        else
                            currentLevel++;
                        break;

                    case ConsoleKey.N:
                        gameOver = true;
                        break;

                    case ConsoleKey.Escape:
                        gameOver = true;
                        break;
                }

                if (currentLevel > 80 && !gameOver)
                {
                    Minefield field = new Minefield($"TOWER FINAL FLOOR", 220, 110, difficulty);
                    if (!field.Play())
                        gameOver = true;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(Graphics.Boxify($" - THE TOWER - \n\nYOU'VE WON!\n\nYou reached:\nFloor MAX\n\nSize: 220x110\nMines: {difficulty}%", true, false));
                        Thread.Sleep(2000);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    }
                }

                if (gameOver)
                {
                    Console.Clear();
                    Console.WriteLine(Graphics.Boxify($" - THE TOWER - \n\nGAME OVER!\n\nYou reached:\nFloor {currentLevel + 1}\n\nSize: {sizeX}x{sizeY}\nMines: {difficulty}%", true, false));
                    Thread.Sleep(2000);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }
            }
        }

        static void MenuHelp()
        {
            Console.Clear();
            Console.WriteLine(Graphics.Boxify("    - HELP -    \n\nMovement:\n WASD/Arrows\nFlag:\n Q/E/F\nReveal:\n Space/Enter\nReset:\n R\nExit:\n Escape\n\nHint:\n Corners = Safe", true, false));
            Console.ReadKey();
        }
    }

    static class Graphics
    {
        private static readonly Dictionary<char, string> UniDict = new Dictionary<char, string>
        {
            {'a', "11111-10001-11111-10001-10001" },
            {'b', "11110-10001-11110-10001-11110" },
            {'c', "01111-10000-10000-10000-01111" },
            {'d', "11110-10001-10001-10001-11110" },
            {'e', "11111-10000-11100-10000-11111" },
            {'f', "11111-10000-11100-10000-10000" },
            {'g', "11111-10000-10011-10001-11111" },
            {'h', "10001-10001-11111-10001-10001" },
            {'i', "11111-00100-00100-00100-11111" },
            {'j', "11111-00001-00001-10001-01110" },
            {'k', "10001-10010-11100-10010-10001" },
            {'l', "10000-10000-10000-10000-11111" },
            {'m', "10001-11011-10101-10001-10001" },
            {'n', "10001-11001-10101-10011-10001" },
            {'o', "01110-10001-10001-10001-01110" },
            {'p', "11111-10001-11111-10000-10000" },
            {'q', "01110-10001-10001-10011-01111" },
            {'r', "11111-10001-11111-10010-10001" },
            {'s', "11111-10000-11111-00001-11111" },
            {'t', "11111-00100-00100-00100-00100" },
            {'u', "10001-10001-10001-10001-01110" },
            {'v', "10001-10001-10001-01010-00100" },
            {'w', "10001-10001-10101-11011-10001" },
            {'x', "10001-01010-00100-01010-10001" },
            {'y', "10001-01010-00100-00100-00100" },
            {'z', "11111-00010-00100-01000-11111" },
            {'0', "01110-11001-10101-10011-01110" },
            {'1', "00011-00101-01001-10001-00001" },
            {'2', "01110-10001-00110-01000-11111" },
            {'3', "11110-00001-00110-00001-11110" },
            {'4', "10001-10001-11111-00001-00001" },
            {'5', "11111-01000-00110-10001-01110" },
            {'6', "01110-10000-11110-10001-01110" },
            {'7', "11111-00001-00010-00010-00010" },
            {'8', "01110-10001-01110-10001-01110" },
            {'9', "01110-10001-01111-00001-01110" },
            {'!', "00100-00100-00100-00000-00100" },
            {'?', "01110-00010-00100-00000-00100" },
            {':', "00000-00100-00000-00100-00000" },
            {'+', "00000-00100-01110-00100-00000" },
            {'-', "00000-00000-01110-00000-00000" },
            {'/', "00010-00100-00100-00100-01000" },
            {'\'', "01000-00100-00100-00100-00010" },
            {' ', "00000-00000-00000-00000-00000" },
        };

        public static string Unify(string input, bool fillempty, int padding)
        {
            string paddingText = "";

            if (padding != 0)
                for (int i = 0; i < padding; i++)
                    paddingText += "0";

            input = input.ToLower();
            string[] unistrings = new string[] { "0" + paddingText, "0" + paddingText, "0" + paddingText, "0" + paddingText, "0" + paddingText };

            for (int i = 0; i < input.Length; i++)
            {
                string[] sepratedstring;

                try
                {
                    sepratedstring = UniDict[input[i]].Split('-');
                }
                catch
                {
                    sepratedstring = "00000-00000-00000-00000-00000".Split('-');
                }

                for (int a = 0; a < 5; a++)
                    unistrings[a] += sepratedstring[a] + "0";
            }

            for (int i = 0; i < 5; i++)
                unistrings[i] += paddingText;

            string additive = "";
            for (int i = 0; i < unistrings[0].Length; i++)
                additive += "0";

            return $"{additive}\n{unistrings[0]}\n{unistrings[1]}\n{unistrings[2]}\n{unistrings[3]}\n{unistrings[4]}\n{additive}".Replace('0', fillempty ? '░' : ' ').Replace('1', '█');
        }

        public static string Boxify(string input, bool wide, bool tall)
        {
            int maxlength = 0;

            string[] boxText = input.Split('\n');

            for (int i = 0; i < boxText.Length; i++)
                maxlength = boxText[i].Length > maxlength ? boxText[i].Length : maxlength;

            int borderAdd = wide ? 2 : 0;
            int textAdd = wide ? 1 : 0;
            string spaceAdd = wide ? " " : "";

            string outText = "╔";
            for (int i = 0; i < maxlength + borderAdd; i++)
                outText += "═";
            outText += "╗\n";

            if (tall)
            {
                outText += "║";
                for (int i = 0; i < maxlength + borderAdd; i++)
                    outText += " ";
                outText += "║\n";
            }

            for (int i = 0; i < boxText.Length; i++)
            {
                outText += "║" + spaceAdd + boxText[i];
                for (int _i = 0; _i < maxlength - boxText[i].Length + textAdd; _i++)
                    outText += " ";
                outText += "║\n";
            }

            if (tall)
            {
                outText += "║";
                for (int i = 0; i < maxlength + borderAdd; i++)
                    outText += " ";
                outText += "║\n";
            }

            outText += "╚";
            for (int i = 0; i < maxlength + borderAdd; i++)
                outText += "═";
            outText += "╝\n";

            return outText;
        }
    }

    internal class Minefield
    {
        //PERMANENT VALUES
        private readonly string Name;
        private readonly int SizeY;
        private readonly int SizeX;
        private readonly int TotalMines;
        private readonly int TotalFields;
        private readonly int MinePercent;
        private int[,] Field;
        private DateTime StartTime;

        //CHANGING VALUES
        private int FakeMinesRemain;
        private int RealMinesRemain;
        private int FieldsRemain;
        private char[,] Display;
        private int CursorY = 0;
        private int CursorX = 0;
        private bool GameOver = false;

        //CHAR CONSTANTS
        private const char CharCovered = '·';
        private const char CharFlagged = '▒';
        private const char CharEmpty = ' ';

        public Minefield(string name, int sizex, int sizey, int percent)
        {
            //Setting init values
            Name = name;
            SizeX = sizex;
            SizeY = sizey;
            MinePercent = percent;

            //Setting calculated values
            TotalMines = (int)Math.Floor(sizex * sizey * percent / 100f);
            TotalFields = sizey * sizex - TotalMines;
            FieldsRemain = TotalFields;
            FakeMinesRemain = TotalMines;
            RealMinesRemain = TotalMines;
            StartTime = DateTime.Now;

            //Generating Field and Display
            GenerateField();
            GenerateDisplay();
        }

        private void Reset()
        {
            //Reset calculated values
            FieldsRemain = TotalFields;
            FakeMinesRemain = TotalMines;
            RealMinesRemain = TotalMines;
            StartTime = DateTime.Now;

            //Generating Field and Display
            GenerateField();
            GenerateDisplay();
        }

        private void GenerateField()
        {
            Field = new int[SizeY, SizeX];

            for (int y = 0; y < SizeY; y++)
                for (int x = 0; x < SizeX; x++)
                    Field[y, x] = 0;

            Random r = new Random();

            for (int placed = 0; placed < TotalMines; placed++)
            {
                int targetY = r.Next(0, SizeY);
                int targetX = r.Next(0, SizeX);

                if (Field[targetY, targetX] == -1 || (targetX == 0 && targetY == 0) || (targetX == 0 && targetY == SizeY - 1) || (targetX == SizeX - 1 && targetY == 0) || (targetX == SizeX - 1 && targetY == SizeY - 1))
                {
                    placed--;
                    continue;
                }

                Field[targetY, targetX] = -1;

                for (int y = Math.Max(targetY - 1, 0); y < Math.Min(targetY + 2, SizeY); y++)
                    for (int x = Math.Max(targetX - 1, 0); x < Math.Min(targetX + 2, SizeX); x++)
                        if (Field[y, x] != -1)
                            Field[y, x]++;
            }
        }

        private void GenerateDisplay()
        {
            Display = new char[SizeY, SizeX];

            for (int y = 0; y < SizeY; y++)
                for (int x = 0; x < SizeX; x++)
                    Display[y, x] = CharCovered;
        }

        public bool Play()
        {
            while (true)
            {
                if (!GameOver)
                {
                    Console.Clear();
                    DisplayField();
                    TimeSpan timetaken = DateTime.Now - StartTime;
                    Console.WriteLine(Graphics.Boxify($"{Name}\n\nMines left: {FakeMinesRemain}/{TotalMines}\nFields left: {FieldsRemain}/{TotalFields}\n\nTime: {timetaken.Hours}h{timetaken.Minutes}m{timetaken.Seconds}s", false, false));

                    ConsoleKey input = Console.ReadKey().Key;

                    if (input == ConsoleKey.W || input == ConsoleKey.UpArrow)
                        CursorY = --CursorY >= 0 ? CursorY : 0;
                    else if (input == ConsoleKey.S || input == ConsoleKey.DownArrow)
                        CursorY = ++CursorY < SizeY ? CursorY : SizeY - 1;
                    else if (input == ConsoleKey.A || input == ConsoleKey.LeftArrow)
                        CursorX = --CursorX >= 0 ? CursorX : 0;
                    else if (input == ConsoleKey.D || input == ConsoleKey.RightArrow)
                        CursorX = ++CursorX < SizeX ? CursorX : SizeX - 1;
                    else if (input == ConsoleKey.E || input == ConsoleKey.Q || input == ConsoleKey.F)
                        FlagField();
                    else if (input == ConsoleKey.Spacebar || input == ConsoleKey.Enter)
                        RevealField();
                    else if (input == ConsoleKey.Escape)
                        GameOver = true;
                    else if (input == ConsoleKey.R)
                        Reset();

                    if ((RealMinesRemain == 0 || FieldsRemain == 0) && FakeMinesRemain >= 0)
                    {
                        GameOver = true;
                        RealMinesRemain = 0;
                        FieldsRemain = 0;
                    }
                }
                else
                {
                    Console.Clear();

                    string wintext; ;
                    if (RealMinesRemain == 0 || FieldsRemain == 0)
                        wintext = "You've WON!";
                    else
                        wintext = "You've LOST!";

                    RevealMines();
                    DisplayField();
                    TimeSpan timetaken = DateTime.Now - StartTime;
                    Console.WriteLine(Graphics.Boxify($"{Name}\n\n{wintext}\n\nMines left: {FakeMinesRemain}/{TotalMines}\nFields left: {FieldsRemain}/{TotalFields}\n\nTime: {timetaken.Hours}h{timetaken.Minutes}m{timetaken.Seconds}s", false, false));
                    Thread.Sleep(2000);
                    Console.WriteLine("Press any button to continue...");
                    Console.ReadKey();

                    if (RealMinesRemain == 0 || FieldsRemain == 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        private void DisplayField()
        {
            string stringOne = "╔";
            string stringTwo = "";
            string inverted = "";
            ref string currentString = ref stringOne;

            for (int i = 0; i < SizeX; i++)
                stringOne += "═";
            stringOne += "╗\n";

            for (int y = 0; y < SizeY; y++)
            {
                currentString += "║";
                for (int x = 0; x < SizeX; x++)
                {
                    if (x == CursorX && y == CursorY)
                    {
                        inverted += Display[y, x];
                        currentString = ref stringTwo;
                    }
                    else
                        currentString += Display[y, x];
                }
                currentString += "║\n";
            }

            stringTwo += "╚";
            for (int i = 0; i < SizeX; i++)
                stringTwo += "═";
            stringTwo += "╝\n";

            Console.Write(stringOne);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(inverted);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(stringTwo);
        }

        private void RevealMines()
        {
            for (int y = 0; y < SizeY; y++)
                for (int x = 0; x < SizeX; x++)
                    if (Field[y, x] == -1)
                        Display[y, x] = 'X';
        }

        private void FlagField()
        {
            if (Display[CursorY, CursorX] == CharCovered)
            {
                Display[CursorY, CursorX] = CharFlagged;
                FakeMinesRemain--;

                if (Field[CursorY, CursorX] == -1)
                    RealMinesRemain--;
            }
            else if (Display[CursorY, CursorX] == CharFlagged)
            {
                Display[CursorY, CursorX] = CharCovered;
                FakeMinesRemain++;

                if (Field[CursorY, CursorX] == -1)
                    RealMinesRemain++;
            }
        }

        private void RevealField()
        {
            switch (Display[CursorY, CursorX])
            {
                case CharFlagged: // Flag cancels
                    return;

                case CharEmpty: // Zero cancels
                    return;

                case CharCovered:
                    switch (Field[CursorY, CursorX])
                    {
                        case 0:
                            ZeroReveal(CursorY, CursorX);
                            return;

                        case -1:
                            GameOver = true;
                            return;

                        default:
                            FieldsRemain--;
                            Display[CursorY, CursorX] = (Field[CursorY, CursorX] + "")[0];
                            return;
                    }

                default:
                    int flagcount = 0;
                    for (int y = Math.Max(CursorY - 1, 0); y < Math.Min(CursorY + 2, SizeY); y++)
                        for (int x = Math.Max(CursorX - 1, 0); x < Math.Min(CursorX + 2, SizeX); x++)
                        {
                            if (Display[y, x] == CharFlagged)
                                flagcount++;
                        }

                    if (flagcount == Field[CursorY, CursorX])
                    {
                        for (int y = Math.Max(CursorY - 1, 0); y < Math.Min(CursorY + 2, SizeY); y++)
                            for (int x = Math.Max(CursorX - 1, 0); x < Math.Min(CursorX + 2, SizeX); x++)
                            {
                                if (Display[y, x] == CharCovered)
                                {
                                    switch (Field[y, x])
                                    {
                                        case -1:
                                            GameOver = true;
                                            break;

                                        case 0:
                                            ZeroReveal(y, x);
                                            break;

                                        default:
                                            FieldsRemain--;
                                            Display[y, x] = (Field[y, x] + "")[0];
                                            break;
                                    }
                                }
                            }
                    }
                    return;
            }
        }

        private void ZeroReveal(int y, int x)
        {
            if (Display[y, x] == CharCovered)
            {
                if (Field[y, x] == 0)
                {
                    FieldsRemain--;
                    Display[y, x] = CharEmpty;

                    for (int _y = Math.Max(y - 1, 0); _y < Math.Min(y + 2, SizeY); _y++)
                        for (int _x = Math.Max(x - 1, 0); _x < Math.Min(x + 2, SizeX); _x++)
                            ZeroReveal(_y, _x);
                }
                else
                {
                    FieldsRemain--;
                    Display[y, x] = (Field[y, x] + "")[0];
                }
            }
        }
    }
}
