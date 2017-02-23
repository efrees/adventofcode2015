using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day11Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day11Solver_should_
    {
        [Test]
        public void parse_input_line_into_generators_and_microchips()
        {
            var inputLine =
                "The first floor contains a strontium generator, a strontium-compatible microchip, a plutonium generator, and a plutonium-compatible microchip.\n"
                + "Second floor...\nThird floor...\nFourth floor";

            var day11Solver = new Day11Solver();
            day11Solver.GetSolution(inputLine);

            var expectedGenerator = new Day11Component
            {
                Type = "generator",
                Element = "strontium"
            };

            var expectedMicrochip = new Day11Component
            {
                Type = "microchip",
                Element = "plutonium"
            };

            Assert.IsNotNull(day11Solver.ParsedComponentsByFloor);
            Assert.IsTrue(day11Solver.ParsedComponentsByFloor[0].Contains(expectedGenerator), "Parsed input contains expected generator");
            Assert.IsTrue(day11Solver.ParsedComponentsByFloor[0].Contains(expectedMicrochip), "Parsed input contains expected microchip");
        }

        [Test]
        public void parse_multiple_lines_into_correct_floors()
        {
            var inputLines = "The first floor contains a plutonium generator\n"
                             + "The second floor contains a thulium-compatible microchip.\n"
                             + "Third floor...\nFourth floor";

            var day11Solver = new Day11Solver();
            day11Solver.GetSolution(inputLines);

            var expectedGenerator = new Day11Component
            {
                Type = "generator",
                Element = "plutonium"
            };

            var expectedMicrochip = new Day11Component
            {
                Type = "microchip",
                Element = "thulium"
            };

            Assert.IsNotNull(day11Solver.ParsedComponentsByFloor);
            Assert.IsTrue(day11Solver.ParsedComponentsByFloor[0].Contains(expectedGenerator), "Parsed input contains expected generator on first row");
            Assert.IsTrue(day11Solver.ParsedComponentsByFloor[1].Contains(expectedMicrochip), "Parsed input contains expected microchip on second row");
        }

        [Test]
        public void solve_trivial_example_correctly()
        {
            var inputLines = @"The first floor contains a hydrogen-compatible microchip.
The second floor contains a hydrogen generator.
Third floor...
Fourth floor...";

            var expectedAnswer = 3;
            var actualAnswer = new Day11Solver().GetSolution(inputLines);

            Assert.AreEqual(expectedAnswer, actualAnswer);
        }
        
        [Test]
        public void solve_example_correctly()
        {
            var inputLines =
                @"The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
The second floor contains a hydrogen generator.
The third floor contains a lithium generator.
The fourth floor contains nothing relevant.";

            var expectedAnswer = 11;
            var actualAnswer = new Day11Solver().GetSolution(inputLines);

            Assert.AreEqual(expectedAnswer, actualAnswer);
        }
    }
}
