using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day7Solver_should_
    {
        [TestCase("abba")]
        [TestCase("dood")]
        [TestCase("asdffds")]
        [TestCase("abba[mnop]qrst")]
        [TestCase("ioxxoj[asdfgh]zxcvbn")]
        public void count_line_if_single_line_contains_an_abba_sequence_in_part_1(string inputLine)
        {
            var actualResult = Day7Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(1, actualResult);
        }

        [TestCase("abb")]
        [TestCase("baad")]
        [TestCase("aaaa[qwer]tyui")]
        [TestCase("notcounted")]
        public void not_count_line_if_line_does_not_contain_an_abba_sequence_in_part_1(string inputLine)
        {
            var actualResult = Day7Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(0, actualResult);
        }

        [Test]
        public void count_multiple_matches_correctly()
        {
            var inputText = "abba\ndood\nnotcounted\nabba[mnop]qrst";
            var expectedCount = 3;
            var actualResult = Day7Solver.CreateForPart1().GetSolution(inputText);
            Assert.AreEqual(expectedCount, actualResult);
        }

        [TestCase("[abba]")]
        [TestCase("abcd[bddb]xyyx")]
        public void not_count_string_with_abba_sequence_if_abba_sequence_in_brackets_in_part_1(string inputLine)
        {
            var actualResult = Day7Solver.CreateForPart1().GetSolution(inputLine);
            Assert.AreEqual(0, actualResult);
        }

        [TestCase("aba[bab]xyz", 1)]
        [TestCase("xyx[xyx]xyx", 0)]
        [TestCase("aaa[kek]eke", 1)]
        [TestCase("zazbz[bzb]cdb", 1)]
        [TestCase("abc[dedede]fg", 0)]
        [TestCase("aaa[aaa]aaa", 0)]
        public void look_for_corresponding_aba_and_bab_in_part_2(string inputLine, int expectedCount)
        {
            var actualResult = Day7Solver.CreateForPart2().GetSolution(inputLine);
            Assert.AreEqual(expectedCount, actualResult);
        }
    }
}
