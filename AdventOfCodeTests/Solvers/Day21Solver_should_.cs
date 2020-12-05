using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day21Solver_should_
    {
        [Test]
        public void swap_positions_on_command()
        {
            var input = "swap position 2 with position 0";

            var actualResult = new Day21Solver("abcd").GetSolution(input);

            Assert.AreEqual("cbad", actualResult);
        }

        [Test]
        public void swap_letters_on_command()
        {
            var input = "swap letter b with letter d";

            var actualResult = new Day21Solver("abcd").GetSolution(input);

            Assert.AreEqual("adcb", actualResult);
        }

        [Test]
        public void rotate_left_by_steps_on_command()
        {
            var input = "rotate left 2 steps";

            var actualResult = new Day21Solver("abcde").GetSolution(input);

            Assert.AreEqual("cdeab", actualResult);
        }

        [Test]
        public void rotate_right_by_steps_on_command()
        {
            var input = "rotate right 1 step";

            var actualResult = new Day21Solver("abcde").GetSolution(input);

            Assert.AreEqual("eabcd", actualResult);
        }

        [Test]
        public void rotate_based_on_letter_position_on_command()
        {
            var input = "rotate based on position of letter b";

            var actualResult = new Day21Solver("abcde").GetSolution(input);

            Assert.AreEqual("deabc", actualResult);
        }

        [Test]
        public void rotate_based_on_letter_position_at_least_four_on_command()
        {
            var input = "rotate based on position of letter e";

            var actualResult = new Day21Solver("abcde").GetSolution(input);

            Assert.AreEqual("eabcd", actualResult);
        }

        [Test]
        public void reverse_between_positions_on_command()
        {
            var input = "reverse positions 0 through 4";

            var actualResult = new Day21Solver("abcde").GetSolution(input);

            Assert.AreEqual("edcba", actualResult);
        }

        [Test]
        public void move_letter_to_new_position_on_command()
        {
            var input = "move position 1 to position 4";

            var actualResult = new Day21Solver("abcde").GetSolution(input);

            Assert.AreEqual("acdeb", actualResult);
        }

        [Test]
        public void reverse_commands_in_part_2()
        {
            var input = "swap position 1 with position 2\n" +
                        "swap position 2 with position 3";

            var actualResult = new Day21Part2Solver("abcde").GetSolution(input);

            Assert.AreEqual("adbce", actualResult);
        }

        [Test]
        public void move_in_opposite_direction_in_part_2()
        {
            var input = "move position 1 to position 4";

            var actualResult = new Day21Part2Solver("acdeb").GetSolution(input);

            Assert.AreEqual("abcde", actualResult);
        }

        [Test]
        public void reverse_rotation_direction_in_part_2()
        {
            var input = "rotate right 1 step";

            var actualResult = new Day21Part2Solver("abcde").GetSolution(input);

            Assert.AreEqual("bcdea", actualResult);
        }

        [Test]
        public void reverse_rotation_by_position_in_part_2()
        {
            var input = "rotate based on position of letter b";

            var actualResult = new Day21Part2Solver("deabc").GetSolution(input);

            Assert.AreEqual("abcde", actualResult);
        }

        [Test]
        public void reverse_rotation_based_on_letter_position_at_least_four_on_command()
        {
            var input = "rotate based on position of letter e";

            var actualResult = new Day21Part2Solver("cdefghab").GetSolution(input);

            Assert.AreEqual("abcdefgh", actualResult);
        }

    }
}
