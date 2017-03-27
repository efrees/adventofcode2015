using System;
using System.Linq;
using AdventOfCodeTests.Solvers;

namespace AdventOfCode2016.Solvers
{
    internal class Day15Solver
    {
        public static Day15Solver Create()
        {
            return new Day15Solver();
        }

        public int GetSolution(string fileText)
        {
            var rings = fileText.SplitIntoLines()
                .Select(RingDefinition.Create)
                .ToList();

            var startTime = 0;
            while (rings.Any(r => 0 != r.GetPositionAtTime(startTime + r.DiscNumber)))
            {
                startTime++;
            }
            return startTime;
        }
    }
}