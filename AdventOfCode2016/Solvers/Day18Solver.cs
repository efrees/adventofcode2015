using System;
using System.Linq;
using System.Text;
using AdventOfCode2016.Solvers.Day18Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day18Solver
    {
        private readonly Day18RowGenerator _day18RowGenerator;
        private readonly int _targetRowCount;

        public static Day18Solver CreateForPart1()
        {
            return new Day18Solver(new Day18RowGenerator(), 40);
        }

        public static Day18Solver CreateForPart2()
        {
            return new Day18Solver(new Day18RowGenerator(), 400000);
        }

        internal Day18Solver(Day18RowGenerator day18RowGenerator, int rowCount)
        {
            _day18RowGenerator = day18RowGenerator;
            _targetRowCount = rowCount;
        }

        public int GetSolution(string inputText)
        {
            var rowCount = 1;
            var safeTileCount = inputText.Count(TileIsSafe);
            while (rowCount++ < _targetRowCount)
            {
                inputText = _day18RowGenerator.GetNextRow(inputText.Trim());
                safeTileCount += inputText.Count(TileIsSafe);
            }

            return safeTileCount;
        }

        private static bool TileIsSafe(char c)
        {
            return c == '.';
        }
    }
}