namespace ProjectArchive.Projects.ConsoleDefense
{
    class Map
    {
        public string Name { get; }
        public int StartMoney { get; }
        public int StartHealth { get; }
        public Int2D StartLocation { get; }
        public Int2D StartFacing { get; }
        public MapTile[,] Floor { get; }
        public Int2D Size { get; }

        public Map(string name, int money, int health, Int2D startpos, Int2D startfac, MapTile[,] floor)
        {
            Name = name;
            StartMoney = money;
            StartHealth = health;
            StartLocation = startpos;
            StartFacing = startfac;
            Floor = floor;
        }
        public Map(string name, int money, int health, int startposy, int startposx, int startfacy, int startfacx, MapTile[,] floor)
        {
            Name = name;
            StartMoney = money;
            StartHealth = health;
            StartLocation = new Int2D(startposy, startposx);
            StartFacing = new Int2D(startfacy, startfacx);
            Floor = floor;
            Size = new Int2D(floor.GetLength(0), floor.GetLength(1));
        }

        public string[] DrawMap()
        {
            string[] mapstring = new string[Size.Y * 3];

            for (int y = 0; y < Size.Y; y++)
                for (int x = 0; x < Size.X; x++)
                    for (int i = 0; i < 3; i++)
                        mapstring[y * 3 + i] += Floor[y, x].Appearance[i];

            return mapstring;
        }
    }

    static class ListMaps
    {
        public static readonly Map TestMap = new Map("Test Map", 100, 100, 0, 7, 1, 0, new MapTile[,]
        {
            {MTL.Rubble, MTL.Floor, MTL.Path, MTL.Floor, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble },
            {MTL.Rubble, MTL.Floor, MTL.Path, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Rubble, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Rubble, MTL.Rubble },
            {MTL.Rubble, MTL.Floor, MTL.Path, MTL.Floor, MTL.PathR, MTL.Path, MTL.PathD, MTL.Floor, MTL.Rubble, MTL.Floor, MTL.Rubble, MTL.Floor, MTL.Rubble },
            {MTL.Rubble, MTL.Floor, MTL.Path, MTL.Floor, MTL.Path, MTL.Floor, MTL.Path, MTL.Floor, MTL.Rubble, MTL.Rubble, MTL.Floor, MTL.Floor, MTL.Rubble },
            {MTL.Rubble, MTL.Floor, MTL.PathR, MTL.Path, MTL.PathU, MTL.Floor, MTL.Path, MTL.Floor, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble },
            {MTL.Rubble, MTL.Rubble, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Path, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor },
            {MTL.Rubble, MTL.Floor, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Floor, MTL.PathR, MTL.Path, MTL.Path, MTL.Path, MTL.Path, MTL.Path, MTL.Path },
            {MTL.Rubble, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Rubble, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor, MTL.Floor },
            {MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble, MTL.Rubble }
        });
    }
}
