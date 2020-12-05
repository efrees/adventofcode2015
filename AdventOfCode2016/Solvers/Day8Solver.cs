using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2016.Solvers.Day8Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day8Solver
    {
        public static Day8Solver Create()
        {
            return new Day8Solver();
        }

        public int GetSolution(string fileText)
        {
            var parsedInstructions = fileText
                .SplitIntoLines()
                .Select(ParseInstruction);

            var rotatableGrid = new RotatableGrid(50, 6);
            foreach (var instruction in parsedInstructions)
            {
                ApplyInstructionOnGrid(instruction, rotatableGrid);
            }

            Console.WriteLine(rotatableGrid.ToString());
            return rotatableGrid.CountOfSetPixels();
        }

        private static void ApplyInstructionOnGrid(Day8Instruction instruction, RotatableGrid rotatableGrid)
        {
            if (instruction.Command == "rect")
            {
                rotatableGrid.DrawRectangle(instruction.Operand1, instruction.Operand2);
            }
            else if (instruction.Command == "rotate column")
            {
                rotatableGrid.RotateColumn(instruction.Operand1, instruction.Operand2);
            }
            else if (instruction.Command == "rotate row")
            {
                rotatableGrid.RotateRow(instruction.Operand1, instruction.Operand2);
            }
        }

        private Day8Instruction ParseInstruction(string line)
        {
            var rectRegex = "(?<command>rect) (?<op1>\\d+)x(?<op2>\\d+)";
            var rotateRegex = "(?<command>rotate \\w+) [xy]=(?<op1>\\d+) by (?<op2>\\d+)";

            var matchAttempt = Regex.Match(line, rectRegex);
            if (!matchAttempt.Success)
            {
                matchAttempt = Regex.Match(line, rotateRegex);
            }

            Debug.Assert(matchAttempt.Success);

            return new Day8Instruction
            {
                Command = matchAttempt.Groups["command"].Value,
                Operand1 = int.Parse(matchAttempt.Groups["op1"].Value),
                Operand2 = int.Parse(matchAttempt.Groups["op2"].Value)
            };
        }
    }

    internal class Day8Instruction
    {
        public string Command { get; set; }
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
    }
}