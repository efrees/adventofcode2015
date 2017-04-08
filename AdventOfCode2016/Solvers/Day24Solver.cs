using System;
using AdventOfCode2016.Solvers.Day24Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day24Solver
    {
        public static Day24Solver Create()
        {
            return new Day24Solver();
        }

        public int GetSolution(string inputText)
        {
            var graph = Day24Graph.ParseFromInput(inputText);

            var bestPath = graph.GetBestOrderToVisitAllNodesFromZero();
            return graph.GetCostOfPath(bestPath);
        }
    }
}