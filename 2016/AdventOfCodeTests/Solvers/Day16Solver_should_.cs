using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day16Classes;
using Moq;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day16Solver_should_
    {
        [Test]
        public void expand_data_to_desired_length()
        {
            var expectedString = Any.AlphaNumericString();
            var expectedTargetLength = Any.IntBetween(1, 1000);

            var mockGenerator = new Mock<Day16DataGenerator>();
            var mockChecksumCreator = new Mock<Day16ChecksumCreator>();

            var solver = new Day16Solver(expectedString, expectedTargetLength, mockGenerator.Object, mockChecksumCreator.Object);
            solver.GetSolution();

            mockGenerator.Verify(g => g.ExpandData(expectedString, expectedTargetLength), Times.Once);
        }

        [Test]
        public void compute_checksum_of_expanded_data()
        {
            var expectedExpandedData = Any.AlphaNumericString();

            var mockGenerator = new Mock<Day16DataGenerator>();
            var mockChecksumCreator = new Mock<Day16ChecksumCreator>();

            mockGenerator.Setup(g => g.ExpandData(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(expectedExpandedData);

            var solver = new Day16Solver(Any.AlphaNumericString(), expectedExpandedData.Length, mockGenerator.Object, mockChecksumCreator.Object);
            solver.GetSolution();

            mockChecksumCreator.Verify(c => c.GetChecksum(expectedExpandedData), Times.Once);
        }

        [Test]
        public void truncate_data_to_desired_length_before_checksum()
        {
            var expectedTargetLength = Any.IntBetween(1, 100);
            var expectedExpandedData = Any.AlphaNumericString(expectedTargetLength + Any.IntBetween(1, 100));

            var mockGenerator = new Mock<Day16DataGenerator>();
            var mockChecksumCreator = new Mock<Day16ChecksumCreator>();

            mockGenerator.Setup(g => g.ExpandData(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(expectedExpandedData);

            mockChecksumCreator.Setup(c => c.GetChecksum(It.IsAny<string>()))
                .Callback<string>(actualStringPassed =>
                {
                    Assert.AreEqual(expectedTargetLength, actualStringPassed.Length);
                });

            var solver = new Day16Solver(Any.AlphaNumericString(), expectedTargetLength, mockGenerator.Object, mockChecksumCreator.Object);
            solver.GetSolution();
        }
    }
}
