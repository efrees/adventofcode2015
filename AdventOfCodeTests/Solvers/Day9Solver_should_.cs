using AdventOfCode2016;
using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day9Solver_should_
    {
        [TestCase("ADVENT", 6)]
        [TestCase("ADVENT\n", 6)]
        public void return_count_of_characters_in_simple_example(string inputLine, int expectedDecompressedLength)
        {
            var actualResult = Day9Solver.Create().GetSolution(inputLine);
            Assert.AreEqual(expectedDecompressedLength, actualResult);
        }

        [TestCase("A(1x5)BC", 7)]
        [TestCase("(3x3)XYZ", 9)]
        [TestCase("A(2x2)BCD(2x2)EFG", 11)]
        [TestCase("(6x1)(1x3)A", 6)]
        [TestCase("X(8x2)(3x3)ABCY", 18)]
        public void return_count_of_characters_in_example_containing_repeats(string inputLine, int expectedDecompressedLength)
        {
            var actualResult = Day9Solver.Create().GetSolution(inputLine);
            Assert.AreEqual(expectedDecompressedLength, actualResult);
        }
    }
}
