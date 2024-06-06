namespace ProjectArchive.Projects.ConsoleDefense
{
    internal struct Int2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Int2D(int y, int x)
        {
            Y = y;
            X = x;
        }

        public static Int2D Zero()
        {
            return new Int2D(0, 0);
        }
    }

    internal static class Parsing
    {
        private static readonly List<char> Alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly List<char> Numerals = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly List<char> Symbols = new List<char> { ' ', '-', '_' };

        public static bool IsValid(string input, int max, int min, bool alphabet, bool numerals, bool symbols)
        {
            string parsedInput = input.ToLower().Replace(" ", "");

            if (parsedInput == "" || input.Length > max || input.Length < min)
                return false;

            List<char> allowedChars = new List<char>();

            if (alphabet)
                allowedChars.AddRange(Alphabet);
            if (numerals)
                allowedChars.AddRange(Numerals);
            if (symbols)
                allowedChars.AddRange(Symbols);

            for (int i = 0; i < input.Length; i++)
            {
                if (!allowedChars.Contains(input[i]))
                    return false;
            }

            return true;
        }

        public static string[] StringToArray(string input)
        {
            return input.Split('\n');
        }

        public static string ArrayToString(string[] array)
        {
            string output = "";

            for (int i = 0; i < array.Length - 1; i++)
                output += array[i] + '\n';

            output += array[^1];

            return output;
        }

        public static int MaxLength(string[] array)
        {
            int max = 0;

            for (int i = 0; i < array.Length; i++)
                max = Math.Max(max, array[i].Length);

            return max;
        }
    }
}
