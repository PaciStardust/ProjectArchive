namespace ProjectArchive.Projects.DungeonTest
{
    internal static class MainLoop
    {
        public static void StartLoop()
        {
            while (true)
            {
                Slot heldItem = Player.HeldItem;
                Slot floorItem = World.MapTile(Player.WorldPos).Floor(Player.Pos);

                List<string> sideinfo = new List<string>
                {
                    $"{World.Name} - {World.MapTile(Player.WorldPos).Name}",
                     "",
                    $"{Player.Name} Info:",
                    $" > Position: {Player.PrintLocation()}",
                     " > Holding:  " + (heldItem.Amount == 0 ? "None" : heldItem.PrintInfoHeld()),
                     "",
                     "Floor:",
                     " > " + (floorItem.Amount == 0 ? "None" : floorItem.PrintInfoHeld() + " (F)")
                };

                Graphics.Clear();
                Graphics.Add(World.MapTile(Player.WorldPos).MapStringlist(), new Vector2(1, 1));
                Graphics.Add(sideinfo, new Vector2(1, World.Size.X + 4));
                Graphics.Draw();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        Player.Move(Direction.North);
                        continue;

                    case ConsoleKey.S:
                        Player.Move(Direction.South);
                        continue;

                    case ConsoleKey.D:
                        Player.Move(Direction.East);
                        continue;

                    case ConsoleKey.A:
                        Player.Move(Direction.West);
                        continue;
                    case ConsoleKey.E:
                        Storage.ListLoop($"{Player.Name}'s Bag", Player.Items);
                        continue;

                    case ConsoleKey.F:
                        Player.TryPickup();
                        continue;

                    case ConsoleKey.Q: //TEST
                        Utility.NextColor();
                        continue;

                    case ConsoleKey.Spacebar:
                        Player.TryUse();
                        continue;

                    default:
                        continue;
                }
            }
        }
    }
}
