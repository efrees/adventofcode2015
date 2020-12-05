using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day6Solver
    {
        private List<Dictionary<char, int>> _counters;
        private IGetCharacterFromCountsStrategy _getCharacterFromCountsStrategy;

        public static Day6Solver CreateForPart1()
        {
            return new Day6Solver
            {
                _getCharacterFromCountsStrategy = new GetCharacterWithHighestCountStrategy()
            };
        }

        public static Day6Solver CreateForPart2()
        {
            return new Day6Solver
            {
                _getCharacterFromCountsStrategy = new GetCharacterWithLowestCountStrategy()
            };
        }

        private Day6Solver()
        {
        }

        public string GetSolution(string fileText)
        {
            foreach (var line in fileText.SplitIntoLines())
            {
                MakeSureCountersHaveBeenInitialized(line);

                for (var i = 0; i < line.Length; i++)
                {
                    CountCharacter(i, line[i]);
                }
            }

            var solutionCharArray = _counters
                .Select(_getCharacterFromCountsStrategy.GetCharacter)
                .ToArray();

            return new string(solutionCharArray);
        }

        private void CountCharacter(int column, char character)
        {
            var colCounter = _counters[column];
            if (!colCounter.ContainsKey(character))
            {
                colCounter[character] = 1;
            }
            else
            {
                colCounter[character] += 1;
            }
        }

        private void MakeSureCountersHaveBeenInitialized(string line)
        {
            if (_counters != null) return;

            _counters = Enumerable.Range(1, line.Length)
                .Select(i => new Dictionary<char, int>())
                .ToList();
        }

        private interface IGetCharacterFromCountsStrategy
        {
            char GetCharacter(Dictionary<char, int> counter);
        }

        private class GetCharacterWithHighestCountStrategy : IGetCharacterFromCountsStrategy
        {
            public char GetCharacter(Dictionary<char, int> counter)
            {
                return counter.OrderByDescending(p => p.Value)
                    .Select(p => p.Key)
                    .First();
            }
        }

        internal class GetCharacterWithLowestCountStrategy : IGetCharacterFromCountsStrategy
        {
            public char GetCharacter(Dictionary<char, int> counter)
            {
                return counter.OrderBy(p => p.Value)
                    .Select(p => p.Key)
                    .First();
            }
        }
    }
}