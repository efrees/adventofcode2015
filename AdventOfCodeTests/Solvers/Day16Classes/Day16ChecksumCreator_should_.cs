using AdventOfCode2016.Solvers.Day16Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers.Day16Classes
{
    internal class Day16ChecksumCreator_should_
    {
        [TestCase("11", "1")]
        [TestCase("00", "1")]
        [TestCase("01", "0")]
        [TestCase("10", "0")]
        public void reduce_simple_pairs_correctly_for_checksum(string inputData, string expectedChecksum)
        {
            var actualChecksum = new Day16ChecksumCreator().GetChecksum(inputData);
            Assert.AreEqual(expectedChecksum, actualChecksum);
        }

        [TestCase("1")]
        [TestCase("0")]
        [TestCase("101")]
        [TestCase("11111")]
        [TestCase("111110000")]
        public void return_data_as_checksum_if_odd_length(string inputData)
        {
            var actualChecksum = new Day16ChecksumCreator().GetChecksum(inputData);
            Assert.AreEqual(inputData, actualChecksum);
        }

        [TestCase("1111", "1")]
        [TestCase("101010", "000")]
        [TestCase("10000011110010000111", "01100")]
        public void repeatedly_reduce_even_length_data_until_odd_length(string inputData, string expectedChecksum)
        {
            var actualChecksum = new Day16ChecksumCreator().GetChecksum(inputData);
            Assert.AreEqual(expectedChecksum, actualChecksum);
        }
    }
}
