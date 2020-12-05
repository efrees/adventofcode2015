using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day5Solver_should_
    {
        [Test]
        [Ignore("Because it's slow... ~40 seconds")]
        public void solve_example_case_correctly_for_part_1()
        {
            var inputLine = "abc";
            var expectedResult = "18f47a30";

            var actualResult = Day5Solver.CreateForPart1().GetSolution(inputLine);

            StringAssert.AreEqualIgnoringCase(expectedResult, actualResult);
        }

        [Test]
        [Ignore("Because it's slow... ~60 seconds")]
        public void solve_example_case_correctly_for_part_2()
        {
            var inputLine = "abc";
            var expectedResult = "05ace8e3";

            var actualResult = Day5Solver.CreateForPart2().GetSolution(inputLine);

            StringAssert.AreEqualIgnoringCase(expectedResult, actualResult);
        }
    }
}
