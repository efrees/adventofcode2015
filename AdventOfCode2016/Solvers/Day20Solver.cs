using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day20Solver
    {
        public static Day20Solver Create()
        {
            return new Day20Solver();
        }

        public uint GetSolution(string fileText)
        {
            var inputLines = fileText.SplitIntoLines();
            foreach (var line in inputLines)
            {
                AddBlacklistRange(line);
            }

            SortAndSimplifyBlacklist();

            return Enumerable.Range(0, int.MaxValue)
                .Select(i => (uint)i)
                .First(IsNotBlacklisted);
        }

        private List<Range> _blacklist = new List<Range>();

        private void AddBlacklistRange(string line)
        {
            if (string.IsNullOrEmpty(line))
                return;

            var range = Array.ConvertAll(line.Split('-'), uint.Parse);
            _blacklist.Add(new Range
            {
                Start = range[0],
                End = range[1]
            });
        }

        private void SortAndSimplifyBlacklist()
        {
            _blacklist = _blacklist.OrderBy(r => r.Start).ToList();

            for (var i = 0; i < _blacklist.Count; i++)
            {
                while (i + 1 < _blacklist.Count
                       && _blacklist[i].End >= _blacklist[i + 1].Start - 1)
                {
                    _blacklist[i].End = Math.Max(_blacklist[i].End, _blacklist[i + 1].End);
                    _blacklist.RemoveAt(i + 1);
                }
            }
        }

        private bool IsNotBlacklisted(uint ip)
        {
            return !_blacklist.Any(bl => bl.Includes(ip));
        }

        private class Range
        {
            public uint Start { get; set; }
            public uint End { get; set; }

            public bool Includes(uint integer)
            {
                return Start <= integer && End >= integer;
            }
        }
    }
}