using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;

namespace ProjectArchive.Projects.PfpAnimator.Processors
{
    internal class ScollingProcessor : ImageProcessor
    {
        public ScollingProcessor(ImageConfig config) : base(config) { }

        protected override void Process()
        {
            var resOptions = new ResizeOptions()
            {
                Mode = ResizeMode.Stretch,
                Size = ImageFront.Size * 4
            };
            ImageBack.Mutate(x => x.Resize(resOptions));

            var xValue = -ImageFront.Width * 2 / Config.FrameCount;
            var yValue = -ImageFront.Height * 2 / Config.FrameCount;
            for (int i = 0; i < Config.FrameCount; i++)
            {
                var offset = new Point(xValue * i, yValue * i);
                var imageCopy = new Image<Rgb24>(ImageFront.Width, ImageFront.Height);
                imageCopy.Mutate(x => x.DrawImage(ImageBack, offset, 1));
                imageCopy.Mutate(x => x.DrawImage(ImageFront, 1));

                Gif.Frames.AddFrame(imageCopy.Frames[0]);
                Gif.Frames[i + 1].Metadata.GetFormatMetadata(GifFormat.Instance).FrameDelay = Config.FrameTime;

                imageCopy.Dispose();
            }
        }
    }
}
