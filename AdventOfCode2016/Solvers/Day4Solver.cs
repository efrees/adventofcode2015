using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Solvers
{
    internal class Day4Solver
    {
        public static Day4Solver CreateForPart1()
        {
            return new Day4Solver();
        }

        public int GetSolution(string fileText)
        {
            var sectorIdSum = 0;
            foreach (var line in fileText.SplitIntoLines())
            {
                if (IsValidSector(line))
                {
                    sectorIdSum += GetSectorId(line);
                }
            }
            return sectorIdSum;
        }

        private bool IsValidSector(string inputLine)
        {
            var encryptedName = inputLine.Substring(0, inputLine.LastIndexOf("-"));
            var charactersOrderedByCount = GetCharactersOrderedByCountDescending(encryptedName.Replace("-", ""));

            var expectedCheckSum = new string(charactersOrderedByCount.Take(5).ToArray());

            var checkSumExtraction = Regex.Match(inputLine, "\\[(\\w+)\\]");
            var actualCheckSum = checkSumExtraction.Success ? checkSumExtraction.Groups[1].Value : "";

            return expectedCheckSum == actualCheckSum;
        }

        private int GetSectorId(string inputLine)
        {
            var lastHyphenIndex = inputLine.LastIndexOf('-');
            var firstBracketIndex = inputLine.IndexOf('[');
            var sectorIdString = inputLine.Substring(lastHyphenIndex + 1, firstBracketIndex - lastHyphenIndex - 1);

            return int.Parse(sectorIdString);
        }

        private IEnumerable<char> GetCharactersOrderedByCountDescending(string name)
        {
            return name.GroupBy(c => c, (k, group) => new { Character = k, Count = group.Count() })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.Character) //Ties are broken alphabetically
                .Select(g => g.Character);
        }
    }
}