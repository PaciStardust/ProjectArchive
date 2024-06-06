using ProjectArchive.Projects.DungeonTest;

namespace ProjectArchive.Projects.ConJeweled
{
    internal class Graphics
    {
        public Vector2 ScreenSize { get; }

        public Graphics(Vector2 size)
        {
            ScreenSize = size - 1;
            Console.SetWindowSize(size.X + 2, size.Y + 2);
            Console.CursorVisible = false;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void DrawText(Vector2 start, string text)
        {
            start = Vector2.Limit(start, ScreenSize);
            Console.SetCursorPosition(start.X, start.Y);
            Console.Write(text);
        }

        public void DrawCharbox(Vector2 start, Vector2 end, string draw)
        {
            start = Vector2.Limit(start, ScreenSize);
            end = Vector2.Limit(end, ScreenSize);

            for (int i = start.Y; i <= end.Y; i++)
            {
                Console.SetCursorPosition(start.X, i);
                Console.Write(Utility.Repeat(draw, end.X - start.X + 1));
            }
        }

        public void Clear(Vector2 start, Vector2 end)
        {
            DrawCharbox(start, end, " ");
        }

        public void DrawBox(Vector2 start, Vector2 end)
        {
            DrawCharbox(start, end, "█");
        }

        public void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void InvertColor()
        {
            ConsoleColor buffer = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = buffer;
        }
    }
}
