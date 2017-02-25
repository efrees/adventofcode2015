using System;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day7Solver
    {
        public static Day7Solver Create()
        {
            return new Day7Solver();
        }

        public int GetSolution(string fileText)
        {
            return fileText.SplitIntoLines()
                .Count(line => ContainsAbba(line) && DoesNotHaveAbbaInBrackets(line));
        }

        private bool DoesNotHaveAbbaInBrackets(string line)
        {
            foreach (var segment in line.Split('['))
            {
                var closeOfSegment = segment.IndexOf(']');
                if (closeOfSegment > 0 && ContainsAbba(segment.Substring(0, closeOfSegment)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ContainsAbba(string line)
        {
            for (var i = 0; i < line.Length - 3; i++)
            {
                if (line[i] == line[i + 3]
                    && line[i + 1] == line[i + 2]
                    && line[i] != line[i + 1])
                    return true;
            }
            return false;
        }
    }
}