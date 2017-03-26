using System;
using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers.Day13Classes;

namespace AdventOfCode2016
{
    internal class Day13Solver
    {
        internal BreadthFirstSearcher<CartesianNode> GridSearcher;
        private int _targetX = 31;
        private int _targetY = 39;

        public Day13Solver(BreadthFirstSearcher<CartesianNode> gridSearcher)
        {
            GridSearcher = gridSearcher;
        }

        public static Day13Solver CreateForPart1()
        {
            var startNode = new Day13CartesianNode(1, 1, 1350);
            return new Day13Solver(new BreadthFirstSearcher<CartesianNode>(startNode));
        }

        public int GetSolution()
        {
            var targetNode = new CartesianNode(_targetX, _targetY);
            var shortestPath = GridSearcher.GetShortestPathToNode(targetNode);
            return shortestPath.Count - 1;
        }
    }
}