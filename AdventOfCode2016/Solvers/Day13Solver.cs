using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers.Day13Classes;

namespace AdventOfCode2016
{
    internal abstract class Day13Solver
    {
        internal BreadthFirstSearcher<CartesianNode> GridSearcher;

        public Day13Solver(BreadthFirstSearcher<CartesianNode> gridSearcher)
        {
            GridSearcher = gridSearcher;
        }

        public static Day13Solver CreateForPart1()
        {
            var startNode = new Day13CartesianNode(1, 1, 1350);
            return new Day13Part1Solver(new BreadthFirstSearcher<CartesianNode>(startNode));
        }

        public static Day13Solver CreateForPart2()
        {
            var startNode = new Day13CartesianNode(1, 1, 1350);
            return new Day13Part2Solver(new BreadthFirstSearcher<CartesianNode>(startNode, 50));
        }

        public abstract int GetSolution();
    }

    internal class Day13Part1Solver : Day13Solver
    {
        private int _targetX = 31;
        private int _targetY = 39;

        public Day13Part1Solver(BreadthFirstSearcher<CartesianNode> gridSearcher) : base(gridSearcher)
        {
        }

        public override int GetSolution()
        {
            var targetNode = new CartesianNode(_targetX, _targetY);
            var shortestPath = GridSearcher.GetShortestPathToNode(targetNode);
            return shortestPath.Count - 1;
        }
    }

    internal class Day13Part2Solver : Day13Solver
    {
        public Day13Part2Solver(BreadthFirstSearcher<CartesianNode> gridSearcher) : base(gridSearcher)
        {
        }

        public override int GetSolution()
        {
            // Exhaust the depth limit by searching for an unreachable node.
            var unreachableNode = new CartesianNode(-1, -1);
            GridSearcher.GetShortestPathToNode(unreachableNode);
            return GridSearcher.VisitedNodes.Count;
        }
    }
}