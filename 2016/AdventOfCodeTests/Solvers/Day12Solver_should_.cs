using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day12Solver_should_
    {
        [Test]
        public void create_program_with_lines_of_file()
        {
            var solver = Day12Solver.CreateForPart1();

            var expectedLines = "inc a\ninc b\ninc c";

            solver.GetSolution(expectedLines);

            CollectionAssert.AreEqual(expectedLines.Split('\n'), solver.AssemblyProgramInterpreter.Program);
        }

        [Test]
        public void process_copy_command_with_integer()
        {
            var expectedValue = 918;
            var commandString = $"cpy {expectedValue} a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_copy_command_with_register()
        {
            var expectedValue = 918;
            var commandString = $"cpy {expectedValue} b\ncpy b a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_sequence_of_commands()
        {
            var expectedValue = 918;
            var commandString = $"cpy 1 a\ncpy 2 a\ncpy {expectedValue} a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_inc_command()
        {
            var expectedValue = 3;
            var commandString = $"inc a\ninc a\ninc a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_dec_command()
        {
            var expectedValue = -3;
            var commandString = $"dec a\ndec a\ndec a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);

            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_jnz_command_when_register_is_not_zero()
        {
            var expectedValue = 2;
            var commandString = $"inc a\njnz a 2\ninc a\ninc a";

            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);
            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_jnz_command_when_int_operand_is_not_zero()
        {
            var expectedValue = 2;
            var commandString = $"inc a\njnz 1 2\ninc a\ninc a";

            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);
            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void process_jnz_command_when_register_is_zero()
        {
            var expectedValue = 2;
            var commandString = "cpy a 0\njnz a 10\ninc a\ninc a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(commandString);
            Assert.AreEqual(expectedValue, actualResult);
        }

        [TestCase("cpy 41 a\ninc a\ninc a\ndec a\njnz a 2\ndec a", 42)]
        public void pass_given_example(string programText, int expectedValue)
        {
            var actualResult = Day12Solver.CreateForPart1().GetSolution(programText);
            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void support_registers_a_through_d()
        {
            var expectedValue = 11;
            var programText = "cpy 11 d\ncpy d c\ncpy c b\ncpy b a";
            var actualResult = Day12Solver.CreateForPart1().GetSolution(programText);
            Assert.AreEqual(expectedValue, actualResult);
        }

        [Test]
        public void initialize_register_c_to_1_in_part_2()
        {
            var expectedValue = 1;
            var programText = "cpy c a";
            var actualResult = Day12Solver.CreateForPart2().GetSolution(programText);
            Assert.AreEqual(expectedValue, actualResult);
        }
    }
}
