using System;

namespace AdventOfCodeTests
{
    internal class Any
    {
        private static Random _random = new Random();

        public static bool Boolean()
        {
            return _random.Next(0, 100) < 50;
        }

        public static char DirectionCharacter()
        {
            return Boolean() ? 'L' : 'R';
        }

        public static int MovementDistance()
        {
            return _random.Next(1, 1000);
        }
    }
}