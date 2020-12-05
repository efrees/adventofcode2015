using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Solvers
{
    internal class Day22Solver
    {
        public static Day22Solver CreateForPart1()
        {
            return new Day22Solver();
        }

        public static Day22Solver CreateForPart2()
        {
            return new Day22Solver
            {
                CountOperations = true
            };
        }

        public bool CountOperations { get; set; }

        public int GetSolution(string fileText)
        {
            const int boilerplateLineCount = 2;
            var nodeDescriptions = fileText.SplitIntoLines()
                .Skip(boilerplateLineCount)
                .Select(ParseDiskInfo)
                .ToList();

            return CountOperations
                ? SolveAndCountMoveOperations(nodeDescriptions)
                : CountViablePairs(nodeDescriptions);
        }

        private DiskSpaceInfo ParseDiskInfo(string arg)
        {
            var tokens = arg.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var match = Regex.Match(tokens[0], ".*-x(?<xval>\\d+)-y(?<yval>\\d+)");
            return new DiskSpaceInfo
            {
                X = int.Parse(match.Groups["xval"].Value),
                Y = int.Parse(match.Groups["yval"].Value),
                Used = int.Parse(tokens[2].TrimEnd('T')),
                Available = int.Parse(tokens[3].TrimEnd('T'))
            };
        }

        private static int CountViablePairs(List<DiskSpaceInfo> nodeDescriptions)
        {
            var count = 0;
            foreach (var disk in nodeDescriptions)
            {
                count += nodeDescriptions.Count(n => PairIsViable(disk, n));
            }
            return count;
        }

        private static bool PairIsViable(DiskSpaceInfo disk1, DiskSpaceInfo disk2)
        {
            return disk2 != disk1
                   && disk1.Used > 0
                   && disk2.Available >= disk1.Used;
        }

        private int SolveAndCountMoveOperations(List<DiskSpaceInfo> nodeDescriptions)
        {
            //Lots of simplifying assumptions justified by the input
            // - No disk "merges" possible.
            // - Only a single disk has enough space to accept data.
            // - This disk is big enough to accept any but the "obstacle" payloads.
            // - Obstacles don't affect our path, except in the first movement. (detour cost: 8)
            var maxX = nodeDescriptions.Max(d => d.X);
            var maxY = nodeDescriptions.Max(d => d.Y);
            var grid = new DiskSpaceInfo[maxX + 1, maxY + 1];
            nodeDescriptions.ForEach(d => grid[d.X, d.Y] = d);
            var goalDisk = grid[maxX, 0];
            goalDisk.HasTargetData = true;
            var emptyDisk = nodeDescriptions.First(d => d.Used == 0);
            var operationCount = 8;
            while (goalDisk.X != 0)
            {
                PrintGrid(grid, maxX, maxY);
                operationCount += MoveEmptyDiskInFrontOfGoalData(grid, emptyDisk, goalDisk);
                operationCount += MoveGoalDiskIntoEmptyDisk(grid, emptyDisk, goalDisk);
            }
            return operationCount;
        }

        private int MoveEmptyDiskInFrontOfGoalData(DiskSpaceInfo[,] grid, DiskSpaceInfo emptyDisk, DiskSpaceInfo goalDisk)
        {
            var moveToX = goalDisk.X - 1;
            var diskToReplace = grid[moveToX, 0];
            var needsToDodgeGoalDisk = emptyDisk.Y == 0 && emptyDisk.X > goalDisk.X;
            SwapDisks(grid, emptyDisk, diskToReplace);
            return Math.Abs(emptyDisk.X - diskToReplace.X)
                       + Math.Abs(emptyDisk.Y - diskToReplace.Y)
                       + (needsToDodgeGoalDisk ? 2 : 0);
        }

        private int MoveGoalDiskIntoEmptyDisk(DiskSpaceInfo[,] grid, DiskSpaceInfo emptyDisk, DiskSpaceInfo goalDisk)
        {
            Debug.Assert(emptyDisk.X == goalDisk.X - 1);
            Debug.Assert(emptyDisk.Y == goalDisk.Y);

            SwapDisks(grid, emptyDisk, goalDisk);
            return 1;
        }

        private static void SwapDisks(DiskSpaceInfo[,] grid, DiskSpaceInfo emptyDisk, DiskSpaceInfo diskToReplace)
        {
            var moveToX = diskToReplace.X;
            var moveToY = diskToReplace.Y;
            diskToReplace.X = emptyDisk.X;
            diskToReplace.Y = emptyDisk.Y;
            grid[emptyDisk.X, emptyDisk.Y] = diskToReplace;
            emptyDisk.X = moveToX;
            emptyDisk.Y = moveToY;
            grid[moveToX, moveToY] = emptyDisk;
        }

        private void PrintGrid(DiskSpaceInfo[,] grid, int maxX, int maxY)
        {
            for (var j = 0; j <= maxY; j++)
            {
                for (var i = 0; i <= maxX; i++)
                {
                    var representation = '.';
                    if (grid[i, j].Used > 86)
                    {
                        representation = '#';
                    }
                    else if (grid[i, j].Available > 30)
                    {
                        representation = '_';
                    }
                    else if (grid[i, j].HasTargetData)
                    {
                        representation = 'G';
                    }
                    Console.Write(representation);
                }
                Console.WriteLine();
            }
        }
    }

    internal class DiskSpaceInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Used { get; set; }
        public int Available { get; set; }
        public bool HasTargetData { get; set; }
    }
}