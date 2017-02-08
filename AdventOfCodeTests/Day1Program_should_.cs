using System;
using AdventOfCode2016;
using NUnit.Framework;

namespace AdventOfCodeTests
{
    internal class Day1Program_should_
    {
        [Test]
        public void get_correct_final_location_for_single_instruction_with_right_turn()
        {
            var movementDistance = Any.MovementDistance();
            var instruction = $"R{movementDistance}";

            var actualResult = Program.GetRelativeLocationFromDirections(new[] { instruction });

            Assert.AreEqual(movementDistance, actualResult.X);
            Assert.AreEqual(0, actualResult.Y);
        }

        [Test]
        public void get_correct_final_location_for_single_instruction_with_left_turn()
        {
            var movementDistance = Any.MovementDistance();
            var instruction = $"L{movementDistance}";

            var actualResult = Program.GetRelativeLocationFromDirections(new[] { instruction });

            Assert.AreEqual(-movementDistance, actualResult.X);
        }

        [Test]
        public void get_correct_final_location_for_given_example()
        {
            var instructions = new[] { "R2", "R2", "R2" };

            var actualResult = Program.GetRelativeLocationFromDirections(instructions);

            Assert.AreEqual(-2, actualResult.Y);
            Assert.AreEqual(0, actualResult.X);
        }
    }

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
