using AdventOfCode2016.Solvers.Day12Classes;

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
        }

        return new UnsupportedInstruction(rawInstruction);
    }
}