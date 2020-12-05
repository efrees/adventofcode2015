namespace AdventOfCode2016.Solvers.Day24Classes
{
    internal class Day24Node
    {
        public Day24Node(int id)
        {
            Identifier = id;
        }

        public Day24Node(int id, int gridRow, int gridCol)
        {
            Identifier = id;
            GridRow = gridRow;
            GridCol = gridCol;
        }

        public int Identifier { get; }
        public int GridRow { get; set; }
        public int GridCol { get; set; }
    }
}