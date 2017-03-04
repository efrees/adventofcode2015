using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class AssemblyProgramExecutionState
    {
        private readonly char[] _supportedRegisterNames = { 'a', 'b', 'c' };
        private readonly IDictionary<char, int> _registers;

        public int NextInstruction { get; set; }

        public AssemblyProgramExecutionState()
        {
            _registers = _supportedRegisterNames.ToDictionary(r => r, r => 0);
        }

        public bool HasRegister(string operand)
        {
            return _registers.Keys.Contains(operand.First());
        }

        public int GetRegisterValue(char register)
        {
            return _registers[register];
        }

        public void SetRegisterValue(char register, int value)
        {
            _registers[register] = value;
        }
    }
}