using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.SampleProject
{
    [Project("Sample Project", 0, 0, "A sample to display how to add a new project")]
    internal class SampleProject : IProject
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Enter \"end\" to end");
                var input = Console.ReadLine();
                if (input is not null && input.Equals("end", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }
}
