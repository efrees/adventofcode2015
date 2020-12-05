using AdventOfCode2016.Solvers.Day18Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers.Day18Classes
{
    internal class Day18RowGenerator_should_
    {
        [Test]
        public void generate_row_with_same_length_as_previous()
        {
            var initialRow = Any.AlphaNumericString();

            var nextRow = new Day18RowGenerator().GetNextRow(initialRow);

            Assert.AreEqual(initialRow.Length, nextRow.Length);
        }

        [TestCase(".^", "^.")]
        [TestCase("^.", ".^")]
        [TestCase("^..", ".^.")]
        public void set_next_row_tile_as_trap_if_trap_to_left_or_right(string initialRow, string expectedRow)
        {
            var actualNextRow = new Day18RowGenerator().GetNextRow(initialRow);

            Assert.AreEqual(expectedRow, actualNextRow);
        }

        [TestCase("^.^", "...")]
        [TestCase("^^^", "^.^")]
        public void not_set_next_row_tile_as_trap_if_trap_to_both_left_and_right(string initialRow, string expectedRow)
        {
            var actualNextRow = new Day18RowGenerator().GetNextRow(initialRow);

            Assert.AreEqual(expectedRow, actualNextRow);
        }
    }
}
