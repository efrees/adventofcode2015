using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Solutions
{
    public class Day5
    {
        private static readonly List<string> exclusions = new List<string>
        {
            "ab",
            "cd",
            "pq",
            "xy",
        };

        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day5input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            string input;

            var oldNiceCount = 0;
            var newNiceCount = 0;

            while (!string.IsNullOrEmpty(input = inputReader.ReadLine()))
            {
                input = input.ToLower();

                if (CheckThreeVowels(input)
                    && CheckRepeatedLetter(input)
                    && CheckExclusions(input))
                {
                    oldNiceCount++;
                }

                if (CheckRepeatedPairOfLetters(input)
                    && CheckSeparatedLetter(input))
                {
                    newNiceCount++;
                }
            }

            Console.WriteLine("Nice strings (old definition): " + oldNiceCount);
            Console.WriteLine("Nice strings (new definition): " + newNiceCount);
        }

        private static bool CheckExclusions(string input)
        {
            foreach (var ex in exclusions)
            {
                if (input.Contains(ex))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckRepeatedLetter(string input)
        {
            char last = (char)0;

            foreach (var ch in input)
            {
                if (ch == last)
                {
                    return true;
                }

                last = ch;
            }

            return false;
        }

        private static bool CheckThreeVowels(string input)
        {
            var vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };

            int count = 0;
            foreach (var ch in input)
            {
                if (vowels.Contains(ch))
                    count++;
            }

            return count >= 3;
        }

        private static bool CheckRepeatedPairOfLetters(string input)
        {
            //Rule: a pair of letters is repeated.
            for(int i = 0; i < input.Length - 3; i++)
            {
                var current = input.Substring(i, 2);

                if (input.Substring(i + 2).Contains(current))
                    return true;
            }

            return false;
        }

        private static bool CheckSeparatedLetter(string input)
        {
            //Rule: a letter is repeated with exactly one character between the two instances.
            for(int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2])
                    return true;
            }

            return false;
        }
    }
}
