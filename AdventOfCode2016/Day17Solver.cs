using System;
using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers.Day17Classes;

namespace AdventOfCode2016
{
    internal class Day17Solver
    {
        private string _saltInput;

        public static Day17Solver Create()
        {
            return new Day17Solver("dmypynyp");
        }

        public Day17Solver(string saltInput)
        {
            this._saltInput = saltInput;
        }

        public string GetSolution()
        {
            var startNode = new Day17SearchNode(_saltInput, 0, 0);
            var heuristic = new Day17SearchHeuristic();
            var searcher = new AStarSearcher<CartesianNode>(startNode, heuristic);
            var path = searcher.GetShortestPathToNode(new CartesianNode(3, 3));
            return Day17PathFormatter.GetDirectionStringFromPath(path);
        }
    }

    internal class Day17SearchHeuristic : ISearchHeuristic<CartesianNode>
    {
        public double EstimateSearchCost(CartesianNode startNode, CartesianNode targetNode)
        {
            return Math.Abs(targetNode.X - startNode.X)
                   + Math.Abs(targetNode.Y - startNode.Y);
        }
    }
}