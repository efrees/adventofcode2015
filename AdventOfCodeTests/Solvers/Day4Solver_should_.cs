using System;
using AdventOfCode2016.Solvers;
using Moq;
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
            var actualResult = Day4Solver.Create().GetSolution(inputLine);
            Assert.AreEqual(expectedSectorId, actualResult);
        }

        [TestCase("totally-real-room-200[decoy]")]
        public void return_zero_for_single_invalid_sector(string inputLine)
        {
            var actualResult = Day4Solver.Create().GetSolution(inputLine);
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
            var actualResult = Day4Solver.Create().GetSolution(inputText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void report_sector_id_of_room_with_north_pole_objects()
        {
            var inputText = "aaaaa-bbb-z-y-x-123[abxyz]" + Environment.NewLine +
                            "a-b-c-d-e-f-g-h-987[abcde]" + Environment.NewLine +
                            "kloqemlib-lygbzq-pqloxdb-991[lbqod]";
            var expectedResult = 991;
            var solverHarness = new Mock<Day4Solver>() { CallBase = true };

            solverHarness.Object.GetSolution(inputText);

            solverHarness.Verify(h => h.ReportSectorIdOfNorthPoleObjectStorage(It.IsAny<int>()), Times.Once);
            solverHarness.Verify(h => h.ReportSectorIdOfNorthPoleObjectStorage(expectedResult), Times.Once);
        }
    }
}
