using System;
using AdventOfCode2016;
using AdventOfCode2016.Solvers.Day8Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class RotatableGrid_should_
    {
        [Test]
        public void start_with_all_spaces_empty()
        {
            var grid = new RotatableGrid(7, 3);
            var expectedStringRepresentation = "......." + Environment.NewLine
                                             + "......." + Environment.NewLine
                                             + "......." + Environment.NewLine;
            Assert.AreEqual(expectedStringRepresentation, grid.ToString());
        }

        [Test]
        public void draw_rectangle()
        {
            var grid = new RotatableGrid(7, 3);
            grid.DrawRectangle(3, 2);
            var expectedStringRepresentation = "###...." + Environment.NewLine
                                             + "###...." + Environment.NewLine
                                             + "......." + Environment.NewLine;
            Assert.AreEqual(expectedStringRepresentation, grid.ToString());
        }

        [Test]
        public void rotate_row()
        {
            var grid = new RotatableGrid(7, 3);
            grid.DrawRectangle(3, 2);

            grid.RotateRow(1, 1);
            var expectedStringRepresentation = "###...." + Environment.NewLine
                                             + ".###..." + Environment.NewLine
                                             + "......." + Environment.NewLine;
            Assert.AreEqual(expectedStringRepresentation, grid.ToString());
        }

        [Test]
        public void rotate_column()
        {
            var grid = new RotatableGrid(7, 3);
            grid.DrawRectangle(3, 2);

            grid.RotateColumn(1, 1);
            var expectedStringRepresentation = "#.#...." + Environment.NewLine
                                             + "###...." + Environment.NewLine
                                             + ".#....." + Environment.NewLine;
            Assert.AreEqual(expectedStringRepresentation, grid.ToString());

            grid.RotateColumn(1, 1);
            expectedStringRepresentation = "###...." + Environment.NewLine
                                         + "#.#...." + Environment.NewLine
                                         + ".#....." + Environment.NewLine;
            Assert.AreEqual(expectedStringRepresentation, grid.ToString());
        }

        [Test]
        public void process_example_sequence()
        {
            var grid = new RotatableGrid(7, 3);
            grid.DrawRectangle(3, 2);
            grid.RotateColumn(1, 1);
            grid.RotateRow(0, 4);
            grid.RotateColumn(1, 1);
            var expectedStringRepresentation = ".#..#.#" + Environment.NewLine
                                             + "#.#...." + Environment.NewLine
                                             + ".#....." + Environment.NewLine;

            Assert.AreEqual(expectedStringRepresentation, grid.ToString());
        }
    }
}
