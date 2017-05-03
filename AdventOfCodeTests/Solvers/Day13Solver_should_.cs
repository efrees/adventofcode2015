using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016;
using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day13Classes;
using Moq;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day13Solver_should_
    {
        [Test]
        public void configure_search_nodes_for_day_13_formula()
        {
            var solver = Day13Solver.CreateForPart1();
            var startNode = solver.GridSearcher.StartNode;
            Assert.IsInstanceOf<Day13CartesianNode>(startNode);
            Assert.AreEqual(1350, ((Day13CartesianNode)startNode).DesignerInputNumber);
        }

        [Test]
        public void find_length_of_shortest_path_to_coordinates_31_39()
        {
            var expectedStepCount = Any.IntBetween(1, 100);
            var pathLength = expectedStepCount + 1;
            var pathWithExpectedLength = Any.ListOf(Any.CartesianNode, pathLength);

            var mockSearcher = new Mock<BreadthFirstSearcher<CartesianNode>>(Any.CartesianNode());
            mockSearcher.Setup(s => s.GetShortestPathToNode(It.Is<CartesianNode>(n => n.X == 31 && n.Y == 39)))
                .Returns(pathWithExpectedLength);

            var solver = new Day13Part1Solver(mockSearcher.Object);
            var actualLength = solver.GetSolution();

            Assert.AreEqual(expectedStepCount, actualLength);
        }

        [Test]
        public void return_total_visited_node_count_in_part_2()
        {
            var expectedCount = Any.IntBetween(1, 100);
            var setWithExpectedCount = new HashSet<CartesianNode>();
            Enumerable.Range(1, expectedCount)
                .Select(x => new CartesianNode(x, 1))
                .ToList()
                .ForEach(node =>
                {
                    setWithExpectedCount.Add(node);
                });

            var mockSearcher = new Mock<BreadthFirstSearcher<CartesianNode>>(Any.CartesianNode());
            mockSearcher.Setup(s => s.VisitedNodes).Returns(setWithExpectedCount);

            var solver = new Day13Part2Solver(mockSearcher.Object);
            var actualCount = solver.GetSolution();

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
