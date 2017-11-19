using System;
using System.IO;

namespace AdventOfCode.Solutions
{
    internal static class Day17
    {
        public static void ProcessInput(StreamReader fileReader)
        {
            //Assumes input is 20 or fewer containers.
            var containers = Array.ConvertAll(fileReader.ReadToEnd()
                    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            var totalCombinations = Math.Pow(2, containers.Length);
            var qualifyingCombinations = 0;

            var minQualifyingContainers = int.MaxValue;
            var countQualifyingAtMinCount = 0;

            var combination = 1;
            while (combination < totalCombinations)
            {
                if (CombinationSum(combination, containers) == 150)
                {
                    qualifyingCombinations++;
                    var containerCount = ContainerCount(combination);
                    if (containerCount < minQualifyingContainers)
                    {
                        minQualifyingContainers = containerCount;
                        countQualifyingAtMinCount = 1;
                    }
                    else if (containerCount == minQualifyingContainers)
                    {
                        countQualifyingAtMinCount++;
                    }
                }
                combination++;
            }

            Console.WriteLine($"Combinations with exact sum (P1): {qualifyingCombinations}");
            Console.WriteLine($"Min count among qualifying combinations: {minQualifyingContainers}");
            Console.WriteLine($"Combinations with min count (P2): {countQualifyingAtMinCount}");
        }

        private static int CombinationSum(int combination, int[] containers)
        {
            var sum = 0;
            var index = 0;
            while (combination > 0)
            {
                var indexIsIncluded = (combination & 1) == 1;
                if (indexIsIncluded)
                {
                    sum += containers[index];
                }

                combination >>= 1;
                index++;
            }

            return sum;
        }

        private static int ContainerCount(int combination)
        {
            var count = 0;
            while (combination > 0)
            {
                count++;
                combination &= combination - 1;
            }
            return count;
        }
    }
}