using AdventOfCode2016;
using NUnit.Framework;

namespace AdventOfCodeTests
{
    internal class Day17Solver_should_
    {
        [TestCase("ihgpwlah", "DDRRRD")]
        [TestCase("kglvqrro", "DDUDRLRRUDRD")]
        [TestCase("ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
        public void solve_examples_correctly(string inputSalt, string expectedPath)
        {
            var actualPath = new Day17Solver(inputSalt).GetSolution();
            Assert.AreEqual(expectedPath, actualPath);
        }
    }
}
