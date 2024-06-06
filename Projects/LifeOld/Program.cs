using System.Text;

namespace ProjectArchive.Projects.LifeOld
{
    [Project("Old Game Of Life", 1, 1, "An old version of game of life, unknown creation time")]
    internal class Program : IProject
    {
        static int width;
        static int height;
        static int interval;

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Give values (width, height, interval)");
            string inputstring = Console.ReadLine() ?? string.Empty;
            string[] inputlist = inputstring.Split(",");
            width = Int32.Parse(inputlist[0]);
            height = Int32.Parse(inputlist[1]);
            interval = Int32.Parse(inputlist[2]);
            int[,] life = new int[height, width];
            life = FillGrid(life);
            PrintLife(life);
            while (true)
            {
                Thread.Sleep(interval * 100);
                Console.Clear();
                life = UpdateGrid(life);
                PrintLife(life);

            }
        }

        static void PrintLife(int[,] life)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (life[y, x] == 1)
                    {
                        Console.Write("\u25A0");
                    }
                    else
                    {
                        Console.Write("\u25A1");
                    }
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        static int[,] FillGrid(int[,] life)
        {
            Random r = new Random();
            int[,] filledlife = life;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    filledlife[y, x] = r.Next(2);
                }
            }
            return filledlife;
        }

        static int getAlivePartners(int[,] life, int xcord, int ycord)
        {
            int alivePartners = 0;
            for (int yadd = -1; yadd <= 1; yadd++)
            {
                for (int xadd = -1; xadd <= 1; xadd++)
                {
                    if (yadd == 0 && xadd == 0)
                    {
                        alivePartners += 0;
                    }
                    else
                    {
                        if (ycord + yadd >= 0 && ycord + yadd < height)
                        {
                            if (xcord + xadd >= 0 && xcord + xadd < width)
                            {
                                if (life[ycord + yadd, xcord + xadd] == 1)
                                {
                                    alivePartners += 1;
                                }
                            }
                        }
                    }
                }
            }
            return alivePartners;
        }

        static int[,] UpdateGrid(int[,] oldlife)
        {
            int[,] newlife = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int partners = getAlivePartners(oldlife, x, y);
                    if (oldlife[y, x] == 1 && partners > 1 && partners < 4)
                    {
                        newlife[y, x] = 1;
                    }
                    else if (oldlife[y, x] == 0 && partners == 3)
                    {
                        newlife[y, x] = 1;
                    }
                    else
                    {
                        newlife[y, x] = 0;
                    }
                }
            }
            return newlife;
        }
    }
}
