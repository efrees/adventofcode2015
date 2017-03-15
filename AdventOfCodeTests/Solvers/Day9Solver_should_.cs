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
            var actualResult = Day9Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(expectedDecompressedLength, actualResult);
        }

        [TestCase("A(1x5)BC", 7)]
        [TestCase("(3x3)XYZ", 9)]
        [TestCase("A(2x2)BCD(2x2)EFG", 11)]
        [TestCase("(6x1)(1x3)A", 6)]
        [TestCase("X(8x2)(3x3)ABCY", 18)]
        public void return_count_of_characters_in_example_containing_repeats(string inputLine, int expectedDecompressedLength)
        {
            var actualResult = Day9Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(expectedDecompressedLength, actualResult);
        }

        [TestCase("X(8x2)(3x3)ABCY", 20)]
        [TestCase("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", 445)]
        [TestCase("(27x12)(20x12)(13x14)(7x10)(1x12)A", 241920)]
        public void expand_nested_markers_in_example_containing_repeats_in_part_2(string inputLine, int expectedDecompressedLength)
        {
            var actualResult = Day9Solver.CreateForPart2().GetSolution(inputLine);
            Assert.AreEqual(expectedDecompressedLength, actualResult);
        }

        [Ignore("The given input doesn't include the self-referencing repeat sections demonstrated here.")]
        [TestCase("(11x3)(1x3)a(6x3)bcdefg", 39)]
        public void handle_complicated_case_that_is_not_intended_to_work(string inputLine, int expectedDecompressedLength)
        {
            var actualResult = Day9Solver.CreateForPart2().GetSolution(inputLine);
            Assert.AreEqual(expectedDecompressedLength, actualResult);
        }
    }
}
