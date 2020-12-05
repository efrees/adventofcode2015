using System.Linq;
using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers.Day17Classes;
using NUnit.Framework;

namespace AdventOfCodeTests
{
    internal class Day17SearchNode_should_
    {
        [Test]
        public void correctly_compute_connected_nodes_using_salt()
        {
            var salt = "hijkl";

            var firstNode = new Day17SearchNode(salt, 0, 0);
            var children = firstNode.GetChildren();

            Assert.AreEqual(1, children.Count());
            Assert.AreEqual(0, children.First().X, "X");
            Assert.AreEqual(1, children.First().Y, "Y");
        }

        [Test]
        public void correctly_compute_connected_nodes_using_salt_and_path()
        {
            var salt = "hijkl";

            var parentNode = new Day17SearchNode(salt, 0, 0);
            var firstNode = new Day17SearchNode(salt, 0, 1);
            firstNode.StoreParentNode(parentNode);

            var children = firstNode.GetChildren();

            Assert.AreEqual(2, children.Count());
            var expectedChildren = new[] { new CartesianNode(0, 0), new CartesianNode(1, 1) };
            CollectionAssert.AreEquivalent(expectedChildren, children);
        }

        [Test]
        public void handle_given_examples_correctly()
        {
            var node = new Day17SearchNode("hijklDR", 1, 1);
            CollectionAssert.IsEmpty(node.GetChildren());

            node = new Day17SearchNode("hijklDU", 0, 0);
            CollectionAssert.Contains(node.GetChildren(), new CartesianNode(1, 0));
        }
    }
}
