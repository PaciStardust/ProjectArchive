using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ProjectArchive.Projects.GifSplicer
{
    [Project("Gif Splicer", 1694296800, 1702162800, "Overlays 2 Gifs")]
    internal class Program : IProject
    {
        public void Run()
        {
            Console.WriteLine("File 1");
            var fileGif1 = Console.ReadLine()!.Trim('"');
            Console.WriteLine("File 2");
            var fileGif2 = Console.ReadLine()!.Trim('"');
            Console.WriteLine("PX Offset");
            var offset = int.Parse(Console.ReadLine()!);

            var gif1 = Image.Load(fileGif1!);
            var gif2 = Image.Load(fileGif2!);

            if (gif1.Width != gif2.Width)
            {
                Console.WriteLine("Width no match");
                return;
            }
            else if (gif1.Height != gif2.Height)
            {
                Console.WriteLine("Height no match");
                return;
            }

            var frames1 = gif1.Frames;
            var frames2 = gif2.Frames;

            if (frames1.Count != frames2.Count)
            {
                Console.WriteLine("Frames no match");
                return;
            }

            var cropRect = new Rectangle(offset, 0, gif1.Width - offset, gif1.Height);
            var drawPoint = new Point(offset, 0);
            var gOpt = new GraphicsOptions()
            {
                AlphaCompositionMode = PixelAlphaCompositionMode.Src
            };
            for (int i = 0; i < frames1.Count; i++)
            {
                var frame = frames1.CloneFrame(i);
                var frame2 = frames2.CloneFrame(i);

                frame2.Mutate(x => x.Crop(cropRect));
                frame.Mutate(x => x.DrawImage(frame2, drawPoint, gOpt));
                frames1.RemoveFrame(i);
                var newFrame =
                frames1.InsertFrame(i, frame.Frames.First());
            }

            Console.WriteLine("File Save");
            gif1.SaveAsGif(Console.ReadLine()!.Trim('"'));
        }
    }
}
