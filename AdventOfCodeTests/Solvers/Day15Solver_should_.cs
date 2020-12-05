using System;
using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day15Solver_should_
    {
        [Test]
        public void solve_example_correctly()
        {
            var input = "Disc #1 has 5 positions; at time=0, it is at position 4." + Environment.NewLine +
                        "Disc #2 has 2 positions; at time=0, it is at position 1.";
            var solver = Day15Solver.CreateForPart1();
            var actualResult = solver.GetSolution(input);

            Assert.AreEqual(5, actualResult);
        }

        [Test]
        public void include_additional_disc_in_part_2()
        {
            var input = "Disc #1 has 5 positions; at time=0, it is at position 4." + Environment.NewLine +
                        "Disc #2 has 2 positions; at time=0, it is at position 1.";
            var solver = Day15Solver.CreateForPart2();

            Assert.IsNotNull(solver.RingToAppend);

            var actualResult = solver.GetSolution(input);
            Assert.AreEqual(85, actualResult);
        }
    }
}
