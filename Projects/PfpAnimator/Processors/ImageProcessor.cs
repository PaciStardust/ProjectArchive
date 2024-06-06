using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace ProjectArchive.Projects.PfpAnimator.Processors
{
    internal abstract class ImageProcessor
    {
        protected Image ImageFront { get; set; }
        protected Image ImageBack { get; set; }
        protected ImageConfig Config { get; set; }
        protected Image<Rgb24> Gif { get; set; }

        public ImageProcessor(ImageConfig config)
        {
            Config = config;
            ImageFront = Image.Load(Config.PathFront);
            ImageBack = Image.Load(Config.PathBack);
        }

        public void Create()
        {
            Gif = new Image<Rgb24>(ImageFront.Width, ImageFront.Height);
            var metadata = Gif.Metadata.GetFormatMetadata(GifFormat.Instance);
            metadata.ColorTableMode = GifColorTableMode.Local;
            metadata.RepeatCount = 0;

            Process();

            Gif.Frames.RemoveFrame(0);
            Gif.SaveAsGif(Config.PathSaveFull);
        }

        protected abstract void Process();
    }
}
