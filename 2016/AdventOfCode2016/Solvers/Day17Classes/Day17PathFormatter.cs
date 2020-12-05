using System.Collections.Generic;
using System.Text;
using AdventOfCode2016.Searchers;

namespace AdventOfCode2016.Solvers.Day17Classes
{
    internal class Day17PathFormatter
    {
        public static string GetDirectionStringFromPath(IList<CartesianNode> path)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < path.Count - 1; i++)
            {
                if (path[i].X < path[i + 1].X)
                    sb.Append('R');
                else if (path[i].X > path[i + 1].X)
                    sb.Append('L');
                if (path[i].Y < path[i + 1].Y)
                    sb.Append('D');
                else if (path[i].Y > path[i + 1].Y)
                    sb.Append('U');
            }
            return sb.ToString();
        }
    }
}