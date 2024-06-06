using ProjectArchive.Projects.PfpAnimator.Processors;

namespace ProjectArchive.Projects.PfpAnimator
{
    [Project("Pfp Animator", 1659909600, timeEnd: 1663452000, "Animate transparent profile pictures by adding a scrolling background")]
    internal class Program : IProject
    {
        public void Run()
        {
            var imgConfig = new ImageConfig()
            {
                PathFront = ImageConfig.AskFile("Foreground Image").Trim('\"'),
                PathBack = ImageConfig.AskFile("Background Image").Trim('\"'),
                PathSaveFolder = ImageConfig.AskPath("Save Folder").Trim('\"'),
                PathSaveName = ImageConfig.AskName("Save Name"),
                FrameCount = ImageConfig.AskInt("Frames"),
                FrameTime = ImageConfig.AskInt("MS per Frame")
            };

            ImageProcessor processor = ImageConfig.Ask("Mode: S = Scroll, Default = Hue").ToUpper() switch
            {
                "S" => new ScollingProcessor(imgConfig),
            };

            processor.Create();
        }
    }
}
