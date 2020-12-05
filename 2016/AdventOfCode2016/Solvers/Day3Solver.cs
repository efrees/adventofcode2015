using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day3Solver
    {
        public static Day3Solver CreateForPart1()
        {
            return new Day3Solver();
        }

        public static Day3Solver CreateForPart2()
        {
            return new Day3Solver { _transposeTriples = true };
        }

        private bool _transposeTriples;

        private Day3Solver() { }

        public int GetSolution(string fileText)
        {
            var splitLines = fileText.Trim().Split('\n');

            var possibleCount = 0;

            var triplesFromInputLines = _transposeTriples
                ? GetTransposedTriplesFromInputLines(splitLines)
                : GetTriplesFromInputLines(splitLines);

            foreach (var triple in triplesFromInputLines)
            {
                Array.Sort(triple);

                if (triple[0] + triple[1] > triple[2])
                    possibleCount++;
            }

            return possibleCount;
        }

        private IEnumerable<int[]> GetTransposedTriplesFromInputLines(string[] splitLines)
        {
            var tripleBuffer = new List<int[]>();

            foreach (var triple in GetTriplesFromInputLines(splitLines))
            {
                tripleBuffer.Add(triple);

                if (tripleBuffer.Count == 3)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var transposedTriple = new[]
                        {
                            tripleBuffer[0][j],
                            tripleBuffer[1][j],
                            tripleBuffer[2][j]
                        };
                        yield return transposedTriple;
                    }

                    tripleBuffer.Clear();
                }
            }
        }

        private static IEnumerable<int[]> GetTriplesFromInputLines(string[] inputLines)
        {
            for (int index = 0; index < inputLines.Length; index++)
            {
                var line = inputLines[index];
                var integers = GetTripleFromInputLine(line);
                if (integers.Length < 3)
                    Debug.WriteLine($"Looks like invalid input: {line}");

                yield return integers;
            }
        }

        private static int[] GetTripleFromInputLine(string line)
        {
            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Select(w => int.Parse(w)).ToArray();
        }
    }
}