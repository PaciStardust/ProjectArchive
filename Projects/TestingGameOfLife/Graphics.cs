using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.TestingGameOfLife
{
    static class Graphics
    {
        private static readonly Dictionary<char, string> UniDict = new Dictionary<char, string>
        {
            {'a', "11111-10001-11111-10001-10001" },
            {'b', "11110-10001-11110-10001-11110" },
            {'c', "01111-10000-10000-10000-01111" },
            {'d', "11110-10001-10001-10001-11110" },
            {'e', "11111-10000-11100-10000-11111" },
            {'f', "11111-10000-11100-10000-10000" },
            {'g', "11111-10000-10011-10001-11111" },
            {'h', "10001-10001-11111-10001-10001" },
            {'i', "11111-00100-00100-00100-11111" },
            {'j', "11111-00001-00001-10001-01110" },
            {'k', "10001-10010-11100-10010-10001" },
            {'l', "10000-10000-10000-10000-11111" },
            {'m', "10001-11011-10101-10001-10001" },
            {'n', "10001-11001-10101-10011-10001" },
            {'o', "01110-10001-10001-10001-01110" },
            {'p', "11111-10001-11111-10000-10000" },
            {'q', "01110-10001-10001-10011-01111" },
            {'r', "11111-10001-11111-10010-10001" },
            {'s', "11111-10000-11111-00001-11111" },
            {'t', "11111-00100-00100-00100-00100" },
            {'u', "10001-10001-10001-10001-01110" },
            {'v', "10001-10001-10001-01010-00100" },
            {'w', "10001-10001-10101-11011-10001" },
            {'x', "10001-01010-00100-01010-10001" },
            {'y', "10001-01010-00100-00100-00100" },
            {'z', "11111-00010-00100-01000-11111" },
            {'0', "01110-11001-10101-10011-01110" },
            {'1', "00011-00101-01001-10001-00001" },
            {'2', "01110-10001-00110-01000-11111" },
            {'3', "11110-00001-00110-00001-11110" },
            {'4', "10001-10001-11111-00001-00001" },
            {'5', "11111-01000-00110-10001-01110" },
            {'6', "01110-10000-11110-10001-01110" },
            {'7', "11111-00001-00010-00010-00010" },
            {'8', "01110-10001-01110-10001-01110" },
            {'9', "01110-10001-01111-00001-01110" },
            {'!', "00100-00100-00100-00000-00100" },
            {'?', "01110-00010-00100-00000-00100" },
            {':', "00000-00100-00000-00100-00000" },
            {'+', "00000-00100-01110-00100-00000" },
            {'-', "00000-00000-01110-00000-00000" },
            {'/', "00010-00100-00100-00100-01000" },
            {'\'', "01000-00100-00100-00100-00010" },
            {' ', "00000-00000-00000-00000-00000" },
        };

        public static string Unify(string input, bool fillempty, int padding)
        {
            string paddingText = "";

            if (padding != 0)
                for (int i = 0; i < padding; i++)
                    paddingText += "0";

            input = input.ToLower();
            string[] unistrings = new string[] { "0" + paddingText, "0" + paddingText, "0" + paddingText, "0" + paddingText, "0" + paddingText };

            for (int i = 0; i < input.Length; i++)
            {
                string[] sepratedstring;

                try
                {
                    sepratedstring = UniDict[input[i]].Split('-');
                }
                catch
                {
                    sepratedstring = "00000-00000-00000-00000-00000".Split('-');
                }

                for (int a = 0; a < 5; a++)
                    unistrings[a] += sepratedstring[a] + "0";
            }

            for (int i = 0; i < 5; i++)
                unistrings[i] += paddingText;

            string additive = "";
            for (int i = 0; i < unistrings[0].Length; i++)
                additive += "0";

            return $"{additive}\n{unistrings[0]}\n{unistrings[1]}\n{unistrings[2]}\n{unistrings[3]}\n{unistrings[4]}\n{additive}".Replace('0', fillempty ? '░' : ' ').Replace('1', '█');
        }

        public static string Boxify(string input, bool wide, bool tall)
        {
            int maxlength = 0;

            string[] boxText = input.Split('\n');

            for (int i = 0; i < boxText.Length; i++)
                maxlength = boxText[i].Length > maxlength ? boxText[i].Length : maxlength;

            int borderAdd = wide ? 2 : 0;
            int textAdd = wide ? 1 : 0;
            string spaceAdd = wide ? " " : "";

            string outText = "╔";
            for (int i = 0; i < maxlength + borderAdd; i++)
                outText += "═";
            outText += "╗\n";

            if (tall)
            {
                outText += "║";
                for (int i = 0; i < maxlength + borderAdd; i++)
                    outText += " ";
                outText += "║\n";
            }

            for (int i = 0; i < boxText.Length; i++)
            {
                outText += "║" + spaceAdd + boxText[i];
                for (int _i = 0; _i < maxlength - boxText[i].Length + textAdd; _i++)
                    outText += " ";
                outText += "║\n";
            }

            if (tall)
            {
                outText += "║";
                for (int i = 0; i < maxlength + borderAdd; i++)
                    outText += " ";
                outText += "║\n";
            }

            outText += "╚";
            for (int i = 0; i < maxlength + borderAdd; i++)
                outText += "═";
            outText += "╝\n";

            return outText;
        }
    }
}
