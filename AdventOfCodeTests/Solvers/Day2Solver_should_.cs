using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day2Solver_should_
    {
        [TestCase("U", 1)]
        [TestCase("U\nD\nU\n", 3)]
        [TestCase("U\nD\r\nU\nR", 4)]
        public void return_a_digit_for_each_line(string inputLines, int expectedNumberOfDigits)
        {
            var actualResult = Day2Solver.CreateForPart1().GetSolution(inputLines);

            Assert.AreEqual(expectedNumberOfDigits, actualResult.ToString().Length);
        }

        [Test]
        public void start_processing_from_five()
        {
            var expectedResult = "5";
            var actualResult = Day2Solver.CreateForPart1().GetSolution("UD");
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("UR", "3")]
        [TestCase("DL", "7")]
        [TestCase("U\nR\nDD\nLL\r\nUU", "23971")]
        public void apply_rectangular_keypad_shape_in_part_1(string inputLines, string expectedResult)
        {
            var actualResult = Day2Solver.CreateForPart1().GetSolution(inputLines);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("ULD", "5")]
        [TestCase("RRUU\nL", "11")]
        [TestCase("ULL\nRRDDD\nLURDL\nUUUUD", "5DB3")]
        public void apply_diamond_keypad_shape_in_part_2(string inputLines, string expectedResult)
        {
            var actualResult = Day2Solver.CreateForPart2().GetSolution(inputLines);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
