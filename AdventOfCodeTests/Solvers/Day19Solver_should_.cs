using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day19Solver_should_
    {
        [Test]
        public void start_from_first_elf()
        {
            var numberOfElves = 2;
            var expectedWinner = 1;

            var actualWinner = new Day19Part1Strategy(numberOfElves).GetSolution();

            Assert.AreEqual(expectedWinner, actualWinner);
        }

        [TestCase(3, 3)]
        [TestCase(4, 1)]
        [TestCase(5, 3)]
        [TestCase(6, 5)]
        [TestCase(11, 7)]
        [TestCase(12, 9)]
        public void continue_around_the_circle_for_part_1(int numberOfElves, int expectedWinner)
        {
            var actualWinner = new Day19Part1Strategy(numberOfElves).GetSolution();

            Assert.AreEqual(expectedWinner, actualWinner);
        }

        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 5)]
        public void eliminate_elves_across_the_table_for_part_2(int numberOfElves, int expectedWinner)
        {
            var actualWinner = new Day19Part2Strategy(numberOfElves).GetSolution();

            Assert.AreEqual(expectedWinner, actualWinner);
        }
    }
}
