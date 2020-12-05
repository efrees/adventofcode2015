using System.Linq;

namespace AdventOfCode2016.Solvers.Day12Classes
{
    internal class ToggleInstruction : Instruction
    {
        private readonly string _operandString;

        public ToggleInstruction(string operandString)
        {
            _operandString = operandString;
        }

        public override void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState)
        {
            var op1Value = executionState.HasRegister(_operandString)
                ? executionState.GetRegisterValue(_operandString.First())
                : int.Parse(_operandString);

            var instructionToToggle = executionState.NextInstruction + op1Value;
            var targetInstruction = executionState.GetInstruction(instructionToToggle);
            var newInstruction = GetToggledInstruction(targetInstruction);
            executionState.ReplaceInstruction(instructionToToggle, newInstruction);
        }

        private static string GetToggledInstruction(string targetInstruction)
        {
            var tokens = targetInstruction.Split(' ');
            if (tokens[0] == "inc")
            {
                tokens[0] = "dec";
            }
            else if (tokens[0] == "jnz")
            {
                tokens[0] = "cpy";
            }
            else if (tokens.Length == 2)
            {
                tokens[0] = "inc";
            }
            else if (tokens.Length == 3)
            {
                tokens[0] = "jnz";
            }

            return string.Join(" ", tokens);
        }
    }
}