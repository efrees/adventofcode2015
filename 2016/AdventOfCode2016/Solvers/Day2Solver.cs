using System.Text;
using AdventOfCode2016.Solvers.Day2Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day2Solver
    {
        private Day2KeyPad _keypad;

        public static Day2Solver CreateForPart1()
        {
            var solver = new Day2Solver();
            solver._keypad = new Day2Part1Keypad();
            return solver;
        }

        public static Day2Solver CreateForPart2()
        {
            var solver = new Day2Solver();
            solver._keypad = new Day2Part2Keypad();
            return solver;
        }

        private Day2Solver() { }

        public string GetSolution(string fileText)
        {
            var splitLines = fileText.Trim().Split('\n');
            
            var sb = new StringBuilder();
            foreach (var line in splitLines)
            {
                var nextDigit = ResultAfterAllMovements(_keypad, line);
                sb.Append(nextDigit);
            }
            return sb.ToString();
        }

        private char ResultAfterAllMovements(Day2KeyPad keypad, string line)
        {
            foreach (var direction in line)
            {
                switch (direction)
                {
                    case 'U':
                        keypad.MoveUp(); break;
                    case 'D':
                        keypad.MoveDown(); break;
                    case 'L':
                        keypad.MoveLeft(); break;
                    case 'R':
                        keypad.MoveRight(); break;
                }
            }
            return keypad.CurrentKey;
        }
    }
}
