using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Solvers.Day14Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day14Solver
    {
        public readonly BufferedKeyQualifier KeyQualifier;
        private readonly int _targetKeyOrdinal;

        public static Day14Solver Create()
        {
            var salt = "zpqevtbw";
            var md5Hasher = new Md5Hasher();
            var sequence = GetSequence(salt, md5Hasher);
            return new Day14Solver(new BufferedKeyQualifier(sequence), 64);
        }

        internal static Day14Solver CreateForExample()
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
    }
}