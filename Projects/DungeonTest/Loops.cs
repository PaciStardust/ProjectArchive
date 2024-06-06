namespace ProjectArchive.Projects.DungeonTest
{
    static class Loops
    {
        public static void CreateWorld() //Asks for worldname and then inits world
        {
            while (true)
            {
                Console.Clear();
                Console.Write("What is this place called? \n > ");
                string answer = Console.ReadLine();

                if (answer.Replace(" ", "") != "" && answer.Length <= 12 && Utility.IsStringName(answer.ToLower()))
                {
                    Console.WriteLine($"The world from now on shall be called {answer}!\n(Press any key to continue)");
                    Console.ReadKey();
                    World.InitWorld(answer, new Vector2(9, 11));
                    return;
                }
                else
                {
                    Console.WriteLine($"\"{answer}\" is not a valid name!\n(Press any key to retry)");
                    Console.ReadKey();
                }
            }
        }

        public static void CreatePlayer() //Asks for player name and location and inits player
        {
            string name;
            Vector2 cursor = new Vector2(0, 0);

            while (true)
            {
                Console.Clear();
                Console.Write("What is your name? \n > ");
                string answer = Console.ReadLine();

                if (answer.Replace(" ", "") != "" && answer.Length <= 12 && Utility.IsStringName(answer.ToLower()))
                {
                    Console.WriteLine($"You shall be called {answer}!\n(Press any key to continue)");
                    Console.ReadKey();
                    name = answer;
                    break;
                }
                else
                {
                    Console.WriteLine($"\"{answer}\" is not a valid name!\n(Press any key to retry)");
                    Console.ReadKey();
                }
            }

            while (true)
            {
                MapTile_Base currentTile = World.MapTile(cursor);

                List<string> sideinfo = new List<string>
                {
                    $"{World.Name} - World Map",
                     "",
                    $"Sector Info:",
                     " > Name: " + currentTile.Name,
                     " > Locked: " + currentTile.IsLocked,
                     " > Info: " + currentTile.Description,
                     "",
                     "",
                     "Where would you like to start?",
                     "(X is cursor, SPACE to select, can't pick locked tiles)"
                };

                Graphics.Clear();
                Graphics.Add(World.MapStringlist(cursor), new Vector2(1, 1));
                Graphics.Add(sideinfo, new Vector2(1, World.Size.X + 4));
                Graphics.Draw();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        if (cursor.Y - 1 >= 0)
                            cursor.Y--;
                        continue;

                    case ConsoleKey.S:
                        if (cursor.Y + 1 < World.Size.Y)
                            cursor.Y++;
                        continue;

                    case ConsoleKey.A:
                        if (cursor.X - 1 >= 0)
                            cursor.X--;
                        continue;

                    case ConsoleKey.D:
                        if (cursor.X + 1 < World.Size.X)
                            cursor.X++;
                        continue;

                    case ConsoleKey.Spacebar:
                        if (World.Map[cursor.Y, cursor.X].IsLocked)
                            continue;

                        Random r = new Random();
                        Vector2 spawnPos = new Vector2(r.Next(0, World.Size.Y), r.Next(0, World.Size.X));

                        while (World.MapTile(cursor).Tile(spawnPos) != Items.none)
                            spawnPos = new Vector2(r.Next(0, World.Size.Y), r.Next(0, World.Size.X));

                        Player.InitPlayer(name, cursor, spawnPos);
                        return;

                    default:
                        continue;
                }
            }
        }
    }
}
