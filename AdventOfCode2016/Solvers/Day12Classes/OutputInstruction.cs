using System;
using System.IO;
using System.Text;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class OutputInstruction : Instruction, IDisposable
    {
        private readonly string _operandString;
        private TextWriter _outputStreamWriter;

        public OutputInstruction(string operandString)
        {
            _operandString = operandString;
        }

        public override void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState)
        {
            var outputValue = GetOperandValue(executionState, _operandString);
            _outputStreamWriter.Write(outputValue);
            _outputStreamWriter.Flush();
        }

        public void AttachOutputStream(Stream outputStream)
        {
            _outputStreamWriter = outputStream == null ? TextWriter.Null : new StreamWriter(outputStream, Encoding.UTF8, 512, true);
        }
        public void Dispose()
        {
            _outputStreamWriter.Dispose();
        }
    }
}