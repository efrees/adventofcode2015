using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day19Solver
    {
        private readonly int _numberOfElves;

        public static Day19Solver CreateForPart1()
        {
            var inputValue = 3005290;
            return new Day19Solver(inputValue);
        }

        internal Day19Solver(int numberOfElves)
        {
            _numberOfElves = numberOfElves;
        }
        
        public int GetSolution()
        {
            var elves = Enumerable.Range(1, _numberOfElves).ToArray();
            var stepSize = 1;
            var shouldRemoveNext = false;
            var currentPosition = 0;
            var countEliminated = 0;
            while (_numberOfElves - countEliminated > 1)
            {
                if (shouldRemoveNext)
                {
                    elves[currentPosition] = 0;
                    countEliminated++;
                }
                currentPosition += stepSize;
                if (currentPosition >= elves.Length)
                {
                    stepSize *= 2;
                    currentPosition = GetFirstRemainingElfFromStart(elves);
                }
                shouldRemoveNext = !shouldRemoveNext;

            }
            return elves.FirstOrDefault(e => e != 0);
        }

        private static int GetFirstRemainingElfFromStart(IReadOnlyList<int> elves)
        {
            var nextPosition = 0;
            while (elves[nextPosition] == 0)
            {
                nextPosition++;
            }
            return nextPosition;
        }
    }
}