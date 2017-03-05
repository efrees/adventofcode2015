using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class CopyInstruction : Instruction
    {
        private readonly string _operandString;

        public CopyInstruction(string operandString)
        {
            this._operandString = operandString;
        }

        public override void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState)
        {
            var operands = _operandString.Split(' ');
            var op1Value = executionState.HasRegister(operands[0])
                ? executionState.GetRegisterValue(operands[0].First())
                : int.Parse(operands[0]);

            var op2Register = operands[1].First();
            executionState.SetRegisterValue(op2Register, op1Value);
        }
    }
}