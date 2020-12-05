using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal abstract class Instruction
    {
        public abstract void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState);

        public static Instruction ParseFromText(string rawInstruction)
        {
            var command = rawInstruction.Substring(0, 4).TrimEnd();
            var operandString = rawInstruction.Substring(4);

            switch (command)
            {
                case "cpy":
                    return new CopyInstruction(operandString);
                case "inc":
                    return new IncrementInstruction(operandString);
                case "dec":
                    return new DecrementInstruction(operandString);
                case "jnz":
                    return new JumpInstruction(operandString);
                case "tgl":
                    return new ToggleInstruction(operandString);
                case "out":
                    return new OutputInstruction(operandString);
            }

            return new UnsupportedInstruction(rawInstruction);
        }

        protected virtual int GetOperandValue(AssemblyProgramExecutionState executionState, string operandString)
        {
            return executionState.HasRegister(operandString)
                ? executionState.GetRegisterValue(operandString.First())
                : int.Parse(operandString);
        }
    }
}