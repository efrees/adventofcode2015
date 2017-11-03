using System;
using System.Collections.Generic;

namespace AdventOfCode2016.Solvers
{
    internal class Day23Solver
    {
        private readonly int _initialValue;

        public static Day23Solver CreateForPart1()
        {
            return new Day23Solver(7);
        }

        public static Day23Solver CreateForPart2()
        {
            //Runtime is factorial. Solved by interpreting the multiplications manually,
            // but should have simply added the "mul" command to the language.
            return new Day23Solver(12);
        }

        private Day23Solver(int initialValue)
        {
            _initialValue = initialValue;
        }

        public int GetSolution(string fileText)
        {
            var initialRegisterValues = new Dictionary<char, int>
            {
                { 'a', _initialValue}
            };
            return new AssemblyProgramInterpreter(initialRegisterValues)
                .ExecuteAssemblyProgram(fileText);
        }
    }
}