using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2016.Solvers
{
    internal class Day3Solver
    {
        public static Day3Solver CreateForPart1()
        {
            return new Day3Solver();
        }

        private Day3Solver() { }

        public int GetSolution(string fileText)
        {
            var splitLines = fileText.Trim().Split('\n');

            var possibleCount = 0;

            foreach (var line in splitLines)
            {
                var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var integers = words.Select(w => int.Parse(w)).ToArray();
                Array.Sort(integers);

                if (integers.Length < 3)
                    Debug.WriteLine($"Looks like invalid input: {line}");

                if (integers[0] + integers[1] > integers[2])
                    possibleCount++;
            }

            return possibleCount;
        }
    }
}