using System;
using System.Collections.Generic;

namespace AdventOfCode2016.Solvers
{
    internal class Day23Solver
    {
        public static Day23Solver Create()
        {
            return new Day23Solver();
        }

        public int GetSolution(string fileText)
        {
            var initialRegisterValues = new Dictionary<char, int>()
            {
                { 'a', 7 }
            };
            return new AssemblyProgramInterpreter(initialRegisterValues)
                .ExecuteAssemblyProgram(fileText);
        }
    }
}