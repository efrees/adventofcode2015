using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Solvers.Day12Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day12Solver
    {
        public static Day12Solver CreateForPart1()
        {
            return new Day12Solver();
        }

        public static Day12Solver CreateForPart2()
        {
            var day12Solver = new Day12Solver();
            day12Solver._initialRegisterValues['c'] = 1;
            return day12Solver;
        }

        private Dictionary<char, int> _initialRegisterValues = new Dictionary<char, int>();

        public int GetSolution(string programText)
        {
            Program = programText.SplitIntoLines().ToArray();
            var state = GetInitialExecutionState();

            while (Program.Count > state.NextInstruction)
            {
                var rawInstruction = Program[state.NextInstruction];
                var instruction = Instruction.ParseFromText(rawInstruction);
                instruction.ExecuteWithCurrentState(state);

                state.NextInstruction++;
            }

            return state.GetRegisterValue('a');
        }

        private AssemblyProgramExecutionState GetInitialExecutionState()
        {
            var assemblyProgramExecutionState = new AssemblyProgramExecutionState();
            foreach (var k in _initialRegisterValues.Keys)
            {
                assemblyProgramExecutionState.SetRegisterValue(k, _initialRegisterValues[k]);
            }
            return assemblyProgramExecutionState;
        }

        public IReadOnlyList<string> Program { get; set; }
    }
}