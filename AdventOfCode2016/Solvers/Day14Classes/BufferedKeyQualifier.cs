using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers.Day14Classes
{
    internal class BufferedKeyQualifier
    {
        private const int QualificationLookAheadRange = 1000;
        private static string _hexadecimalCharacters = "0123456789abcdef";
        private readonly IEnumerable<string> _hexadecimalQuintuples = _hexadecimalCharacters
            .Select(c => new string(Enumerable.Repeat(c, 5).ToArray()));

        private readonly ISet<int>[] _quintupleLocations = _hexadecimalCharacters.Select(c => new HashSet<int>()).ToArray();

        public readonly IEnumerable<string> SequenceGenerator;

        public BufferedKeyQualifier(IEnumerable<string> sequenceGenerator)
        {
            SequenceGenerator = sequenceGenerator;
        }

        public virtual IEnumerable<Tuple<int, string>> GetQualifiedKeyStream()
        {
            var index = 0;

            var buffer = new Queue<Tuple<int, string>>();

            foreach (var sequenceElement in SequenceGenerator)
            {
                buffer.Enqueue(Tuple.Create(index, sequenceElement));
                RecordIfHasQuintuple(index, sequenceElement);
                index++;

                if (buffer.Count < QualificationLookAheadRange + 1)
                    continue;

                var candidate = buffer.Dequeue();
                if (IsQualifiedKey(candidate.Item1, candidate.Item2))
                {
                    yield return candidate;
                }

            }
        }

        private bool IsQualifiedKey(int index, string keyCandidate)
        {
            var lastChar = '-';
            var sequenceStart = -1;
            for (var i = 0; i < keyCandidate.Length; i++)
            {
                if (keyCandidate[i] != lastChar)
                {
                    lastChar = keyCandidate[i];
                    sequenceStart = i;
                }

                if (i - sequenceStart == 2)
                {
                    return QuintupleWasFoundInNextThousand(lastChar, index);
                }
            }
            return false;
        }

        private void RecordIfHasQuintuple(int index, string sequenceElement)
        {
            foreach (var quintuple in _hexadecimalQuintuples)
            {
                if (sequenceElement.Contains(quintuple))
                {
                    var hexValue = _hexadecimalCharacters.IndexOf(quintuple.First());
                    _quintupleLocations[hexValue].Add(index);
                }
            }
        }

        private bool QuintupleWasFoundInNextThousand(char quintupleChar, int currentIndex)
        {
            var hexValue = _hexadecimalCharacters.IndexOf(quintupleChar);
            return _quintupleLocations[hexValue].Any(i => i > currentIndex && i <= currentIndex + QualificationLookAheadRange);
        }
    }
}