namespace ProjectArchive.Projects.MiningTest
{
    [Project("Mining Test", 1, 1, "Prototype for the U# port of Motherload, unknown creation time")]
    internal class Program : IProject
    {
        static Cube player = new Cube("P", 0); //Player Character

        static Cube sky = new Cube("a", 9999999); //fake air in sky
        static Cube air = new Cube("a", 0);

        static Cube dirt = new Cube("d", 1);
        static Cube stone = new Cube("s", 2);
        static Cube rock = new Cube("r", 3);
        static Cube hardrock = new Cube("h", 4);
        static Cube nightstone = new Cube("n", 5);

        static int playerY = 5;
        static int playerX = 5;
        static int currentStrength = 1;
        static int currentCurrency;
        static int currentDepth;
        static int maxDepth = 0;

        static Cube[,] playArea = new Cube[500, 11];

        public void Run()
        {
            FillPlayArea();
            PlayLoop();
            //DebugMapPrint();
        }

        private static void FillPlayArea()
        {
            for (int y = 0; y < 500; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    playArea[y, x] = TerrainPassOne(y, x);
                }
            }
        }

        private static Cube TerrainPassOne(int ycord, int xcord)
        {
            if (ycord < 5)
            {
                return sky;
            }
            else if (ycord == 5)
            {
                if (xcord == 5)
                {
                    return player;
                }
                else
                {
                    return air;
                }
            }
            else if (ycord < 50)
            {
                return dirt;
            }
            else if (ycord < 100)
            {
                return stone;
            }
            else if (ycord < 150)
            {
                return rock;
            }
            else if (ycord < 200)
            {
                return hardrock;
            }
            else
            {
                return nightstone;
            }
        }

        private static void PrintPlayArea()
        {
            Console.WriteLine(currentCurrency + "G, " + currentStrength + "S, " + currentDepth + "/" + maxDepth + "D");

            for (int printY = playerY - 5; printY < playerY + 6; printY++)
            {
                for (int printX = 0; printX < 11; printX++)
                {
                    Console.Write("" + playArea[printY, printX].displayIcon);
                }
                Console.Write("\n");
            }
        }

        private static void MovePlayer(int moveY, int moveX)
        {
            int targetX = playerX + moveX;
            int targetY = playerY + moveY;

            //check if target within range
            if (targetX > 10 || targetX < 0)
            {
                return;
            }

            if (playArea[targetY, targetX].requiredStrength <= currentStrength)
            {
                currentCurrency += playArea[targetY, targetX].valueGained;
                playArea[playerY, playerX] = air;
                playerX += moveX;
                playerY += moveY;
                playArea[targetY, targetX] = player;
            }

            currentDepth = playerY - 5;
            if (currentDepth > maxDepth)
            {
                maxDepth = currentDepth;
            }
        }

        private static void PlayLoop()
        {
            while (true)
            {
                Console.Clear();
                PrintPlayArea();
                Console.Write("Where do you wanna go?\n> ");
                var input = Console.ReadKey().Key;

                if (input == ConsoleKey.W)
                {
                    MovePlayer(-1, 0);
                }
                else if (input == ConsoleKey.S)
                {
                    MovePlayer(1, 0);
                }
                else if (input == ConsoleKey.A)
                {
                    MovePlayer(0, -1);
                }
                else if (input == ConsoleKey.D)
                {
                    MovePlayer(0, 1);
                }
            }
        }

        private static void DebugMapPrint()
        {
            for (int ycord = 0; ycord < 500; ycord++)
            {
                for (int xcord = 0; xcord < 11; xcord++)
                {
                    Console.Write(playArea[ycord, xcord].displayIcon);
                }
                Console.Write("\n");
            }
        }

        private class Cube
        {
            public string displayIcon;
            public int requiredStrength;
            public int valueGained;

            public Cube(string display, int strength)
            {
                displayIcon = display;
                requiredStrength = strength;
                valueGained = 0;
            }

            public Cube(string display, int strength, int value)
            {
                displayIcon = display;
                requiredStrength = strength;
                valueGained = value;
            }
        }
    }
}
