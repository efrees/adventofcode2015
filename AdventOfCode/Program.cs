using AdventOfCode.Solutions;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileInfo = InputFiles.GetInputFileInfo("Day18");

            if (File.Exists(fileInfo.FullName))
            {
                using (var fileStream = fileInfo.OpenRead())
                {
                    using (var fileReader = new StreamReader(fileStream))
                    {
                        Day18.ProcessInput(fileReader);
                    }
                }
            }
            else
            {
                Console.WriteLine("File Not Found");
            }
            Console.ReadKey();
        }
    }

    internal class Day18
    {
        public static void ProcessInput(StreamReader fileReader)
        {
            var numberOfStepsToTake = 100;
            var originalStates = new int[100, 100];

            int row = 0;
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine();
                for (var col = 0; col < originalStates.GetLength(1); col++)
                {
                    originalStates[row, col] = line[col] == '#' ? 1 : 0;
                }
                row++;
            }

            var numberOfLightsOn = SimulateGameFromState(originalStates, numberOfStepsToTake);
            Console.WriteLine($"Number of lights on (P1): {numberOfLightsOn}");

            var numberOfLightsOnWithCornersStuck = SimulateGameFromState(originalStates, numberOfStepsToTake, true);
            Console.WriteLine($"Number of lights on (P2): {numberOfLightsOnWithCornersStuck}");
        }

        private static int SimulateGameFromState(int[,] originalStates, int numberOfStepsToTake, bool stuckCorners = false)
        {
            var activeStates = new int[100, 100];
            var newStates = new int[100, 100];
            Array.Copy(originalStates, activeStates, 10000);

            while (numberOfStepsToTake > 0)
            {
                CalculateNewState(activeStates, newStates);

                SwapRefs(ref activeStates, ref newStates);

                if (stuckCorners)
                {
                    activeStates[0, 0] = activeStates[0, 99] = activeStates[99, 0] = activeStates[99, 99] = 1;
                }
                numberOfStepsToTake--;
            }

            return CountLightsOn(activeStates);
        }

        private static void CalculateNewState(int[,] lightStates, int[,] newStates)
        {
            for (var i = 0; i < lightStates.GetLength(0); i++)
            {
                for (var j = 0; j < lightStates.GetLength(1); j++)
                {
                    var neighborsOn = -lightStates[i, j];
                    for (var n = 0; n < 9; n++)
                    {
                        neighborsOn += SafeAccess(lightStates, i - 1 + n / 3, j - 1 + n % 3);
                    }

                    newStates[i, j] = (neighborsOn == 3 || (neighborsOn == 2 && lightStates[i, j] == 1))
                        ? 1
                        : 0;
                }
            }
        }

        private static int SafeAccess(int[,] lightStates, int i, int j)
        {
            if (i < 0 || j < 0
                || i >= lightStates.GetLength(0)
                || j >= lightStates.GetLength(1))
            {
                return 0;
            }
            return lightStates[i, j];
        }

        private static void SwapRefs(ref int[,] lightStates, ref int[,] newStates)
        {
            var t = lightStates;
            lightStates = newStates;
            newStates = t;
        }

        private static int CountLightsOn(int[,] lightStates)
        {
            return lightStates.Cast<int>().Sum();
        }
    }
}
