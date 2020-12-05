using System.Collections.Generic;
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
            var initialRegisterValues = new Dictionary<char, int>
            {
                { 'c', 1 }
            };
            return new Day12Solver(initialRegisterValues);
        }

        internal readonly AssemblyProgramInterpreter AssemblyProgramInterpreter;

        private Day12Solver(Dictionary<char, int> initialRegisterValues = null)
        {
            AssemblyProgramInterpreter = new AssemblyProgramInterpreter(initialRegisterValues);
        }

        public int GetSolution(string programText)
        {
            return AssemblyProgramInterpreter.ExecuteAssemblyProgram(programText);
        }
    }
}