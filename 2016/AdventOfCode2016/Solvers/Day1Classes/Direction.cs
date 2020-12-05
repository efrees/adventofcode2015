namespace AdventOfCode2016.Solvers.Day1Classes
{
    internal class Direction
    {
        private readonly int _horizontalMultiplier;
        private readonly int _verticalMultiplier;

        public static Direction North { get; } = new Direction(0, 1);
        public static Direction South { get; } = new Direction(0, -1);
        public static Direction East { get; } = new Direction(1, 0);
        public static Direction West { get; } = new Direction(-1, 0);

        private Direction(int horizontalMultiplier, int verticalMultiplier)
        {
            this._horizontalMultiplier = horizontalMultiplier;
            this._verticalMultiplier = verticalMultiplier;
        }

        public Direction GetRightTurnDirection()
        {
            if (this == North) return East;
            if (this == East) return South;
            if (this == South) return West;
            return North;
        }

        public Direction GetLeftTurnDirection()
        {
            if (this == North) return West;
            if (this == West) return South;
            if (this == South) return East;
            return North;
        }

        public BlockLocation GetMovement(int distance)
        {
            return new BlockLocation
            {
                X = _horizontalMultiplier * distance,
                Y = _verticalMultiplier * distance
            };
        }
    }
}