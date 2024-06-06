using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace ProjectArchive.Projects.CodeEffectGen
{
    [Project("Code Effect Generator", 1677452400, 1677452400, "Generates a scrolling texture used for 3D model eyes")]
    internal class Program : IProject
    {
        public void Run()
        {
            var width = AskInt("Width");
            var height = AskInt("Height");
            var maxLen = AskInt("MaxLen");
            var minLen = AskInt("MinLen");
            var gap = AskInt("Gap");
            var maxBlock =  AskInt("MaxBlock");
            var minBlock = AskInt("MinBlock");
            var path = AskString("Path");

            var bmp = new Image<Rgba32>(width, height, new Rgba32(0, 0, 0, 0));

            var r = new Random();

            for (int y = 0; y < height; y += gap + 1)
            {
                var blocks = r.Next(minBlock, maxBlock + 1);

                int x = 0;
                int block = 0;

                while (x < width && block < blocks)
                {
                    var blockLen = r.Next(minLen, maxLen + 1);

                    var maxX = x + blockLen;

                    if (maxX >= width)
                        break;

                    while (x < maxX)
                    {
                        bmp[x, y] = Color.Black;
                        x++;
                    }
                    x++;
                    block++;
                }
            }

            bmp.SaveAsPng(path);
     
        }

        private static string AskString(string question)
        {
            Console.Write($"{question}\n> ");
            var value = Console.ReadLine();
            Console.WriteLine();
            return value ?? string.Empty;
        }

        private static int AskInt(string question)
        {
            while (true)
            {
                var answer = AskString(question);

                if (int.TryParse(answer, out int value))
                    return value;
            }
        }
    }
}
