using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    public class Day6
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day6input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            bool[,] grid = new bool[1000, 1000];
            int[,] grid2 = new int[1000, 1000];

            string inputLine;
            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                var tokens = inputLine.ToLower().Trim().Split(' ');
                var tokenList = new Queue<string>(tokens);

                try
                {
                    var next = tokenList.Dequeue();
                    int x1, y1, x2, y2;

                    if (next == "toggle")
                    {
                        ParseRange(tokenList, out x1, out y1, out x2, out y2);

                        ToggleRange(grid, x1, y1, x2, y2);
                        IncreaseRangeBy2(grid2, x1, y1, x2, y2);
                    }
                    else
                    {
                        //First word must be "turn". Get direction
                        next = tokenList.Dequeue();
                        var turnTo = next == "on";

                        ParseRange(tokenList, out x1, out y1, out x2, out y2);

                        TurnRange(grid, turnTo, x1, y1, x2, y2);
                        AddOrSubtract1(grid2, turnTo, x1, y1, x2, y2);
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }

            var totalOn = CountLights(grid);
            var brightness = SubIntensities(grid2);

            Console.WriteLine("Lit Lights Count (P1): " + totalOn);
            Console.WriteLine("Total Brightness (P2): " + brightness);
            //Console.WriteLine("Addendum producing 6 leading zeros: " + secondAnswer);
        }

        private static void ParseRange(Queue<string> tokenList, out int x1, out int y1, out int x2, out int y2)
        {
            Regex pattern = new Regex("(?<x>\\d+),(?<y>\\d+)");

            var pair = tokenList.Dequeue();

            //No error handling here. Assuming valid inputs.
            var matches = pattern.Match(pair);

            x1 = int.Parse(matches.Groups["x"].Value);
            y1 = int.Parse(matches.Groups["y"].Value);

            //trash the "through"
            tokenList.Dequeue();

            pair = tokenList.Dequeue();

            matches = pattern.Match(pair);

            x2 = int.Parse(matches.Groups["x"].Value);
            y2 = int.Parse(matches.Groups["y"].Value);
        }

        private static void ToggleRange(bool[,] grid, int x1, int y1, int x2, int y2)
        {
            ApplyFuncOnRange(grid, (b) => !b, x1, y1, x2, y2);
        }

        private static void TurnRange(bool[,] grid, bool turnOn, int x1, int y1, int x2, int y2)
        {
            ApplyFuncOnRange(grid, (b) => turnOn, x1, y1, x2, y2);
        }

        private static void IncreaseRangeBy2(int[,] grid, int x1, int y1, int x2, int y2)
        {
            ApplyFuncOnRange(grid, (b) => b + 2, x1, y1, x2, y2);
        }

        private static void AddOrSubtract1(int[,] grid, bool increase, int x1, int y1, int x2, int y2)
        {
            var delta = increase ? 1 : -1;

            ApplyFuncOnRange(grid, (b) => Math.Max(0, b + delta), x1, y1, x2, y2);
        }

        private static void ApplyFuncOnRange<T>(T[,] grid, Func<T, T> function, int x1, int y1, int x2, int y2)
        {
            for (var i = y1; i <= y2; i++)
            {
                for (var j = x1; j <= x2; j++)
                {
                    grid[i, j] = function(grid[i, j]);
                }
            }
        }

        private static int CountLights(bool[,] grid)
        {
            var count = 0;

            foreach (var b in grid)
            {
                if (b) count++;
            }

            return count;
        }

        private static int SubIntensities(int[,] grid)
        {
            var sum = 0;

            foreach (var b in grid)
            {
                sum += b;
            }

            return sum;
        }
    }
}
