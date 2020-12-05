using System.Collections.Generic;

namespace AdventOfCode2016.Searchers
{
    internal class CartesianNode : SearchNode<CartesianNode>
    {
        public CartesianNode(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public override IEnumerable<CartesianNode> GetChildren()
        {
            return new[]
            {
                new CartesianNode(X-1, Y),
                new CartesianNode(X+1, Y),
                new CartesianNode(X, Y-1),
                new CartesianNode(X, Y+1)
            };
        }
        
        public override bool Equals(object obj)
        {
            var other = obj as CartesianNode;
            if (other != null)
            {
                return X == other.X && Y == other.Y;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X * 97 + Y;
        }
    }
}