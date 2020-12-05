using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Solvers.Day14Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day14Solver
    {
        public readonly BufferedKeyQualifier KeyQualifier;
        private readonly int _targetKeyOrdinal;

        public static Day14Solver CreateForPart1()
        {
            var salt = "zpqevtbw";
            var md5Hasher = new Md5Hasher();
            var sequence = GetSequence(salt, md5Hasher);
            return new Day14Solver(new BufferedKeyQualifier(sequence), 64);
        }

        public static Day14Solver CreateForPart2()
        {
            var salt = "zpqevtbw";
            var md5Hasher = new Md5Hasher();
            var sequence = GetSequence(salt, md5Hasher);
            sequence = GetKeyStretchedSequence(sequence, md5Hasher);
            return new Day14Solver(new BufferedKeyQualifier(sequence), 64);
        }

        internal static Day14Solver CreateForPart1Example()
        {
            var salt = "abc";
            var sequence = GetSequence(salt, new Md5Hasher());
            return new Day14Solver(new BufferedKeyQualifier(sequence), 64);
        }

        public Day14Solver(BufferedKeyQualifier keyQualifier, int targetKeyOrdinal)
        {
            KeyQualifier = keyQualifier;
            _targetKeyOrdinal = targetKeyOrdinal;
        }

        public int GetSolution()
        {
            return KeyQualifier.GetQualifiedKeyStream()
                .Skip(_targetKeyOrdinal - 1)
                .First().Item1;
        }

        public static IEnumerable<string> GetSequence(string salt, Md5Hasher md5Hasher)
        {
            var maxSequenceLength = 1000000;
            return Enumerable.Range(0, maxSequenceLength)
                .Select(i => salt + i)
                .Select(data => md5Hasher.HashDataAsHexString(data).ToLower());
        }

        internal static IEnumerable<string> GetKeyStretchedSequence(IEnumerable<string> sequence, Md5Hasher md5Hasher)
        {
            var targetHashCount = 2016;
            foreach (var element in sequence)
            {
                var stretchedElement = element;
                var hashCount = 0;
                while (hashCount++ < targetHashCount)
                {
                    stretchedElement = md5Hasher.HashDataAsHexString(stretchedElement).ToLower();
                }

                yield return stretchedElement;
            }
        }
    }
}