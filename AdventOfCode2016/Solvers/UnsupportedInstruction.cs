using System;
using AdventOfCode2016.Solvers.Day12Classes;

internal class UnsupportedInstruction : Instruction
{
    private readonly string _rawInstruction;

    public UnsupportedInstruction(string rawInstruction)
    {
        _rawInstruction = rawInstruction;
    }

    public override void ExecuteWithCurrentState(AssemblyProgramExecutionState executionState)
    {
        throw new NotSupportedException(_rawInstruction);
    }
}