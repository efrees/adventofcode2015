using System.Linq;
using AdventOfCode2016.Searchers;
using NUnit.Framework;

namespace AdventOfCodeTests.Searchers
{
    internal class ExhaustiveSearcher_should_
    {
        [Test]
        public void find_a_path_to_target_node_when_one_step_away()
        {
            var targetNode = new TestNode();
            var startNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };

            var searcher = new ExhaustiveSearcher<TestNode>(startNode);
            var paths = searcher.FindAllPaths(targetNode);

            Assert.AreEqual(1, paths.Count());
            CollectionAssert.AreEqual(new[] { startNode, targetNode }, paths.First());
        }

        [Test]
        public void not_include_paths_beyond_target_node()
        {
            var targetNode = new TestNode();
            var startNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };
            var otherTarget = TestNode.CreateNodeEqualTo(targetNode);
            targetNode.ChildNodes = new[] { otherTarget };

            var searcher = new ExhaustiveSearcher<TestNode>(startNode);
            var paths = searcher.FindAllPaths(targetNode);
            Assert.AreEqual(1, paths.Count());
            Assert.AreEqual(2, paths.First().Count);
        }

        [Test]
        public void return_multiple_paths()
        {
            var targetNode = new TestNode();
            var middleNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };
            var startNode = new TestNode
            {
                ChildNodes = new[] { targetNode, middleNode }
            };

            var searcher = new ExhaustiveSearcher<TestNode>(startNode);
            var paths = searcher.FindAllPaths(targetNode);

            Assert.AreEqual(2, paths.Count());
        }
    }
}
