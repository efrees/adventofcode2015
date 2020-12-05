using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Searchers
{
    internal class CartesianNode_should_
    {
        [Test]
        public void always_have_four_child_nodes()
        {
            var testNode = Any.CartesianNode();

            var childNodes = testNode.GetChildren();

            Assert.AreEqual(4, childNodes.Count());
        }
    }
}
