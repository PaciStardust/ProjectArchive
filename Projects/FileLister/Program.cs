namespace ProjectArchive.Projects.FileLister
{
    [Project("File Lister", 1, 1, "First project, creation unknown, lists files in directory with type")]
    internal class Program : IProject
    {
        public void Run()
        {
            Console.Write("Give me a directory\n> ");
            string Input = Console.ReadLine() ?? string.Empty;

            Console.Write("Give me a filename\n> ");
            string Filename = Console.ReadLine() ?? string.Empty;

            string[] Filelist = Directory.GetFiles(Input);
            string[] Folderlist = Directory.GetDirectories(Input);

            Dictionary<string, string> Filetypelist = new Dictionary<string, string>()
            {
                {"exe", "Executable"},
                {"url", "Executable"},
                {"lnk", "Executable"},
                {"bat", "Script File"},
                {"dll", "Script File"},
                {"ini", "Ini - File"},
                {"png", "Image File"},
                {"jpeg", "Image File"},
                {"txt", "Text File"},
                {"bin", "Text File"},
                {"dat", "Text File"},
                {"log", "Log File"},
                {"none", "No file extention"}
            };

            List<string> ExecutableList = new List<string>();
            List<string> IniList = new List<string>();
            List<string> ImageList = new List<string>();
            List<string> TextList = new List<string>();
            List<string> ScriptList = new List<string>();
            List<string> LogList = new List<string>();
            List<string> UnknownList = new List<string>();
            List<List<string>> ListList = new List<List<string>>
                {
                    ExecutableList,
                    IniList,
                    ImageList,
                    TextList,
                    ScriptList,
                    LogList,
                    UnknownList
                };

            Dictionary<List<string>, ConsoleColor> ColorList = new Dictionary<List<string>, ConsoleColor>()
            {
                {ExecutableList, ConsoleColor.Yellow},
                {IniList, ConsoleColor.Red},
                {ImageList, ConsoleColor.Magenta},
                {TextList, ConsoleColor.Cyan},
                {ScriptList, ConsoleColor.Blue},
                {LogList, ConsoleColor.Gray},
                {UnknownList, ConsoleColor.DarkGray},
            };

            List<string> AllList = new List<string>();

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string foldername in Folderlist)
            {
                string FolderName = foldername.Replace(Input + "\\", "");
                Console.WriteLine(FolderName + " (Folder)");
                AllList.Add(FolderName + " (Folder)");
            }

            foreach (string filename in Filelist)
            {
                string FileName = filename.Replace(Input + "\\", "");
                int DotIndex = FileName.LastIndexOf(".");
                string FileEx = "none";

                if (DotIndex != -1)
                {
                    FileEx = FileName.Substring(DotIndex + 1).ToLower();
                }

                if (Filetypelist.ContainsKey(FileEx))
                {
                    FileName = FileName + " (" + Filetypelist[FileEx] + ")";

                    switch (Filetypelist[FileEx])
                    {
                        case "Executable": ExecutableList.Add(FileName); break;
                        case "Ini - File": IniList.Add(FileName); break;
                        case "Image File": ImageList.Add(FileName); break;
                        case "Text File": TextList.Add(FileName); break;
                        case "Script File": ScriptList.Add(FileName); break;
                        case "Log File": LogList.Add(FileName); break;
                        default: UnknownList.Add(FileName); break;
                    }
                }
                else
                {
                    FileName = FileName + " (Unknown File Extention)";
                    UnknownList.Add(FileName);
                }
            }

            foreach (List<string> exlist in ListList)
            {
                Console.ForegroundColor = ColorList[exlist];
                foreach (string filename in exlist)
                {
                    Console.WriteLine(filename);
                    AllList.Add(filename);
                }
                Console.ResetColor();
            }

            Console.Write("Give me a filename for saving\n> ");
            string Savefilename = Console.ReadLine() ?? string.Empty;

            File.WriteAllLines(Savefilename, AllList);
        }
    }
}
