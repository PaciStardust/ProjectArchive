namespace ProjectArchive.Projects.ConJeweled
{
    [Project("ConJeweled", 1640300400, timeEnd: 1640386800, "A WIP bejeweled for console")]
    internal class Program : IProject
    {
        public void Run()
        {

            Console.ReadLine();
            //Graphics test & init
            Graphics screen = new Graphics(new Vector2(30, 30));

            screen.DrawBox(new Vector2(1), new Vector2(10));
            screen.Clear(new Vector2(2), new Vector2(9));

            screen.DrawBox(new Vector2(1, 12), new Vector2(10, 14));
            screen.Clear(new Vector2(2, 13), new Vector2(9, 13));

            screen.DrawBox(new Vector2(4), new Vector2(6));

            Vector2 cursorMax = new Vector2(7);
            Vector2 cursor = new Vector2(4, 3);

            Random r = new Random();
            int[] testArray = new int[64].Select(x => r.Next(0, 5)).ToArray();

            Vector2 selected = new Vector2(-1);

            while (true)
            {
                for (int i = 0; i < testArray.Length; i++)
                {
                    if (i == cursor.Y * 8 + cursor.X || i == selected.Y * 8 + selected.X)
                    {
                        screen.InvertColor();
                        screen.DrawText(new Vector2(i % 8, i / 8) + 2, testArray[i] + "");
                        screen.InvertColor();
                    }
                    else
                    {
                        screen.DrawText(new Vector2(i % 8, i / 8) + 2, testArray[i] + "");
                    }
                }
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        cursor.Y--;
                        continue;

                    case ConsoleKey.S:
                        cursor.Y++;
                        break;

                    case ConsoleKey.D:
                        cursor.X++;
                        break;

                    case ConsoleKey.A:
                        cursor.X--;
                        break;

                    case ConsoleKey.Enter:
                        if (selected.X == -1)
                        {
                            selected = cursor;
                        }
                        else
                        {
                            int selectedpos = selected.X + selected.Y * 8;
                            int cursorpos = cursor.X + selected.Y * 8;
                            int buffer = testArray[selectedpos];
                            testArray[selectedpos] = testArray[cursorpos];
                            testArray[cursorpos] = buffer;
                            selected = new Vector2(-1);
                        }
                        break;
                }
                //cursor = Vector2.Limit(cursor, cursorMax);
            }
        }
    }
}
