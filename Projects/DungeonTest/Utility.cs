namespace ProjectArchive.Projects.DungeonTest
{
    static class Utility
    {
        public static List<char> CharacterWhitelist = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ' ', '-', '_' };
        public static string MapBorder;
        public static Random RNG = new Random();
        public static List<ConsoleColor> Colors = new List<ConsoleColor>();

        public static bool IsStringName(string name) //Returns true if string is a "valid" name
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (!CharacterWhitelist.Contains(name[i]))
                    return false;
            }

            return true;
        }

        public static void NextColor()
        {
            if (Colors.Count == 0)
            {
                foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
                    Colors.Add(color);
            }

            int nextIndex = Colors.IndexOf(Console.ForegroundColor) + 1;

            if (nextIndex >= Colors.Count)
                nextIndex = 0;

            Console.ForegroundColor = Colors[nextIndex];
        }
    }

    static class Symbol //I hope this class gets more use
    {
        public static char Box = '\u2588';
    }

    struct Vector2 //Vector 2 to make my life easier
    {
        public int Y { get; set; }
        public int X { get; set; }

        public Vector2(int ypos, int xpos)
        {
            Y = ypos;
            X = xpos;
        }
    }

    enum Direction
    {
        North,
        East,
        South,
        West
    }
}
