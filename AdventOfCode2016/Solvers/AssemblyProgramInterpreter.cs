using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Solvers.Day12Classes;

namespace AdventOfCode2016.Solvers
{
    internal class AssemblyProgramInterpreter
    {
        private readonly Dictionary<char, int> _initialRegisterValues;
        private string[] _program;

        public AssemblyProgramInterpreter(Dictionary<char, int> initialRegisterValues = null)
        {
            _initialRegisterValues = initialRegisterValues ?? new Dictionary<char, int>();
        }

        public IReadOnlyList<string> Program => _program;

        public int ExecuteAssemblyProgram(string programText)
        {
            _program = programText.SplitIntoLines().ToArray();
            var state = GetInitialExecutionState(_program);

            while (Program.Count > state.NextInstruction)
            {
                var rawInstruction = Program[state.NextInstruction];
                var instruction = Instruction.ParseFromText(rawInstruction);
                instruction.ExecuteWithCurrentState(state);

                state.NextInstruction++;
            }

            return state.GetRegisterValue('a');
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