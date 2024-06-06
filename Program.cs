using System.Reflection;
using System.Text;

namespace ProjectArchive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var projectList = GetProjects();
            if (projectList.Count == 0)
            {
                Console.WriteLine("Unable to find any projects, press any key to close");
                Console.ReadKey();
                return;
            }

            RunMainMenuLoop(projectList);
        }

        #region Main Menu
        private static void RunMainMenuLoop(List<ProjectReflectionInfo> projectList)
        {
            var currentIndex = 0;
            while (true)
            {
                ShowMainMenu(currentIndex, projectList);

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    currentIndex = Math.Max(currentIndex - 1, 0);

                else if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    currentIndex = Math.Min(currentIndex + 1, projectList.Count - 1);

                else if (key == ConsoleKey.Enter)
                    RunDetailMenuLoop(projectList[currentIndex]);
            }
        }

        private static void ShowMainMenu(int index, List<ProjectReflectionInfo> projectList)
        {
            Console.Clear();

            var slotsAboveIndex = projectList.Count - 1 - index;
            var displaySlotsBelowIndex = slotsAboveIndex < 6 ? 12 - slotsAboveIndex : 6;
            var displaySlotsAboveIndex = index < 6 ? 12 - index : 6;

            var startIndex = Math.Max(index - displaySlotsBelowIndex, 0);
            var endIndex = Math.Min(index + displaySlotsAboveIndex, projectList.Count - 1);

            var textBuilder = new StringBuilder($"All Projects ({projectList.Count})\n");

            for (int i = startIndex; i <= endIndex; i++)
            {
                var displayDots = (i == startIndex && i != 0) || (i == endIndex && i != projectList.Count - 1);
                var startTime = DateTime.UnixEpoch.AddSeconds(projectList[i].Details?.TimeStart ?? 0);
                var endTime = DateTime.UnixEpoch.AddSeconds(projectList[i].Details?.TimeEnd ?? 0);

                var infoText = displayDots ? "..." : $"{i} - {projectList[i].Details?.Name ?? projectList[i].Type.Name} ({startTime:d} - {endTime:d})";

                textBuilder.Append($"\n {(i == index ? ">" : " ")} {infoText}");
            }

            Console.WriteLine(textBuilder);
        }
        #endregion

        #region Detail Menu
        private static void RunDetailMenuLoop(ProjectReflectionInfo reflectionInfo)
        {
            Console.Clear();
            while (true)
            {
                var startTime = DateTime.UnixEpoch.AddSeconds(reflectionInfo.Details?.TimeStart ?? 0);
                var endTime = DateTime.UnixEpoch.AddSeconds(reflectionInfo.Details?.TimeEnd ?? 0);

                Console.WriteLine($"Name: {reflectionInfo.Details?.Name ?? reflectionInfo.Type.Name}\n\nTime: {startTime:d} - {endTime:d}\nInfo: {reflectionInfo.Details?.Description ?? "None"}");
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Enter)
                {
                    RunProject(reflectionInfo.Type);
                    break;
                }

                if (key == ConsoleKey.Escape)
                    break;
            }
        }

        private static void RunProject(Type projectType)
        {
            Console.Clear();

            if (!projectType.IsAssignableTo(typeof(IProject)) || projectType == typeof(IProject))
            {
                Console.WriteLine("Started project does not inherit IProject");
            }
            else
            {
                try
                {
                    var project = Activator.CreateInstance(projectType);
                    ((IProject)project!).Run();
                    Console.WriteLine("Project has finished running");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }
        #endregion

        #region Utils
        private static List<ProjectReflectionInfo> GetProjects()
        {
            var projectList = new List<ProjectReflectionInfo>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!type.IsAssignableTo(typeof(IProject)) || type == typeof(IProject))
                    continue;

                var projectAttribute = type.GetCustomAttribute<ProjectAttribute>();
                projectList.Add(new(type, projectAttribute));
            }
            return projectList.OrderBy(x => x.Details?.TimeStart ?? 0).ToList();
        }
        #endregion
    }
}
