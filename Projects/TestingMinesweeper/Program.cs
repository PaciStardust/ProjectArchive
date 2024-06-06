namespace ProjectArchive.Projects.TestingMinesweeper
{
    [Project("Testing Minesweeper", 1631052000, 1631138400, "Minesweeper from test project")]
    internal class Program : IProject
    {
        //TODO:
        // - Difficulties

        //Field Values
        private static int SizeY = 0;
        private static int SizeX = 0;
        private static int MinePercent = 0;
        private static int[,] Field;

        //Game Values
        private static string SizeName = "";
        private static string DifficultyName = "";
        private static int PlacedMines = 0;
        private static int TotalFields = 0;
        private static int FieldsRemain = 0;
        private static int MinesRemainFake = 0;
        private static int MinesRemainReal = 0;
        private static int CursorY = 0;
        private static int CursorX = 0;
        private static char[,] Display;
        private static bool GameOver = false;
        private static DateTime StartTime;

        public void Run()
        {
            InitLoop();
            GenerateMinefield();
            GameplayLoop();
        }

        private static void InitLoop()
        {
            bool custom = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(Graphics.Boxify("Do you want a custom game? (Y/N)", true, false));

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    custom = true;
                    break;
                }
                else if (key == ConsoleKey.N)
                    break;
            }

            if (custom)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(Graphics.Boxify("Pick a size and mine ratio (x,y,%)", true, false));
                    string[] rawInput = Console.ReadLine().Split(',');

                    if (rawInput.Length != 3 || !Int32.TryParse(rawInput[0], out SizeX) || !Int32.TryParse(rawInput[1], out SizeY) || !Int32.TryParse(rawInput[2], out MinePercent) || SizeX > 220 || SizeY > 110 || MinePercent > 99)
                        continue;

                    SizeName = SizeX + "x" + SizeY;
                    DifficultyName = MinePercent + "%";

                    break;
                }
            }
            else
            {
                bool loop = true;
                while (loop)
                {
                    Console.Clear();
                    Console.WriteLine(Graphics.Boxify("Which size do you want?\n\n1 - Tiny (10x10)\n2 - Small (16x10)\n3 - Medium (16x16)\n4 - Large (24x20)\n5 - Mega (32x24)\n6 - Giga (48x32)\n7 - Ultra (64x32)\n8 - Ultra+ (96x64)\n9 - Ultra++ (160x96)\n0 - Otherworldly (220x110)", true, false));

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            SizeName = "TINY";
                            SizeX = 10;
                            SizeY = 10;
                            loop = false;
                            break;

                        case ConsoleKey.D2:
                            SizeName = "SMALL";
                            SizeX = 16;
                            SizeY = 10;
                            loop = false;
                            break;

                        case ConsoleKey.D3:
                            SizeName = "MEDIUM";
                            SizeX = 16;
                            SizeY = 16;
                            loop = false;
                            break;

                        case ConsoleKey.D4:
                            SizeName = "LARGE";
                            SizeX = 24;
                            SizeY = 20;
                            loop = false;
                            break;

                        case ConsoleKey.D5:
                            SizeName = "MEGA";
                            SizeX = 32;
                            SizeY = 24;
                            loop = false;
                            break;

                        case ConsoleKey.D6:
                            SizeName = "GIGA";
                            SizeX = 48;
                            SizeY = 32;
                            loop = false;
                            break;

                        case ConsoleKey.D7:
                            SizeName = "ULTRA";
                            SizeX = 64;
                            SizeY = 32;
                            loop = false;
                            break;

                        case ConsoleKey.D8:
                            SizeName = "ULTRA+";
                            SizeX = 96;
                            SizeY = 64;
                            loop = false;
                            break;

                        case ConsoleKey.D9:
                            SizeName = "ULTRA++";
                            SizeX = 160;
                            SizeY = 96;
                            loop = false;
                            break;

                        case ConsoleKey.D0:
                            SizeName = "OTHERWORLDLY";
                            SizeX = 220;
                            SizeY = 110;
                            loop = false;
                            break;
                    }
                }

                loop = true;
                while (loop)
                {
                    Console.Clear();
                    Console.WriteLine(Graphics.Boxify("Which difficulty do you want?\n\n1 - Stone (20%)\n2 - Iron (25%)\n3 - Bronze (30%)\n4 - Silver (34%)\n5 - Gold (37%)\n6 - Platinum (40%)\n7 - Diamond (43%)\n8 - Diamond+ (46%)\n9 - Diamond++ (50%)\n0 - Hellstone (55%)", true, false));

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            DifficultyName = "STONE";
                            MinePercent = 20;
                            loop = false;
                            break;

                        case ConsoleKey.D2:
                            DifficultyName = "IRON";
                            MinePercent = 25;
                            loop = false;
                            break;

                        case ConsoleKey.D3:
                            DifficultyName = "BRONZE";
                            MinePercent = 30;
                            loop = false;
                            break;

                        case ConsoleKey.D4:
                            DifficultyName = "SILVER";
                            MinePercent = 34;
                            loop = false;
                            break;

                        case ConsoleKey.D5:
                            DifficultyName = "GOLD";
                            MinePercent = 37;
                            loop = false;
                            break;

                        case ConsoleKey.D6:
                            DifficultyName = "PLATINUM";
                            MinePercent = 40;
                            loop = false;
                            break;

                        case ConsoleKey.D7:
                            DifficultyName = "DIAMOND";
                            MinePercent = 43;
                            loop = false;
                            break;

                        case ConsoleKey.D8:
                            DifficultyName = "DIAMOND+";
                            MinePercent = 46;
                            loop = false;
                            break;

                        case ConsoleKey.D9:
                            DifficultyName = "DIAMOND++";
                            MinePercent = 50;
                            loop = false;
                            break;

                        case ConsoleKey.D0:
                            DifficultyName = "HELLSTONE";
                            MinePercent = 55;
                            loop = false;
                            break;
                    }
                }
            }
        }

        private static void GenerateMinefield()
        {
            GameOver = false;
            CursorX = 0;
            CursorY = 0;

            Field = new int[SizeY, SizeX];

            for (int y = 0; y < SizeY; y++)
                for (int x = 0; x < SizeX; x++)
                    Field[y, x] = 0;

            Random r = new Random();

            PlacedMines = (int)Math.Floor(SizeX * SizeY * MinePercent / 100f);
            MinesRemainFake = PlacedMines;
            MinesRemainReal = PlacedMines;

            TotalFields = SizeX * SizeY - PlacedMines;
            FieldsRemain = TotalFields;

            for (int placed = 0; placed < PlacedMines; placed++)
            {
                int targetY = r.Next(0, SizeY);
                int targetX = r.Next(0, SizeX);

                if (Field[targetY, targetX] == -1 || (targetX == 0 && targetY == 0) || (targetX == 0 && targetY == SizeY - 1) || (targetX == SizeX - 1 && targetY == 0) || (targetX == SizeX - 1 && targetY == SizeY - 1))
                {
                    placed--;
                    continue;
                }

                Field[targetY, targetX] = -1;

                for (int y = targetY - 1 < 0 ? 0 : targetY - 1; y < (targetY + 2 > SizeY ? SizeY : targetY + 2); y++)
                    for (int x = targetX - 1 < 0 ? 0 : targetX - 1; x < (targetX + 2 > SizeX ? SizeX : targetX + 2); x++)
                        if (Field[y, x] != -1)
                            Field[y, x]++;
            }

            Display = new char[SizeY, SizeX];
            for (int y = 0; y < SizeY; y++)
                for (int x = 0; x < SizeX; x++)
                    Display[y, x] = '·';
        }

        private static void DisplayField()
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

        private static void GameplayLoop()
        {
            StartTime = DateTime.Now;

            while (true)
            {
                if (!GameOver)
                {
                    Console.Clear();
                    DisplayField();
                    TimeSpan timetaken = DateTime.Now - StartTime;
                    Console.WriteLine(Graphics.Boxify($"{SizeName} {DifficultyName}\n\nMines left: {MinesRemainFake}/{PlacedMines}\nFields left: {FieldsRemain}/{TotalFields}\n\nTime: {timetaken.Hours}h{timetaken.Minutes}m{timetaken.Seconds}s", false, false));

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

                    if ((MinesRemainReal == 0 || FieldsRemain == 0) && MinesRemainFake >= 0)
                    {
                        GameOver = true;
                        MinesRemainReal = 0;
                        FieldsRemain = 0;
                    }
                }
                else
                {
                    string wintext; ;
                    if (MinesRemainReal == 0 || FieldsRemain == 0)
                        wintext = "You've WON!";
                    else
                        wintext = "You've LOST!";

                    RevealAll();
                    Console.Clear();
                    DisplayField();
                    TimeSpan timetaken = DateTime.Now - StartTime;
                    Console.WriteLine(Graphics.Boxify($"{SizeName} {DifficultyName}\n\n{wintext}\n\nMines left: {MinesRemainFake}/{PlacedMines}\nFields left: {FieldsRemain}/{TotalFields}\n\nTime: {timetaken.Hours}h{timetaken.Minutes}m{timetaken.Seconds}s", false, false));
                    Thread.Sleep(2000);
                    Console.ReadKey();
                    break;
                }
            }
        }

        private static void FlagField()
        {
            if (Display[CursorY, CursorX] == '·')
            {
                Display[CursorY, CursorX] = '▒';
                MinesRemainFake--;

                if (Field[CursorY, CursorX] == -1)
                    MinesRemainReal--;
            }
            else if (Display[CursorY, CursorX] == '▒')
            {
                Display[CursorY, CursorX] = '·';
                MinesRemainFake++;

                if (Field[CursorY, CursorX] == -1)
                    MinesRemainReal++;
            }
        }

        private static void RevealAll()
        {
            for (int y = 0; y < SizeY; y++)
                for (int x = 0; x < SizeX; x++)
                {
                    switch (Field[y, x])
                    {
                        case -1:
                            Display[y, x] = 'X';
                            continue;

                        case 0:
                            Display[y, x] = ' ';
                            continue;

                        default:
                            Display[y, x] = (Field[y, x] + "")[0];
                            continue;
                    }
                }
        }

        private static void RevealField()
        {
            switch (Display[CursorY, CursorX])
            {
                case '▒': // Flag cancels
                    return;

                case ' ': // Zero cancels
                    return;

                case '·':
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
                    for (int y = CursorY - 1 < 0 ? 0 : CursorY - 1; y < (CursorY + 2 > SizeY ? SizeY : CursorY + 2); y++)
                        for (int x = CursorX - 1 < 0 ? 0 : CursorX - 1; x < (CursorX + 2 > SizeX ? SizeX : CursorX + 2); x++)
                        {
                            if (Display[y, x] == '▒')
                                flagcount++;
                        }

                    if (flagcount == Field[CursorY, CursorX])
                    {
                        for (int y = CursorY - 1 < 0 ? 0 : CursorY - 1; y < (CursorY + 2 > SizeY ? SizeY : CursorY + 2); y++)
                            for (int x = CursorX - 1 < 0 ? 0 : CursorX - 1; x < (CursorX + 2 > SizeX ? SizeX : CursorX + 2); x++)
                            {
                                if (Display[y, x] == '·')
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

        private static void ZeroReveal(int y, int x)
        {
            if (Display[y, x] == '·')
            {
                if (Field[y, x] == 0)
                {
                    FieldsRemain--;
                    Display[y, x] = ' ';

                    for (int _y = y - 1 >= 0 ? y - 1 : 0; _y < (y + 2 > SizeY ? SizeY : y + 2); _y++)
                        for (int _x = x - 1 >= 0 ? x - 1 : 0; _x < (x + 2 > SizeX ? SizeX : x + 2); _x++)
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
