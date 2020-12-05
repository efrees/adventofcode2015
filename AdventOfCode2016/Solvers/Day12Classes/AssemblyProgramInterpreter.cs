using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class AssemblyProgramInterpreter
    {
        private readonly Stream _outputStream;
        private readonly Dictionary<char, int> _initialRegisterValues;
        private string[] _program;

        public AssemblyProgramInterpreter(Dictionary<char, int> initialRegisterValues = null, Stream outputStream = null)
        {
            _initialRegisterValues = initialRegisterValues ?? new Dictionary<char, int>();
            _outputStream = outputStream;
        }

        public IReadOnlyList<string> Program => _program;

        public int ExecuteAssemblyProgram(string programText)
        {
            _program = programText.SplitIntoLines().ToArray();
            var state = GetInitialExecutionState(_program);

            while (Program.Count > state.NextInstruction)
            {
                ExecuteNextInstruction(state);
            }

            return state.GetRegisterValue('a');
        }

        private void ExecuteNextInstruction(AssemblyProgramExecutionState state)
        {
            var rawInstruction = Program[state.NextInstruction];
            var instruction = Instruction.ParseFromText(rawInstruction);

            (instruction as OutputInstruction)?.AttachOutputStream(_outputStream);
            instruction.ExecuteWithCurrentState(state);

            state.NextInstruction++;
        }

        private AssemblyProgramExecutionState GetInitialExecutionState(IList<string> program)
        {
            var assemblyProgramExecutionState = new AssemblyProgramExecutionState(program);
            foreach (var k in _initialRegisterValues.Keys)
            {
                assemblyProgramExecutionState.SetRegisterValue(k, _initialRegisterValues[k]);
            }
            return assemblyProgramExecutionState;
        }
    }
}