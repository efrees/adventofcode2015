using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day20Solver_should_
    {
        [Test]
        public void return_zero_if_not_blacklisted()
        {
            var actualResult = Day20Solver.CreateForPart1().GetSolution("");

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void return_lowest_available_number()
        {
            var input = "0-2";

            var actualResult = Day20Solver.CreateForPart1().GetSolution(input);

            Assert.AreEqual(3, actualResult);
        }

        [Test]
        public void consider_multiple_blacklist_ranges()
        {
            var input = "7-10\n0-4\n4-5";
            var expected = 6;

            var actual = Day20Solver.CreateForPart1().GetSolution(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void handle_full_uint_range()
        {
            var input = "0-100\n102-4294967295";
            var expected = 101;

            var actual = Day20Solver.CreateForPart1().GetSolution(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void return_allowed_count_of_zero_when_all_excluded_for_part_2()
        {
            var input = "0-4294967295";

            var actual = Day20Solver.CreateForPart2().GetSolution(input);

            Assert.AreEqual(0, actual);
        }

        [TestCase("0-99\n200-4294967295", 100u)]
        [TestCase("0-99\n200-100000000\n100000002-4294967295", 101u)]
        public void return_allowed_count_for_part_2(string input, uint expected)
        {
            var actual = Day20Solver.CreateForPart2().GetSolution(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
