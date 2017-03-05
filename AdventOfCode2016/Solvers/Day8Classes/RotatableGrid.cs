using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2016.Solvers.Day8Classes
{
    internal class RotatableGrid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private readonly bool[,] _rawGrid;
        private readonly int[] _rowOffsets;
        private readonly int[] _columnOffsets;

        public RotatableGrid(int width, int height)
        {
            Width = width;
            Height = height;
            _rawGrid = new bool[height, width];
            _rowOffsets = Enumerable.Repeat(0, height).ToArray();
            _columnOffsets = Enumerable.Repeat(0, width).ToArray();
        }

        public int CountOfSetPixels()
        {
            var count = 0;
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    if (GetGrid(i, j)) count++;
                }
            }
            return count;
        }

        public void DrawRectangle(int width, int height)
        {
            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    SetGrid(i, j, true);
                }
            }
        }

        public void RotateRow(int row, int amount)
        {
            while (amount < 0)
            {
                amount += Width;
            }
            amount = amount % Width;

            var temp = new Queue<bool>();
            for (var i = -amount; i < Width; i++)
            {
                temp.Enqueue(GetGrid(i, row));

                if (temp.Count > amount)
                {
                    SetGrid(i, row, temp.Dequeue());
                }
            }
        }

        public void RotateColumn(int column, int amount)
        {
            while (amount < 0)
            {
                amount += Height;
            }
            amount = amount % Height;

            var temp = new Queue<bool>();
            for (var i = -amount; i < Height; i++)
            {
                temp.Enqueue(GetGrid(column, i));

                if (temp.Count > amount)
                {
                    SetGrid(column, i, temp.Dequeue());
                }
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    var cellRepresentation = GetGrid(i, j) ? '#' : '.';
                    stringBuilder.Append(cellRepresentation);
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        private bool GetGrid(int x, int y)
        {
            var effectiveX = (x + Width) % Width;
            var effectiveY = (y + Height) % Height;
            return _rawGrid[effectiveY, effectiveX];
        }

        private void SetGrid(int x, int y, bool newValue)
        {
            _rawGrid[y, x] = newValue;
        }
    }
}