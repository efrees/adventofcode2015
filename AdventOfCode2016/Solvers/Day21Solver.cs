using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdventOfCode2016.Solvers
{
    internal class Day21Solver
    {
        private readonly string _startString;

        public static Day21Solver Create()
        {
            return new Day21Solver("abcdefgh");
        }

        internal Day21Solver(string startString)
        {
            _startString = startString;
        }

        public string GetSolution(string fileText)
        {
            var commands = fileText.SplitIntoLines();

            return TransformWithCommands(_startString, commands);
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
            else if (command.StartsWith("rotate based on"))
            {
                var letter = tokens.Last()[0];
                var position = Array.IndexOf(data, letter);
                var amount = 1 + position + (position >= 4 ? 1 : 0);
                RotateByAmount(data, "right", amount);
            }
            else if (command.StartsWith("rotate"))
            {
                var amount = int.Parse(tokens[2]);
                RotateByAmount(data, tokens[1], amount);
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
            return data;
        }

        private static void SwapPositions(char[] data, int position1, int position2)
        {
            var temp = data[position1];
            data[position1] = data[position2];
            data[position2] = temp;
        }

        private static void RotateByAmount(char[] data, string direction, int amount)
        {
            amount %= data.Length;
            amount = direction == "left" ? amount : data.Length - amount;
            var newData = data.Skip(amount).Concat(data.Take(amount));
            newData.ToArray().CopyTo(data, 0);
        }

        private static void ReverseSubstring(char[] data, int position1, int position2)
        {
            var substringLength = position2 - position1 + 1;
            var newData = data.Take(position1)
                .Concat(data.Skip(position1).Take(substringLength).Reverse())
                .Concat(data.Skip(position1 + substringLength));
            newData.ToArray().CopyTo(data, 0);
        }

        private void MoveToNewPosition(char[] data, int position1, int position2)
        {
            var dataList = data.ToList();
            var letter = dataList[position1];
            dataList.RemoveAt(position1);
            dataList.Insert(position2, letter);
            dataList.CopyTo(data);
        }
    }
}