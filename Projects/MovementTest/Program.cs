using System.Text;

namespace ProjectArchive.Projects.MovementTest
{
    [Project("Movement Test", 1, 1, "Movement test for U# 2.5D Minecraft, unknown creation time")]
    internal class Program : IProject
    {
        enum MoveType { idle, moving, placing }

        static Tile[,] field = new Tile[0, 0];
        static string? playerName;
        static bool isAlive = true;

        static int boundsY;
        static int boundsX;

        static int posY;
        static int posX;

        static Tile air = new Tile("Air", '0', false);
        static Tile block = new Tile("Block", '1', true);
        static Tile wall = new Tile("Wall", '\u2588', true);

        static bool IsAlive { get => isAlive; set => isAlive = value; }

        public void Run()
        {
            AskForName();

            GenerateField(10, 10);

            posY = 2;
            posX = 2;

            GameplayLoop();
        }

        static void AskForName()
        {
            playerName = "";
            while (playerName == "" || playerName.Length > 5)
            {
                Console.Write("What is your name (Max 5 char)?\n > ");
                playerName = Console.ReadLine()?.ToUpper() ?? "none";
            }

            if (playerName.Length != 5)
                for (int i = 0; i < 6 - playerName.Length; i++)
                    playerName = playerName + " ";
        }

        static void GenerateField(int ybound, int xbound)
        {
            boundsY = ybound;
            boundsX = xbound;

            field = new Tile[ybound, xbound];

            for (int y = 0; y < boundsY; y++)
            {
                for (int x = 0; x < boundsX; x++)
                {
                    if (y == 0 || x == 0 || y == boundsY - 1 || x == boundsX - 1)
                        field[y, x] = wall;
                    else
                        field[y, x] = air;
                }
            }
        }

        static void GameplayLoop()
        {
            while (IsAlive)
            {
                DrawField();

                TakeInput();
            }
        }

        static void DrawField()
        {
            Console.Clear();

            StringBuilder text = new StringBuilder("");
            string spaces = "    ";

            int camY = posY;
            int camX = posX;

            //Align "cam" to bounds
            if (camY > boundsY - 3)
                camY = boundsY - 3;

            if (camY < 2)
                camY = 2;

            if (camX > boundsX - 3)
                camX = boundsX - 3;

            if (camX < 2)
                camX = 2;

            //Draw Cam Area
            text.Append($"{spaces}///////\n{spaces}/{playerName}/\n{spaces}///////\n");

            for (int y = camY - 2; y < camY + 3; y++)
            {
                text.Append($"{spaces}/");
                for (int x = camX - 2; x < camX + 3; x++)
                {
                    if (x == posX && y == posY)
                        text.Append("P");
                    else
                        text.Append(field[y, x].Icon);
                }
                text.Append($"/\n");
            }

            text.Append($"{spaces}///////\n\n");

            Console.WriteLine($"{text}ARROWS = Movement\n  WASD = Placing");
        }

        static void TakeInput()
        {
            ConsoleKey input = Console.ReadKey().Key;

            int moveY = posY;
            int moveX = posX;

            MoveType moveType = MoveType.idle;

            //Movement
            if (input == ConsoleKey.RightArrow && posX != boundsX - 1 && field[posY, posX + 1].isSolid == false)
            {
                moveX = posX + 1;
                moveType = MoveType.moving;
            }

            else if (input == ConsoleKey.LeftArrow && posX != 0 && field[posY, posX - 1].isSolid == false)
            {
                moveX = posX - 1;
                moveType = MoveType.moving;
            }

            else if (input == ConsoleKey.UpArrow && posY != 0 && field[posY - 1, posX].isSolid == false)
            {
                moveY = posY - 1;
                moveType = MoveType.moving;
            }

            else if (input == ConsoleKey.DownArrow && posY != boundsY - 1 && field[posY + 1, posX].isSolid == false)
            {
                moveY = posY + 1;
                moveType = MoveType.moving;
            }

            //Placement
            else if (input == ConsoleKey.D && posX != boundsX - 1)
            {
                moveX = posX + 1;
                moveType = MoveType.placing;
            }

            else if (input == ConsoleKey.A && posX != 0)
            {
                moveX = posX - 1;
                moveType = MoveType.placing;
            }

            else if (input == ConsoleKey.W && posY != 0)
            {
                moveY = posY - 1;
                moveType = MoveType.placing;
            }

            else if (input == ConsoleKey.S && posY != boundsY - 1)
            {
                moveY = posY + 1;
                moveType = MoveType.placing;
            }

            //Execute
            switch (moveType)
            {
                case MoveType.moving:

                    posY = moveY;
                    posX = moveX;
                    break;

                case MoveType.placing:
                    if (field[moveY, moveX] == air)
                        field[moveY, moveX] = block;
                    else if (field[moveY, moveX] == block)
                        field[moveY, moveX] = air;
                    break;
            }
        }

        private class Tile
        {
            private string _name;
            private char _icon;
            private bool _isSolid;

            public Tile(string name, char icon, bool isSolid)
            {
                _name = name;
                _icon = icon;
                _isSolid = isSolid;
            }

            public string Name
            {
                get { return _name; }
            }

            public char Icon
            {
                get { return _icon; }
            }

            public bool isSolid
            {
                get { return _isSolid; }
            }
        }
    }
}
