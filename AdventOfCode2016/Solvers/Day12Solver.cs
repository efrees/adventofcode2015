using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Solvers.Day12Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day12Solver
    {
        public static Day12Solver Create()
        {
            return new Day12Solver();
        }

        public int GetSolution(string programText)
        {
            Program = programText.SplitIntoLines().ToArray();
            var state = new AssemblyProgramExecutionState();

            while (Program.Count > state.NextInstruction)
            {
                var rawInstruction = Program[state.NextInstruction];
                var instruction = Instruction.ParseFromText(rawInstruction);
                instruction.ExecuteWithCurrentState(state);

                state.NextInstruction++;
            }

            return state.GetRegisterValue('a');
        }

        public IReadOnlyList<string> Program { get; set; }
    }
}