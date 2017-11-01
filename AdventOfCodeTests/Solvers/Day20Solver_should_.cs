using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day20Solver_should_
    {
        [Test]
        public void return_zero_if_not_blacklisted()
        {
            var actualResult = Day20Solver.Create().GetSolution("");

            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void return_lowest_available_number()
        {
            var input = "0-2";

            var actualResult = Day20Solver.Create().GetSolution(input);

            Assert.AreEqual(3, actualResult);
        }

        [Test]
        public void consider_multiple_blacklist_ranges()
        {
            var input = "7-10\n0-4\n4-5";
            var expected = 6;

            var actual = Day20Solver.Create().GetSolution(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void handle_full_uint_range()
        {
            var input = "0-100\n102-4294967295";
            var expected = 101;

            var actual = Day20Solver.Create().GetSolution(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
