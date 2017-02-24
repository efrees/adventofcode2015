using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2016.Solvers
{
    internal class Day4Solver
    {
        public static Day4Solver Create()
        {
            return new Day4Solver();
        }

        public int GetSolution(string fileText)
        {
            var sectorIdSum = 0;
            foreach (var line in fileText.SplitIntoLines())
            {
                var sectorDescriptor = Day4Sector.CreateFromLine(line);
                if (sectorDescriptor.IsValidSector())
                {
                    var decryptedName = sectorDescriptor.GetDecryptedName();

                    if (decryptedName.Contains("north"))
                        ReportSectorIdOfNorthPoleObjectStorage(sectorDescriptor.SectorId);

                    sectorIdSum += sectorDescriptor.SectorId;
                }
            }
            return sectorIdSum;
        }

        internal virtual void ReportSectorIdOfNorthPoleObjectStorage(int sectorId)
        {
            Console.WriteLine("North Pole object storage sector : " + sectorId);
        }

        private class Day4Sector
        {
            private readonly string _encryptedName;
            private readonly int _sectorId;
            private readonly string _checksumString;

            public int SectorId => _sectorId;

            private Day4Sector(string encryptedName, int sectorId, string checksumString)
            {
                _encryptedName = encryptedName;
                _sectorId = sectorId;
                _checksumString = checksumString;
            }

            public static Day4Sector CreateFromLine(string line)
            {
                var lastHyphenIndex = line.LastIndexOf('-');
                var firstBracketIndex = line.IndexOf('[');
                var encryptedName = line.Substring(0, lastHyphenIndex);
                var sectorIdString = line.Substring(lastHyphenIndex + 1, firstBracketIndex - lastHyphenIndex - 1);
                var checksumString = line.Substring(firstBracketIndex + 1).TrimEnd(']');

                var sectorId = int.Parse(sectorIdString);
                return new Day4Sector(encryptedName, sectorId, checksumString);
            }

            public bool IsValidSector()
            {
                var charactersOrderedByCount = GetCharactersOrderedByCountDescending(_encryptedName.Replace("-", ""));

                var expectedCheckSum = new string(charactersOrderedByCount.Take(5).ToArray());
                return expectedCheckSum == _checksumString;
            }

            private IEnumerable<char> GetCharactersOrderedByCountDescending(string name)
            {
                return name.GroupBy(c => c, (k, group) => new { Character = k, Count = group.Count() })
                    .OrderByDescending(g => g.Count)
                    .ThenBy(g => g.Character) //Ties are broken alphabetically
                    .Select(g => g.Character);
            }

            public string GetDecryptedName()
            {
                var sb = new StringBuilder();
                foreach (var ch in _encryptedName)
                {
                    sb.Append(DecryptCharacter(ch));
                }
                return sb.ToString();
            }

            private char DecryptCharacter(char ch)
            {
                if (ch == '-')
                    return ' ';

                var charIntValue = (int)ch - 'a';
                var rotatedIntValue = (charIntValue + SectorId) % 26;
                return (char)(rotatedIntValue + 'a');
            }
        }
    }
}