using System.Diagnostics;

namespace ProjectArchive.Projects.PrimeFinder
{
    [Project("Prime Finder", 1, 1, "Finds the nth prime, unknown creation time")]
    internal class Program : IProject
    {
        public void Run()
        {
            Stopwatch sw = new Stopwatch();
            var input = AskInt("Which prime do you want to know?");
            sw.Restart();
            long i = EulerSeven(input);
            sw.Stop();
            Console.WriteLine(i + ": " + sw.ElapsedMilliseconds);
        }

        public static long EulerSeven(long primecount)
        {
            long[] primes = new long[primecount + 1];
            primes[0] = 2;
            primes[1] = 3;
            int emptyindex = 2;

            for (long i = 3; emptyindex < primecount + 1; i = i + 2)
            {
                foreach (long div in primes)
                {
                    if (i % div == 0)
                        break;

                    if (div > Math.Sqrt(i))
                    {
                        primes[emptyindex] = i;
                        emptyindex++;
                        break;
                    }
                }
            }

            return primes[emptyindex - 1];
        }

        internal static int AskInt(string question)
        {
            while (true)
            {
                var answer = AskString(question);

                if (int.TryParse(answer, out int value))
                    return value;
            }
        }

        internal static string AskString(string question)
        {
            Console.Write($"{question}\n> ");
            var value = Console.ReadLine();
            Console.WriteLine();
            return value ?? string.Empty;
        }
    }
}
