namespace ProjectArchive.Projects.DungeonTest
{
    static class Graphics
    {
        private static char[,] printText;
        private readonly static Vector2 size = new Vector2(25, 100);

        public static void Clear()
        {
            printText = new char[size.Y, size.X];

            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                    printText[y, x] = ' ';
        }

        public static void Add(List<string> list, Vector2 pos)
        {
            Vector2 listPos = new Vector2(0, 0);

            for (int y = pos.Y; y < pos.Y + list.Count; y++, listPos.Y++)
            {
                if (y >= size.Y)
                    return;

                for (int x = pos.X; x < pos.X + list[listPos.Y].Length; x++, listPos.X++)
                {
                    if (x >= size.X)
                        continue;

                    printText[y, x] = list[listPos.Y][listPos.X];
                }
                listPos.X = 0;
            }
        }

        public static void Draw()
        {
            Console.Clear();

            string s = "";

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                    s += printText[y, x];
                s += '\n';
            }

            Console.WriteLine(s);
        }
    }
}
