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

        public static Day18Solver Create()
        {
            return new Day18Solver(new Day18RowGenerator(), 40);
        }

        internal Day18Solver(Day18RowGenerator day18RowGenerator, int rowCount)
        {
            _day18RowGenerator = day18RowGenerator;
            _targetRowCount = rowCount;
        }

        public int GetSolution(string inputText)
        {
            var count = 1;
            var rows = new StringBuilder(inputText);
            while (count++ < _targetRowCount)
            {
                inputText = _day18RowGenerator.GetNextRow(inputText.Trim());
                rows.Append(inputText);
            }

            return rows.ToString().Count(TileIsSafe);
        }

        private static bool TileIsSafe(char c)
        {
            return c == '.';
        }
    }
}