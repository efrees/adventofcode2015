using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day12Solver_should_
    {
        [Test]
        public void create_program_with_lines_of_file()
        {
            var solver = Day12Solver.Create();

            var expectedLines = "inc a\ninc b\ninc c";

            solver.GetSolution(expectedLines);

            CollectionAssert.AreEqual(expectedLines.Split('\n'), solver.Program);
        }

        [Test]
        public void process_copy_command_with_integer()
        {
            var expectedValue = 918;
            var commandString = $"cpy {expectedValue} a";
            var actualResult = Day12Solver.Create().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_copy_command_with_register()
        {
            var expectedValue = 918;
            var commandString = $"cpy {expectedValue} b\ncpy b a";
            var actualResult = Day12Solver.Create().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_sequence_of_commands()
        {
            var expectedValue = 918;
            var commandString = $"cpy 1 a\ncpy 2 a\ncpy {expectedValue} a";
            var actualResult = Day12Solver.Create().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_inc_command()
        {
            var expectedValue = 3;
            var commandString = $"inc a\ninc a\ninc a";
            var actualResult = Day12Solver.Create().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_dec_command()
        {
            var expectedValue = -3;
            var commandString = $"dec a\ndec a\ndec a";
            var actualResult = Day12Solver.Create().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }
    }
}
