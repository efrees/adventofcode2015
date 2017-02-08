using System;

namespace AdventOfCode2016
{
    internal class BlockLocation
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BlockLocation Move(BlockLocation movement)
        {
            return new BlockLocation
            {
                X = X + movement.X,
                Y = Y + movement.Y
            };
        }
    }
}