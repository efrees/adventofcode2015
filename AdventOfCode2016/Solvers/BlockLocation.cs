namespace AdventOfCode2016.Solvers
{
    internal class BlockLocation
    {
        public BlockLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public BlockLocation() : this(0, 0)
        {
        }

        public int X { get; set; }
        public int Y { get; set; }

        public BlockLocation Move(BlockLocation movement)
        {
            return new BlockLocation(X + movement.X, Y + movement.Y);
        }
    }
}