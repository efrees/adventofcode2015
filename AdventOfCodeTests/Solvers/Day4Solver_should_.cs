using System;
using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day4Solver_should_
    {
        [TestCase("aaaaa-bbb-z-y-x-123[abxyz]", 123)]
        [TestCase("a-b-c-d-e-f-g-h-987[abcde]", 987)]
        [TestCase("not-a-real-room-404[oarel]", 404)]
        public void return_id_of_single_valid_sector(string inputLine, int expectedSectorId)
        {
            var actualResult = Day4Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(expectedSectorId, actualResult);
        }

        [TestCase("totally-real-room-200[decoy]")]
        public void return_zero_for_single_invalid_sector(string inputLine)
        {
            var actualResult = Day4Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void add_valid_ids_together_for_multiline_input()
        {
            var inputText = "aaaaa-bbb-z-y-x-123[abxyz]" + Environment.NewLine +
                            "a-b-c-d-e-f-g-h-987[abcde]" + Environment.NewLine +
                            "totally-real-room-200[decoy]" + Environment.NewLine +
                            "not-a-real-room-404[oarel]" + Environment.NewLine;
            var expectedResult = 123 + 987 + 404;
            var actualResult = Day4Solver.CreateForPart1().GetSolution(inputText);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
