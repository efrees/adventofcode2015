using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day23Solver_should_
    {
        [Test]
        public void support_toggle_command()
        {
            var exampleProgram = "cpy 2 a\ntgl a\ntgl a\ntgl a\ncpy 1 a\ndec a\ndec a";
            var expectedValue = 3;
            var solver = Day23Solver.Create();

            var actualValue = solver.GetSolution(exampleProgram);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
