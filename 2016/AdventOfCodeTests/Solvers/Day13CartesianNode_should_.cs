using System.Linq;
using AdventOfCode2016.Searchers;
using AdventOfCode2016.Solvers.Day13Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day13CartesianNode_should_
    {
        [Test]
        public void return_correct_child_nodes_for_origin()
        {
            var testNode = new Day13CartesianNode(0, 0, 10);

            var childNodes = testNode.GetChildren();

            Assert.AreEqual(1, childNodes.Count(), "Single child");
            Assert.AreEqual(new CartesianNode(0, 1), childNodes.First(), "Correct child");
        }

        [Test]
        public void return_correct_child_nodes_for_4_2()
        {
            var testNode = new Day13CartesianNode(4, 2, 10);

            var childNodes = testNode.GetChildren();

            Assert.AreEqual(2, childNodes.Count(), "Two children");
            CollectionAssert.Contains(childNodes, new CartesianNode(4, 1), "Should contain (4, 1)");
            CollectionAssert.Contains(childNodes, new CartesianNode(3, 2), "Should contain (3, 2)");
        }

        [Test]
        public void return_correct_child_nodes_for_enclosed_space()
        {
            var testNode = new Day13CartesianNode(5, 3, 10);

            var childNodes = testNode.GetChildren();

            CollectionAssert.IsEmpty(childNodes, "No unblocked child");
        }

    }
}
