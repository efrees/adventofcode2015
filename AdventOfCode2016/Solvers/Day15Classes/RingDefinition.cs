using System;
using System.Text.RegularExpressions;

namespace AdventOfCodeTests.Solvers
{
    internal class RingDefinition
    {
        public static RingDefinition Create(string inputLine)
        {
            var descriptionPattern = "Disc #(?<discNumber>\\d+) has (?<positions>\\d+) positions; at time=0, it is at position (?<initialOffset>\\d+).";
            var match = Regex.Match(inputLine, descriptionPattern);

            if (!match.Success)
                throw new InvalidOperationException(inputLine + " is not a valid ring definition.");

            return new RingDefinition
            {
                DiscNumber = Convert.ToInt32(match.Groups["discNumber"].Value),
                Positions = Convert.ToInt32(match.Groups["positions"].Value),
                StartingOffset = Convert.ToInt32(match.Groups["initialOffset"].Value)
            };
        }

        public int DiscNumber { get; set; }
        public int Positions { get; set; }
        public int StartingOffset { get; set; }

        public int GetPositionAtTime(int time)
        {
            return (StartingOffset + time) % Positions;
        }
    }
}