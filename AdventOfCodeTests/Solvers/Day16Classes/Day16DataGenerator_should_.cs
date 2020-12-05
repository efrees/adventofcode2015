using AdventOfCode2016.Solvers.Day16Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers.Day16Classes
{
    internal class Day16DataGenerator_should_
    {
        [TestCase("1", "100")]
        [TestCase("0", "001")]
        [TestCase("11111", "11111000000")]
        [TestCase("111100001010", "1111000010100101011110000")]
        public void expand_data_string_using_given_algorithm(string inputData, string expectedExpandedData)
        {
            var actualExpandedData = new Day16DataGenerator().ExpandData(inputData, inputData.Length + 1);
            Assert.AreEqual(expectedExpandedData, actualExpandedData);
        }

        [TestCase("1", 5, "1000110")]
        [TestCase("10000", 20, "10000011110010000111110")]
        public void repeat_expansion_until_data_is_at_least_desired_length(string inputData, int desiredLength, string expectedExpandedData)
        {
            var actualExpandedData = new Day16DataGenerator().ExpandData(inputData, desiredLength);
            Assert.AreEqual(expectedExpandedData, actualExpandedData);
        }
    }
}
