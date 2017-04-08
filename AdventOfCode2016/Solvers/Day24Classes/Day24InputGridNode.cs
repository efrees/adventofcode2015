using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Searchers;

namespace AdventOfCode2016.Solvers.Day24Classes
{
    internal class Day24InputGridNode : CartesianNode
    {
        private readonly string[] _inputGrid;

        public Day24InputGridNode(string[] inputGrid, int x, int y) : base(x, y)
        {
            _inputGrid = inputGrid;
        }

        public override IEnumerable<CartesianNode> GetChildren()
        {
            var childCandidates = base.GetChildren();

            return childCandidates
                .Where(NodeIsOpenSpace)
                .Select(node => new Day24InputGridNode(_inputGrid, node.X, node.Y));
        }

        private bool NodeIsOpenSpace(CartesianNode arg)
        {
            if (arg.X < 0 || arg.Y < 0
                || arg.Y >= _inputGrid.Length
                || arg.X >= _inputGrid[0].Length)
            {
                return false;
            }

            return _inputGrid[arg.Y][arg.X] != '#';
        }

    }
}
