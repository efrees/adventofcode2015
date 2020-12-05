using System.Text;

namespace AdventOfCode2016.Solvers.Day18Classes
{
    internal class Day18RowGenerator
    {
        public virtual string GetNextRow(string initialRow)
        {
            var rowBuilder = new StringBuilder();

            for (int i = 0; i < initialRow.Length; i++)
            {
                rowBuilder.Append(NextRowCharacterForIndex(initialRow, i));
            }

            return rowBuilder.ToString();
        }

        private char NextRowCharacterForIndex(string previousRow, int index)
        {
            if (IsTrapToRightOnPreviousRow(previousRow, index) != IsTrapToLeftOnPreviousRow(previousRow, index))
            {
                return '^';
            }

            return '.';
        }

        private static bool IsTrapToLeftOnPreviousRow(string previousRow, int index)
        {
            return index > 0 && previousRow[index - 1] == '^';
        }

        private static bool IsTrapToRightOnPreviousRow(string previousRow, int index)
        {
            return index < previousRow.Length - 1 && previousRow[index + 1] == '^';
        }
    }
}