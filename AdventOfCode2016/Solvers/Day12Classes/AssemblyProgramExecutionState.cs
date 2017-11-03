using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class AssemblyProgramExecutionState
    {
        private readonly char[] _supportedRegisterNames = { 'a', 'b', 'c', 'd' };
        private readonly IDictionary<char, int> _registers;
        private readonly IList<string> _programStore;

        public int NextInstruction { get; set; }

        public AssemblyProgramExecutionState(IList<string> programStore)
        {
            _programStore = programStore;
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

        public string GetInstruction(int instructionPointer)
        {
            return instructionPointer < _programStore.Count
                ? _programStore[instructionPointer]
                : "";
        }

        public void ReplaceInstruction(int instructionPointer, string newInstruction)
        {
            if (instructionPointer < _programStore.Count)
                _programStore[instructionPointer] = newInstruction;
        }
    }
}