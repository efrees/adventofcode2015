using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Searchers;

namespace AdventOfCode2016.Solvers.Day17Classes
{
    internal class Day17SearchNode : CartesianNode
    {
        private readonly string _salt;
        private const int UP_INDEX = 0;
        private const int DOWN_INDEX = 1;
        private const int LEFT_INDEX = 2;
        private const int RIGHT_INDEX = 3;

        public Day17SearchNode(string salt, int x, int y) : base(x, y)
        {
            _salt = salt;
        }

        public override IEnumerable<CartesianNode> GetChildren()
        {
            var indicator = GetDoorIndicatorUsingHash();
            var candidates = base.GetChildren();
            return candidates
                .Where(CoordinatesAreWithin4X4Grid)
                .Where(n => DoorToNodeIsOpen(n, indicator))
                .Select(n => new Day17SearchNode(_salt, n.X, n.Y));
        }

        private string GetDoorIndicatorUsingHash()
        {
            var path = GetPathToHere();
            var directionString = Day17PathFormatter.GetDirectionStringFromPath(path);

            var data = _salt + directionString;
            var hasher = new Md5Hasher();
            var hashedData = hasher.HashDataAsHexString(data);
            return hashedData.Substring(0, 4);
        }

        private static bool CoordinatesAreWithin4X4Grid(CartesianNode arg)
        {
            return arg.X >= 0 && arg.X < 4
                   && arg.Y >= 0 && arg.Y < 4;
        }

        private bool DoorToNodeIsOpen(CartesianNode other, string indicator)
        {
            if (Y > other.Y)
                return CharIndicatesOpenDoor(indicator[UP_INDEX]);
            if (Y < other.Y)
                return CharIndicatesOpenDoor(indicator[DOWN_INDEX]);
            if (X > other.X)
                return CharIndicatesOpenDoor(indicator[LEFT_INDEX]);
            return CharIndicatesOpenDoor(indicator[RIGHT_INDEX]);
        }

        private bool CharIndicatesOpenDoor(char c)
        {
            return char.IsLetter(c) && c != 'a' && c != 'A';
        }
    }
}
