using System.Linq;
using AdventOfCode2016.Solvers.Day15Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day15Solver
    {
        public static Day15Solver CreateForPart1()
        {
            return new Day15Solver();
        }

        public static Day15Solver CreateForPart2()
        {
            var solver = new Day15Solver();
            solver.RingToAppend = new RingDefinition
            {
                Positions = 11,
                StartingOffset = 0
            };
            return solver;
        }

        public RingDefinition RingToAppend { get; private set; }

        public int GetSolution(string fileText)
        {
            var rings = fileText.SplitIntoLines()
                .Select(RingDefinition.Create)
                .ToList();

            if (RingToAppend != null)
            {
                RingToAppend.DiscNumber = rings.Count + 1;
                rings.Add(RingToAppend);
            }

            var startTime = 0;
            while (rings.Any(r => 0 != r.GetPositionAtTime(startTime + r.DiscNumber)))
            {
                startTime++;
            }
            return startTime;
        }
    }
}