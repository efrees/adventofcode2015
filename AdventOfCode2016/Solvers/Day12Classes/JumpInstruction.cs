using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class JumpInstruction : Instruction
    {
        private string _operandString;

        public JumpInstruction(string operandString)
        {
            this._operandString = operandString;
        }

        public override void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState)
        {
            var operands = _operandString.Split(' ');
            var op1String = operands[0];
            var op1Value = GetOperandValue(executionState, op1String);

            if (op1Value != 0)
            {
                var op2Value = GetOperandValue(executionState, operands[1]);
                executionState.NextInstruction += op2Value - 1;
            }
        }

        private static int GetOperandValue(AssemblyProgramExecutionState executionState, string operandString)
        {
            return executionState.HasRegister(operandString)
                ? executionState.GetRegisterValue(operandString.First())
                : int.Parse(operandString);
        }
    }
}