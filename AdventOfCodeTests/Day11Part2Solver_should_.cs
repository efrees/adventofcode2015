using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests
{
    internal class Day11Part2Solver_should_
    {
        [Test]
        public void parse_input_line_into_generators_and_microchips()
        {
            var inputLine =
                "The first floor contains a strontium generator, a strontium-compatible microchip, a plutonium generator, and a plutonium-compatible microchip.\n"
                + "Second floor...\nThird floor...\nFourth floor";

            var day11Solver = new Day11Part2Solver();
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

            Assert.IsNotNull(day11Solver.SearchStartState);
            Assert.IsTrue(day11Solver.SearchStartState.FirstFloor.Contains(expectedGenerator), "Parsed input contains expected generator");
            Assert.IsTrue(day11Solver.SearchStartState.FirstFloor.Contains(expectedMicrochip), "Parsed input contains expected microchip");
        }

        [Test]
        public void parse_multiple_lines_into_correct_floors()
        {
            var inputLines = "The first floor contains a plutonium generator\n"
                             + "The second floor contains a thulium-compatible microchip.\n"
                             + "Third floor...\nFourth floor";

            var day11Solver = new Day11Part2Solver();
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

            Assert.IsNotNull(day11Solver.SearchStartState);
            Assert.IsTrue(day11Solver.SearchStartState.FirstFloor.Contains(expectedGenerator), "Parsed input contains expected generator on first row");
            Assert.IsTrue(day11Solver.SearchStartState.SecondFloor.Contains(expectedMicrochip), "Parsed input contains expected microchip on second row");
        }

        [Test]
        public void add_additional_items_to_first_floor()
        {
            var inputLines = "First floor...\nSecond floor...\nThird floor...\nFourth floor...";

            var day11Solver = new Day11Part2Solver();
            day11Solver.GetSolution(inputLines);

            var eleriumGenerator = new Day11Component { Type = "generator", Element = "elerium" };
            var eleriumMicrochip = new Day11Component { Type = "microchip", Element = "elerium" };
            var dilithiumGenerator = new Day11Component { Type = "generator", Element = "dilithium" };
            var dilithiumMicrochip = new Day11Component { Type = "microchip", Element = "dilithium" };

            CollectionAssert.Contains(day11Solver.SearchStartState.FirstFloor, eleriumGenerator);
            CollectionAssert.Contains(day11Solver.SearchStartState.FirstFloor, eleriumMicrochip);
            CollectionAssert.Contains(day11Solver.SearchStartState.FirstFloor, dilithiumGenerator);
            CollectionAssert.Contains(day11Solver.SearchStartState.FirstFloor, dilithiumMicrochip);
        }

        [Test]
        public void solve_trivial_case_correctly()
        {
            var inputLines = "First floor...\nSecond floor...\nThird floor...\nFourth floor...";

            var expectedAnswer = 15;
            var actualAnswer = new Day11Part2Solver().GetSolution(inputLines);

            Assert.AreEqual(expectedAnswer, actualAnswer);
        }
    }
}
