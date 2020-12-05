using AdventOfCode2016;
using AdventOfCode2016.Solvers;
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
            var actualPath = new Day17Part1Solver(inputSalt).GetSolution();
            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestCase("ihgpwlah", "370")]
        [TestCase("kglvqrro", "492")]
        [TestCase("ulqzkmiv", "830")]
        public void solve_part2_examples_correctly(string inputSalt, string pathLength)
        {
            var actualPathLength = new Day17Part2Solver(inputSalt).GetSolution();
            Assert.AreEqual(pathLength, actualPathLength);
        }
    }
}
