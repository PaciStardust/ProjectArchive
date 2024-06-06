namespace ProjectArchive.Projects.DungeonTest
{
    abstract class MapTile_Base //Implements basics
    {
        public string Name { get; set; }
        public char Icon { get; set; }
        public bool IsLocked { get; set; }
        public string Description { get; set; }
        public Vector2 Size { get; set; }
        public Item_Tile[,] MapTile { get; set; }
        public Slot[,] MapFloor { get; set; }

        public abstract Item_Tile[,] GenerateMap(Vector2 size);

        public Slot[,] GenerateFloor(Vector2 size)
        {
            Slot[,] genfloor = new Slot[size.Y, size.X];

            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                    genfloor[y, x] = new Slot();

            return genfloor;
        }

        public ref Item_Tile Tile(Vector2 location)
        {
            return ref MapTile[location.Y, location.X];
        }

        public ref Slot Floor(Vector2 location)
        {
            return ref MapFloor[location.Y, location.X];
        }

        public List<string> MapStringlist()
        {
            List<string> stringlist = new List<string>();
            Vector2 playerPos = Player.Pos;

            //Top Border
            stringlist.Add(Utility.MapBorder);

            //Middle
            for (int y = 0; y < Size.Y; y++)
            {
                string toAppend = "";
                toAppend += Symbol.Box;
                for (int x = 0; x < Size.X; x++)
                {
                    if (y == playerPos.Y && x == playerPos.X)
                        toAppend += "☺";
                    else
                    {
                        if (MapTile[y, x].Icon == ' ' && MapFloor[y, x].Amount != 0)
                            toAppend += '·';
                        else
                            toAppend += MapTile[y, x].Icon;
                    }
                }
                stringlist.Add(toAppend + Symbol.Box);
            }

            //Bottom border
            stringlist.Add(Utility.MapBorder);

            return stringlist;
        }
    }

    class MapTile_Plains : MapTile_Base
    {
        public MapTile_Plains(Vector2 size)
        {
            Name = "Plains";
            Icon = 'p';
            IsLocked = false;
            Description = "A flat grassy area with a few trees dotted around";
            Size = size;
            MapTile = GenerateMap(size);
            MapFloor = GenerateFloor(size);
        }

        public override Item_Tile[,] GenerateMap(Vector2 size)
        {
            Item_Tile[,] genMap = new Item_Tile[size.Y, size.X];

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    if (Utility.RNG.Next(0, 10) == 0)
                        genMap[y, x] = Items.tree;
                    else
                        genMap[y, x] = Items.none;
                }
            }

            return genMap;
        }
    }
}
