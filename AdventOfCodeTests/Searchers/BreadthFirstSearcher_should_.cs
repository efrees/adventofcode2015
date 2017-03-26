using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Searchers;
using NUnit.Framework;

namespace AdventOfCodeTests.Searchers
{
    internal class BreadthFirstSearcher_should_
    {
        [Test]
        public void visit_start_node_when_searching_for_shortest_path()
        {
            var startNode = new TestNode();

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            searcher.GetShortestPathToNode(new TestNode());

            CollectionAssert.Contains(searcher.VisitedNodes, startNode);
        }

        [Test]
        public void visit_children_of_start_node_when_target_is_not_found()
        {
            var expectedChildNodes = Any.ListOf(() => new TestNode(), Any.IntBetween(1, 100));
            var startNode = new TestNode
            {
                ChildNodes = expectedChildNodes
            };

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            searcher.GetShortestPathToNode(new TestNode());

            CollectionAssert.IsSubsetOf(expectedChildNodes, searcher.VisitedNodes, "All children should be visited.");
        }

        [Test]
        public void not_visit_children_of_target_node()
        {
            var childrenOfTargetNode = Any.ListOf(() => new TestNode(), Any.IntBetween(1, 100));
            var targetNode = new TestNode
            {
                ChildNodes = childrenOfTargetNode
            };
            var startNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            searcher.GetShortestPathToNode(targetNode);

            Assert.IsTrue(childrenOfTargetNode.All(n => !searcher.VisitedNodes.Contains(n)),
                "No child of target node should be visited.");
        }

        [Test]
        public void match_equivalent_target_node_even_if_different_instance()
        {
            var childrenOfTargetNode = Any.ListOf(() => new TestNode(), Any.IntBetween(1, 100));
            var targetNode = new TestNode
            {
                ChildNodes = childrenOfTargetNode
            };
            var startNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };

            var matchingTargetNode = TestNode.CreateNodeEqualTo(targetNode);

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            searcher.GetShortestPathToNode(matchingTargetNode);

            Assert.IsTrue(childrenOfTargetNode.All(n => !searcher.VisitedNodes.Contains(n)),
                "No child of target node should be visited.");
        }

        [Test]
        public void return_path_to_target_node_if_found()
        {
            var targetNode = new TestNode();
            var middleNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };
            var startNode = new TestNode
            {
                ChildNodes = new[] { new TestNode(), new TestNode(), middleNode, new TestNode() }
            };

            var expectedPath = new[] { startNode, middleNode, targetNode };

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            var actualPath = searcher.GetShortestPathToNode(targetNode);

            CollectionAssert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void return_empty_path_if_target_node_not_found()
        {
            var targetNode = new TestNode();
            var startNode = new TestNode
            {
                ChildNodes = new[] { new TestNode() }
            };

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            var actualPath = searcher.GetShortestPathToNode(targetNode);

            CollectionAssert.IsEmpty(actualPath);
        }

        [Test]
        public void not_visit_already_visited_node()
        {
            var startNode = new TestNode();
            var adjacentNode = new TestNode();
            var duplicateStartNode = TestNode.CreateNodeEqualTo(startNode);
            var unprocessedNode = new TestNode();

            startNode.ChildNodes = new[] { adjacentNode };
            adjacentNode.ChildNodes = new[] { duplicateStartNode };
            duplicateStartNode.ChildNodes = new[] { unprocessedNode };

            var searcher = new BreadthFirstSearcher<TestNode>(startNode);
            searcher.GetShortestPathToNode(new TestNode());

            var pathTraversedToUnprocessedNode = unprocessedNode.GetPathToHere();
            CollectionAssert.AreEqual(new[] { unprocessedNode }, pathTraversedToUnprocessedNode,
                "Not having any parent set confirms the parent, duplicateStartNode, was never visited.");
        }

        [Test]
        public void not_visit_node_beyond_depth_limit()
        {
            var targetNode = new TestNode();
            var middleNode = new TestNode
            {
                ChildNodes = new[] { targetNode }
            };
            var startNode = new TestNode
            {
                ChildNodes = new[] { middleNode }
            };

            var searcher = new BreadthFirstSearcher<TestNode>(startNode, 1);
            var actualResult = searcher.GetShortestPathToNode(targetNode);

            CollectionAssert.IsEmpty(actualResult, "Search should not be successful.");
            Assert.AreEqual(2, searcher.VisitedNodes.Count(), "Only two nodes should have been reached.");
        }
    }

    internal class TestNode : SearchNode<TestNode>
    {
        public int Identifier { get; } = Any.Integer();

        public IEnumerable<TestNode> ChildNodes { get; set; } = new TestNode[] { };

        public static TestNode CreateNodeEqualTo(TestNode node)
        {
            return new TestNode(node.Identifier);
        }

        public TestNode() { }

        private TestNode(int identifier)
        {
            Identifier = identifier;
        }

        public override IEnumerable<TestNode> GetChildren()
        {
            return ChildNodes;
        }

        public override bool Equals(object obj)
        {
            var other = obj as TestNode;
            if (other != null)
            {
                return Identifier.Equals(other.Identifier);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}
