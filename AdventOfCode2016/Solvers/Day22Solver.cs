using System;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day22Solver
    {
        public static Day22Solver CreateForPart1()
        {
            return new Day22Solver();
        }

        public int GetSolution(string fileText)
        {
            const int boilerplateLineCount = 2;
            var nodeDescriptions = fileText.SplitIntoLines()
                .Skip(boilerplateLineCount)
                .Select(ParseDiskInfo)
                .ToList();

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

        private DiskSpaceInfo ParseDiskInfo(string arg)
        {
            var tokens = arg.Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new DiskSpaceInfo
            {
                Used = int.Parse(tokens[2].TrimEnd('T')),
                Available = int.Parse(tokens[3].TrimEnd('T'))
            };
        }
    }

    internal class DiskSpaceInfo
    {
        public int Used { get; set; }
        public int Available { get; set; }
    }
}