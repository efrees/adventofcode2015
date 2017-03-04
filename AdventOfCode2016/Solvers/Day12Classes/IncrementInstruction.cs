using System.Linq;
using AdventOfCode2016.Solvers.Day12Classes;

internal class IncrementInstruction : Instruction
{
    private readonly string _operandString;

    public IncrementInstruction(string operandString)
    {
        _operandString = operandString;
    }

    public override void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState)
    {
        var registerOperand = _operandString.First();
        var currentRegisterValue = executionState.GetRegisterValue(registerOperand);
        executionState.SetRegisterValue(registerOperand, currentRegisterValue + 1);
    }
}