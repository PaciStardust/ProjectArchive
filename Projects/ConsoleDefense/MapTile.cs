namespace ProjectArchive.Projects.ConsoleDefense
{
    internal enum TileTag
    {
        EnemyPath,
        EnemyPathTurnUp,
        EnemyPathTurnDown,
        EnemyPathTurnLeft,
        EnemyPathTurnRight,

        Placable,
        Unplacable
    }

    internal class MapTile
    {
        public string Name { get; }
        public string[] Appearance { get; }
        public TileTag Tag { get; }

        public MapTile(string name, TileTag tag, string[] appearance)
        {
            Name = name;
            Tag = tag;
            Appearance = appearance;
        }
    }

    internal static class MTL //Maptilelist
    {
        public static readonly MapTile Rubble = new MapTile("Rubble", TileTag.Unplacable, new string[] { "▓▓▓", "▓▓▓", "▓▓▓" });
        public static readonly MapTile Floor = new MapTile("Floor", TileTag.Placable, new string[] { "░░░", "░░░", "░░░" });

        public static readonly MapTile Path = new MapTile("Pathway", TileTag.EnemyPath, new string[] { "   ", " + ", "   " });
        public static readonly MapTile PathR = new MapTile("Pathway", TileTag.EnemyPathTurnRight, new string[] { "   ", " → ", "   " });
        public static readonly MapTile PathL = new MapTile("Pathway", TileTag.EnemyPathTurnLeft, new string[] { "   ", " ← ", "   " });
        public static readonly MapTile PathU = new MapTile("Pathway", TileTag.EnemyPathTurnUp, new string[] { "   ", " ↑ ", "   " });
        public static readonly MapTile PathD = new MapTile("Pathway", TileTag.EnemyPathTurnDown, new string[] { "   ", " ↓ ", "   " });
    }
}
