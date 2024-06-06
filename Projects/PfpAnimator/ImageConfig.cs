using System.Text.RegularExpressions;

namespace ProjectArchive.Projects.PfpAnimator
{
    internal class ImageConfig
    {
        public string PathFront { get; init; }
        public string PathBack { get; init; }
        public string PathSaveFolder { get; init; }
        public string PathSaveName { get; init; }
        public string PathSaveFull => PathSaveFolder + "/" + PathSaveName;

        public int FrameCount { get; init; }
        public int FrameTime { get; init; }

        public static string Ask(string question)
        {
            Console.Write(question + "\n > ");
            return (Console.ReadLine() ?? "").Replace("\"", "");
        }

        public static string AskFile(string question)
        {
            while (true)
            {
                var res = Ask(question);
                if (File.Exists(res))
                    return res;
            }
        }

        public static int AskInt(string question)
        {
            while (true)
            {
                var res = Ask(question);
                if (int.TryParse(res, out int val))
                    return val;
            }
        }

        public static string AskPath(string question)
        {
            while (true)
            {
                var res = Ask(question);
                if (Directory.Exists(res))
                    return res;
            }
        }

        private static readonly Regex _nameMatcher = new("[a-zA-Z0-9]+");
        public static string AskName(string question)
        {
            while (true)
            {
                var res = Ask(question);
                if (_nameMatcher.IsMatch(res))
                    return res;
            }
        }
    }
}
