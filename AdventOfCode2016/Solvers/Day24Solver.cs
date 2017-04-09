using AdventOfCode2016.Solvers.Day24Classes;

namespace AdventOfCode2016.Solvers
{
    internal abstract class Day24Solver
    {
        public static Day24Solver CreateForPart1()
        {
            return new Day24Part1Solver();
        }

        public static Day24Solver CreateForPart2()
        {
            return new Day24Part2Solver();
        }

        public abstract int GetSolution(string inputText);
    }

    internal class Day24Part1Solver : Day24Solver
    {
        public override int GetSolution(string inputText)
        {
            var graph = Day24Graph.ParseFromInput(inputText);
            var bestPath = graph.GetBestOrderToVisitAllNodesFromZero();
            return graph.GetCostOfPath(bestPath);
        }
    }

    internal class Day24Part2Solver : Day24Solver
    {
        public override int GetSolution(string inputText)
        {
            var graph = Day24Graph.ParseFromInput(inputText);
            var bestPath = graph.GetBestOrderToTourAllNodes();
            return graph.GetCostOfPath(bestPath);
        }
    }
}