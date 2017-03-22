using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day10Solver_should_
    {
        [Test]
        public void pass_value_from_value_to_output_correctly()
        {
            var instruction = "value 5 goes to output 0";

            var solver = Day10Solver.Create();
            solver.GetSolution(instruction);

            Assert.AreEqual(5, solver.GetOutputValue(0));
        }

        [Test]
        public void correctly_sort_chips_in_example_case()
        {
            var instructions = @"value 5 goes to bot 2
bot 2 gives low to bot 1 and high to bot 0
value 3 goes to bot 1
bot 1 gives low to output 1 and high to bot 0
bot 0 gives low to output 2 and high to output 0
value 2 goes to bot 2";

            var solver = Day10Solver.Create();
            solver.GetSolution(instructions);

            Assert.AreEqual(5, solver.GetOutputValue(0));
            Assert.AreEqual(2, solver.GetOutputValue(1));
            Assert.AreEqual(3, solver.GetOutputValue(2));
        }

        [Test]
        public void correctly_identify_which_bot_does_comparison()
        {
            var instructions = @"value 5 goes to bot 2
bot 2 gives low to bot 1 and high to bot 0
value 3 goes to bot 1
bot 1 gives low to output 1 and high to bot 0
bot 0 gives low to output 2 and high to output 0
value 2 goes to bot 2";

            var solver = Day10Solver.Create();
            solver.GetSolution(instructions);

            Assert.AreEqual(2, solver.BotNetwork.FindBotThatDidComparison(5, 2));
        }
    }
}
