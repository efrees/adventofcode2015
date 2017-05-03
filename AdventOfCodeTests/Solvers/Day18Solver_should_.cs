using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day18Classes;
using Moq;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day18Solver_should_
    {
        [Test]
        public void generate_next_row_from_initial()
        {
            var expectedInitial = Any.AlphaNumericString();
            var rowCount = 2;
            var mockGenerator = new Mock<Day18RowGenerator>();

            new Day18Solver(mockGenerator.Object, rowCount)
                .GetSolution(expectedInitial);

            mockGenerator.Verify(g => g.GetNextRow(expectedInitial), Times.Once);
        }

        [Test]
        public void generate_requested_number_of_rows()
        {
            var rowCount = Any.IntBetween(1, 100);
            var mockGenerator = new Mock<Day18RowGenerator>();
            mockGenerator.Setup(g => g.GetNextRow(It.IsAny<string>())).Returns(Any.AlphaNumericString());

            new Day18Solver(mockGenerator.Object, rowCount)
                .GetSolution(Any.AlphaNumericString());

            var expectedGeneratorCount = rowCount - 1;
            mockGenerator.Verify(g => g.GetNextRow(It.IsAny<string>()), Times.Exactly(expectedGeneratorCount));
        }

        [Test]
        public void pass_previous_row_to_generate_next()
        {
            var expectedRow = Any.AlphaNumericString();
            var rowCount = 3;
            var mockGenerator = new Mock<Day18RowGenerator>();
            mockGenerator.Setup(g => g.GetNextRow(It.IsAny<string>())).Returns(expectedRow);

            new Day18Solver(mockGenerator.Object, rowCount)
                .GetSolution(Any.AlphaNumericString());

            mockGenerator.Verify(g => g.GetNextRow(expectedRow), Times.Once);
        }

        [Test]
        public void return_count_of_safe_tiles_across_all_rows()
        {
            var expectedRows = new[]
            {
                "...^^^",
                ".^.^.^",
                "^."
            };
            var expectedSafeTileCount = 7;
            var rowCount = 3;
            var mockGenerator = new Mock<Day18RowGenerator>();
            mockGenerator.SetupSequence(g => g.GetNextRow(It.IsAny<string>()))
                .Returns(expectedRows[1])
                .Returns(expectedRows[2]);

            var safeTileCount = new Day18Solver(mockGenerator.Object, rowCount)
                .GetSolution(expectedRows[0]);

            Assert.AreEqual(expectedSafeTileCount, safeTileCount);
        }
    }
}
