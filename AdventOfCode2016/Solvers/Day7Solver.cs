using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day7Solver
    {
        public static Day7Solver CreateForPart1()
        {
            return new Day7Solver
            {
                DesiredProtocol = "TLS"
            };
        }

        public static Day7Solver CreateForPart2()
        {
            return new Day7Solver
            {
                DesiredProtocol = "SSL"
            };
        }

        public int GetSolution(string fileText)
        {
            return fileText.SplitIntoLines()
                .Count(SupportsDesiredProtocol);
        }

        public string DesiredProtocol { get; private set; }

        private bool SupportsDesiredProtocol(string line)
        {
            if (DesiredProtocol == "TLS")
                return SupportsTls(line);

            return SupportsSsl(line);
        }

        private bool SupportsTls(string line)
        {
            return ContainsAbba(line) && DoesNotHaveAbbaInBrackets(line);
        }

        private bool SupportsSsl(string line)
        {
            var abaCandidates = GetAllUnBrackettedAbas(line);
            var babCandidates = GetAllBrackettedBabs(line);

            return babCandidates.Select(MatchingAbaFromBab)
                .Intersect(abaCandidates)
                .Any();
        }

        private string MatchingAbaFromBab(string arg)
        {
            return new string(new[] { arg[1], arg[0], arg[1] });
        }

        private IEnumerable<string> GetAllBrackettedBabs(string line)
        {
            return GetAllBrackettedSegments(line)
                .SelectMany(GetAbasInSegment);
        }

        private IEnumerable<string> GetAllUnBrackettedAbas(string line)
        {
            return GetAllUnBrackettedSegments(line)
                .SelectMany(GetAbasInSegment);
        }

        private IEnumerable<string> GetAbasInSegment(string unbrackettedSegment)
        {
            for (int i = 0; i < unbrackettedSegment.Length - 2; i++)
            {
                if (unbrackettedSegment[i] == unbrackettedSegment[i + 2]
                    && unbrackettedSegment[i] != unbrackettedSegment[i + 1])
                {
                    yield return unbrackettedSegment.Substring(i, 3);
                }
            }
        }

        private bool DoesNotHaveAbbaInBrackets(string line)
        {
            return !GetAllBrackettedSegments(line).Any(ContainsAbba);
        }

        private IEnumerable<string> GetAllBrackettedSegments(string line)
        {
            foreach (var untrimmedSegment in line.Split('[').Skip(1))
            {
                var closeOfSegment = untrimmedSegment.IndexOf(']');
                if (closeOfSegment > 0)
                {
                    var trimmedSegment = untrimmedSegment.Substring(0, closeOfSegment);
                    yield return trimmedSegment;
                }
            }
        }

        private IEnumerable<string> GetAllUnBrackettedSegments(string line)
        {
            foreach (var untrimmedSegment in line.Split('['))
            {
                var beginningOfSegment = untrimmedSegment.IndexOf(']');
                var trimmedSegment = untrimmedSegment.Substring(Math.Max(beginningOfSegment, 0));
                yield return trimmedSegment;
            }
        }

        private bool ContainsAbba(string line)
        {
            for (var i = 0; i < line.Length - 3; i++)
            {
                if (line[i] == line[i + 3]
                    && line[i + 1] == line[i + 2]
                    && line[i] != line[i + 1])
                    return true;
            }
            return false;
        }
    }
}