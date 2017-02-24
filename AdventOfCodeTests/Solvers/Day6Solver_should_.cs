using System;
using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day6Solver_should_
    {
        [Test]
        public void return_input_word_if_single_line()
        {
            var inputLine = "qeklcsias";

            var actualResult = Day6Solver.CreateForPart1().GetSolution(inputLine);

            Assert.AreEqual(inputLine, actualResult);
        }

        [Test]
        public void return_most_common_letter_for_each_position_for_part_1()
        {
            var inputText = "xbcdxf" + Environment.NewLine +
                            "axcxex" + Environment.NewLine +
                            "abxdef";

            var expectedResult = "abcdef";

            var actualResult = Day6Solver.CreateForPart1().GetSolution(inputText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void return_least_common_letter_for_each_position_for_part_2()
        {
            var inputText = "xbcdzf" + Environment.NewLine +
                            "axcyez" + Environment.NewLine +
                            "abydef";

            var expectedResult = "xxyyzz";

            var actualResult = Day6Solver.CreateForPart2().GetSolution(inputText);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
