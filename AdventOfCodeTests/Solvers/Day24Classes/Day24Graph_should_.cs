using System.Linq;
using AdventOfCode2016.Solvers.Day24Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers.Day24Classes
{
    internal class Day24Graph_should_
    {
        private string _exampleInputMaze = @"###########
#0.1.....2#
#.#######.#
#4.......3#
###########";

        [Test]
        public void create_nodes_from_each_number()
        {
            var inputMaze = _exampleInputMaze;
            var graph = Day24Graph.ParseFromInput(inputMaze);

            Assert.IsNotNull(graph.Nodes);
            CollectionAssert.AreEquivalent(new[] { 0, 1, 2, 3, 4 }, graph.Nodes.Select(n => n.Identifier));
        }

        [Test]
        public void compute_distances_between_each_node_pair()
        {
            var inputMaze = _exampleInputMaze;
            var graph = Day24Graph.ParseFromInput(inputMaze);

            Assert.AreEqual(2, graph.DistanceBetween(0, 1), "0 <--> 1");
            Assert.AreEqual(8, graph.DistanceBetween(0, 2), "0 <--> 2");
            Assert.AreEqual(8, graph.DistanceBetween(0, 2), "1 <--> 3");
        }

        [Test]
        public void find_best_order_to_visit_nodes()
        {
            var inputMaze = _exampleInputMaze;
            var graph = Day24Graph.ParseFromInput(inputMaze);

            var expectedNodeOrder = new[] { 0, 4, 1, 2, 3 };
            var actualNodeOrder = graph.GetBestOrderToVisitAllNodesFromZero();
            CollectionAssert.AreEqual(expectedNodeOrder, actualNodeOrder.Select(n => n.Identifier));
        }
    }
}
