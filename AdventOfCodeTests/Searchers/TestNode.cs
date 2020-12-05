using System.Collections.Generic;
using AdventOfCode2016.Searchers;

namespace AdventOfCodeTests.Searchers
{
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