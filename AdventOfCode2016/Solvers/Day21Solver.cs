using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day21Solver
    {
        private readonly string _startString;

        public static Day21Solver CreateForPart1()
        {
            return new Day21Solver("abcdefgh");
        }

        public static Day21Solver CreateForPart2()
        {
            return new Day21Part2Solver("fbgdceah");
        }

        internal Day21Solver(string startString)
        {
            _startString = startString;
        }

        public string GetSolution(string fileText)
        {
            var commands = GetCommandsFromText(fileText);

            return TransformWithCommands(_startString, commands);
        }

        public virtual IEnumerable<string> GetCommandsFromText(string fileText)
        {
            return fileText.SplitIntoLines();
        }

        private string TransformWithCommands(string dataString, IEnumerable<string> commands)
        {
            var data = dataString.ToCharArray();
            foreach (var command in commands)
            {
                data = ApplyCommand(data, command);
            }
            return new string(data);
        }

        private char[] ApplyCommand(char[] data, string command)
        {
            var tokens = command.Split(' ');
            if (command.StartsWith("swap position"))
            {
                var position1 = int.Parse(tokens[2]);
                var position2 = int.Parse(tokens[5]);
                SwapPositions(data, position1, position2);
            }
            else if (command.StartsWith("swap letter"))
            {
                var position1 = Array.IndexOf(data, tokens[2][0]);
                var position2 = Array.IndexOf(data, tokens[5][0]);
                SwapPositions(data, position1, position2);
            }
            else if (command.StartsWith("reverse positions"))
            {
                var position1 = int.Parse(tokens[2]);
                var position2 = int.Parse(tokens[4]);
                ReverseSubstring(data, position1, position2);
            }
            else if (command.StartsWith("move"))
            {
                var position1 = int.Parse(tokens[2]);
                var position2 = int.Parse(tokens[5]);
                MoveToNewPosition(data, position1, position2);
            }
            else if (command.StartsWith("rotate based on"))
            {
                var letter = tokens.Last()[0];
                var position = Array.IndexOf(data, letter);
                var amount = GetRotatationAmountForLetterPosition(position, data.Length);
                RotateByAmount(data, "right", amount);
            }
            else if (command.StartsWith("rotate"))
            {
                var amount = int.Parse(tokens[2]);
                RotateByAmount(data, tokens[1], amount);
            }

            return data;
        }

        private static void SwapPositions(char[] data, int position1, int position2)
        {
            var temp = data[position1];
            data[position1] = data[position2];
            data[position2] = temp;
        }

        private static void ReverseSubstring(char[] data, int position1, int position2)
        {
            var substringLength = position2 - position1 + 1;
            var newData = data.Take(position1)
                .Concat(data.Skip(position1).Take(substringLength).Reverse())
                .Concat(data.Skip(position1 + substringLength));
            newData.ToArray().CopyTo(data, 0);
        }

        protected virtual void MoveToNewPosition(char[] data, int position1, int position2)
        {
            var dataList = data.ToList();
            var letter = dataList[position1];
            dataList.RemoveAt(position1);
            dataList.Insert(position2, letter);
            dataList.CopyTo(data);
        }

        protected virtual int GetRotatationAmountForLetterPosition(int letterPosition, int dataLength)
        {
            return 1 + letterPosition + (letterPosition >= 4 ? 1 : 0);
        }

        protected virtual void RotateByAmount(char[] data, string direction, int amount)
        {
            amount %= data.Length;
            amount = direction == "left" ? amount : data.Length - amount;
            var newData = data.Skip(amount).Concat(data.Take(amount));
            newData.ToArray().CopyTo(data, 0);
        }
    }

    internal class Day21Part2Solver : Day21Solver
    {
        internal Day21Part2Solver(string startString) : base(startString)
        {
        }

        public override IEnumerable<string> GetCommandsFromText(string fileText)
        {
            return base.GetCommandsFromText(fileText)
                .Reverse();
        }

        protected override void MoveToNewPosition(char[] data, int position1, int position2)
        {
            base.MoveToNewPosition(data, position2, position1);
        }

        protected override void RotateByAmount(char[] data, string direction, int amount)
        {
            var swappedDirection = direction == "right" ? "left" : "right";
            base.RotateByAmount(data, swappedDirection, amount);
        }

        protected override int GetRotatationAmountForLetterPosition(int letterPosition, int dataLength)
        {
            //Solutions are not unique in general, but will be for our given input.
            foreach (var candidateStartPosition in Enumerable.Range(0, dataLength))
            {
                var candidateRotationAmount = 1 + candidateStartPosition + (candidateStartPosition >= 4 ? 1 : 0);
                var candidateResult = (candidateStartPosition + candidateRotationAmount) % dataLength;
                if (candidateResult == letterPosition)
                    return candidateRotationAmount;
            }
            return -1;
        }
    }
}