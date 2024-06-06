namespace ProjectArchive.Projects.DungeonTest
{
    [Project("Dungeon Test", 1629756000, 1630706400, "A simple dungeon game")]
    class Program : IProject
    {
        public void Run()
        {
            Graphics.Clear();

            //Intended way to start the game
            Loops.CreateWorld();
            Loops.CreatePlayer();

            //Quickstart because im lazy
            //World.InitWorld("Hell", new Vector2(9, 9));
            //Player.InitPlayer("Jeff", new Vector2(0, 0), new Vector2(0, 0));

            Storage.TryStore(Items.axeWood, 1, Player.Items);

            MainLoop.StartLoop();
        }
    }
}
