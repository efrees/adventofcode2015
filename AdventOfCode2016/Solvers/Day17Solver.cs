using System;
using System.Linq;
using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers.Day17Classes;

namespace AdventOfCode2016.Solvers
{
    internal abstract class Day17Solver
    {
        protected string _saltInput;

        public static Day17Solver CreateForPart1()
        {
            return new Day17Part1Solver("dmypynyp");
        }

        public static Day17Solver CreateForPart2()
        {
            return new Day17Part2Solver("dmypynyp");
        }

        protected Day17Solver(string saltInput)
        {
            this._saltInput = saltInput;
        }

        public abstract string GetSolution();
    }

    internal class Day17Part1Solver : Day17Solver
    {
        public Day17Part1Solver(string saltInput) : base(saltInput)
        {
        }

        public override string GetSolution()
        {
            var startNode = new Day17SearchNode(_saltInput, 0, 0);
            var heuristic = new Day17SearchHeuristic();
            var searcher = new AStarSearcher<CartesianNode>(startNode, heuristic);
            var path = searcher.GetShortestPathToNode(new CartesianNode(3, 3));
            return Day17PathFormatter.GetDirectionStringFromPath(path);
        }
    }
    internal class Day17Part2Solver : Day17Solver
    {
        public Day17Part2Solver(string saltInput) : base(saltInput)
        {
        }

        public override string GetSolution()
        {
            var startNode = new Day17SearchNode(_saltInput, 0, 0);
            var searcher = new ExhaustiveSearcher<CartesianNode>(startNode);
            var allPaths = searcher.FindAllPaths(new CartesianNode(3, 3));
            var maxPathLength = allPaths.Max(p => p.Count - 1);
            return maxPathLength.ToString();
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