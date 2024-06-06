namespace ProjectArchive.Projects.PathfinderTest
{
    [Project("Pathfinder Test", 1, 1, "Pathfinding test for an unreleased project, unknown creation time")]
    internal class Program : IProject
    {
        static readonly int size = 9;
        static readonly int[][] field = new int[size][]
            .Select(
                row => new int[size]
                .Select(
                    num => num = 1
                ).ToArray()
            ).ToArray();

        static int y = 1;
        static int x = 4;

        static readonly int[,] dirs = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        static int moveIndex = 0;

        public void Run()
        {
            for (int a = 0; a < size; a++)
                for (int b = 0; b < size; b++)
                    if (a == 1 || a == size - 2 || b == 1 || b == size - 2)
                        if (a == 0 || b == 0 || b == size - 1 || a == size - 1)
                            continue;
                        else
                            field[a][b] = 0;

            field[2][1] = 1;
            field[1][2] = 1;
            field[3][2] = 0;
            field[3][3] = 0;
            field[3][4] = 0;
            field[3][5] = 0;
            field[4][5] = 0;
            field[5][5] = 0;
            field[5][4] = 0;
            field[5][3] = 0;
            field[4][3] = 0;
            field[2][3] = 0;

            while (true)
            {
                Move();
                Draw();
                Console.ReadKey();
            }
        }



        static void Move()
        {
            bool hasFinished = false;
            int oldIndex = moveIndex;

            while (!hasFinished)
            {
                int targetY = y + dirs[moveIndex, 0];
                int targetX = x + dirs[moveIndex, 1];

                if (IsValidTile(targetY, targetX))
                {
                    y = targetY;
                    x = targetX;
                    Console.WriteLine("valid");
                    return;
                }

                int newMoveIndex = moveIndex + 1 < 4 ? moveIndex + 1 : 0;
                if (newMoveIndex == oldIndex)
                {
                    hasFinished = true;
                }
                else
                {
                    moveIndex = newMoveIndex;
                }
            }
        }

        static bool IsValidTile(int ypos, int xpos)
        {
            if (xpos < 0 || ypos < 0 || xpos >= size || ypos >= size)
            {
                return false;
            }
            if (field[ypos][xpos] != 0)
            {
                return false;
            }
            return true;
        }

        static void Draw()
        {
            Console.Clear();
            string drawS = "";
            for (int a = size - 1; a > -1; a--)
            {
                for (int b = 0; b < size; b++)
                {
                    if (a != y || b != x)
                        drawS += (field[a][b] == 0) ? '0' : ' ';
                    else
                        drawS += "P";
                }
                drawS += "\n";
            }
            Console.WriteLine(drawS);
        }
    }
}
