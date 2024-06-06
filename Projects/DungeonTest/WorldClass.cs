namespace ProjectArchive.Projects.DungeonTest
{
    static class World //Stores all world info
    {
        public static string Name;
        public static int DayCount; //Unused
        public static MapTile_Base[,] Map;
        public static Vector2 Size;

        public static void InitWorld(string name, Vector2 size)
        {
            Name = name;
            DayCount = 0;
            Map = Generate(size);
            Size = size;

            //Preparing Utility Border for maps
            string border = "";
            for (int i = 0; i < size.X + 2; i++)
                border += Symbol.Box;
            Utility.MapBorder = border;
        }

        public static MapTile_Base[,] Generate(Vector2 size)
        {
            MapTile_Base[,] genmap = new MapTile_Base[size.Y, size.X];

            //TEMP
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                    genmap[y, x] = new MapTile_Plains(size);

            return genmap;
        }

        public static ref MapTile_Base MapTile(Vector2 location)
        {
            return ref Map[location.Y, location.X];
        }

        public static List<string> MapStringlist(Vector2 cursor)
        {
            List<string> stringlist = new List<string>();

            //Top Border
            stringlist.Add(Utility.MapBorder);

            //Middle
            for (int y = 0; y < Size.Y; y++)
            {
                string toAppend = "";
                toAppend += Symbol.Box;
                for (int x = 0; x < Size.X; x++)
                {
                    if (y == cursor.Y && x == cursor.X)
                        toAppend += "X";
                    else
                        toAppend += Map[y, x].Icon;
                }
                stringlist.Add(toAppend + Symbol.Box);
            }

            //Bottom border
            stringlist.Add(Utility.MapBorder);

            return stringlist;
        }
    }
}
