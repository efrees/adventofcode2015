using System.IO;
using System.Text;
using AdventOfCode2016.Solvers.Day12Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class AssemblyProgramInterpreter_should_
    {
        //Most tests are under Day12Solver_should_

        [Test]
        public void process_out_instruction()
        {
            var program = "cpy 2 b\nout 1\nout b";
            var outputStream = new MemoryStream();
            var interpreter = new AssemblyProgramInterpreter(outputStream: outputStream);

            interpreter.ExecuteAssemblyProgram(program);

            outputStream.Position = 0;
            var reader = new StreamReader(outputStream);
            Assert.AreEqual("12", reader.ReadToEnd());
        }

        [Test]
        public void not_throw_for_output_instruction_when_no_output_stream_exists()
        {
            var program = "cpy 2 b\nout 1\nout b";
            var interpreter = new AssemblyProgramInterpreter();

            Assert.DoesNotThrow(() => interpreter.ExecuteAssemblyProgram(program));
        }
    }
}
