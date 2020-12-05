using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Searchers;

namespace AdventOfCode2016.Solvers.Day13Classes
{
    internal class Day13CartesianNode : CartesianNode
    {
        public Day13CartesianNode(int x, int y, int designerNumber) : base(x, y)
        {
            DesignerInputNumber = designerNumber;
        }

        public int DesignerInputNumber { get; }

        public override IEnumerable<CartesianNode> GetChildren()
        {
            var adjacentNodes = base.GetChildren();
            return adjacentNodes.Where(NodeIsOpenSpace)
                .Select(n => new Day13CartesianNode(n.X, n.Y, DesignerInputNumber));
        }

        private bool NodeIsOpenSpace(CartesianNode arg)
        {
            if (arg.X < 0 || arg.Y < 0)
                return false;

            var value = ComputeBaseFormula(arg) + DesignerInputNumber;
            var oneBitCount = CountOneBits(value);
            return oneBitCount % 2 == 0;
        }

        private int CountOneBits(int value)
        {
            var count = 0;
            while (value > 0)
            {
                value &= value - 1;
                count++;
            }
            return count;
        }

        private static int ComputeBaseFormula(CartesianNode node)
        {
            return node.X * node.X + 3 * node.X + 2 * node.X * node.Y + node.Y + node.Y * node.Y;
        }
    }
}