using System.Collections;
using NUnit.Framework;

namespace AdventOfCodeTests.Searchers
{
    internal class SearchNode_should_
    {
        [Test]
        public void build_path_to_get_here_when_parents_have_been_stored()
        {
            var startNode = new TestNode();
            var middleNode = new TestNode();
            middleNode.StoreParentNode(startNode);
            var endNode = new TestNode();
            endNode.StoreParentNode(middleNode);

            var expectedPath = new[] { startNode, middleNode, endNode };
            var actualPath = endNode.GetPathToHere();

            CollectionAssert.AreEqual(expectedPath, actualPath);
        }
    }
}
