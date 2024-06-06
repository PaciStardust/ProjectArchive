using System.Text;

namespace ProjectArchive.Projects.ConJeweled
{
    internal struct Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2(int coord)
        {
            X = coord;
            Y = coord;
        }

        public static Vector2 operator +(Vector2 one, Vector2 two)
        {
            return new Vector2(one.X + two.X, one.Y + two.Y);
        }

        public static Vector2 operator +(Vector2 one, int two)
        {
            return new Vector2(one.X + two, one.Y + two);
        }

        public static Vector2 operator -(Vector2 one, Vector2 two)
        {
            return new Vector2(one.X - two.X, one.Y - two.Y);
        }

        public static Vector2 operator -(Vector2 one, int two)
        {
            return new Vector2(one.X - two, one.Y - two);
        }

        public static Vector2 Limit(Vector2 output, Vector2 limit)
        {
            int x = Math.Max(Math.Min(limit.X, output.X), 0);
            int y = Math.Max(Math.Min(limit.Y, output.Y), 0);
            return new Vector2(x, y);
        }
    }

    internal static class Utility
    {
        public static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }
    }
}
