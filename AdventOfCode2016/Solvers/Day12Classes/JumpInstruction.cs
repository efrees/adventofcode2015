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
            var op1Value = executionState.HasRegister(op1String)
                ? executionState.GetRegisterValue(op1String.First())
                : int.Parse(operands[0]);

            if (op1Value != 0)
            {
                var op2Value = int.Parse(operands[1]);
                executionState.NextInstruction += op2Value - 1;
            }
        }
    }
}