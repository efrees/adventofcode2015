using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day9Solver
    {
        public static Day9Solver Create()
        {
            return new Day9Solver();
        }

        public int GetSolution(string fileText)
        {
            fileText = fileText.Trim();

            var totalCount = 0;
            while (fileText.Length > 0)
            {
                var startOfMarker = fileText.IndexOf('(');
                if (startOfMarker >= 0)
                {
                    totalCount += startOfMarker;
                    fileText = fileText.Substring(startOfMarker + 1);

                    var endOfMarker = fileText.IndexOf(')');
                    var markerContents = fileText.Substring(0, endOfMarker);
                    var operands = markerContents.Split('x').Select(int.Parse).ToArray();

                    fileText = fileText.Substring(endOfMarker + 1);

                    var charactersToRepeat = operands[0];
                    var repeatedSection = fileText.Substring(0, charactersToRepeat);

                    totalCount += charactersToRepeat * operands[1];

                    fileText = fileText.Length > charactersToRepeat
                        ? fileText.Substring(charactersToRepeat)
                        : string.Empty;
                }
                else
                {
                    totalCount += fileText.Length;
                    fileText = string.Empty;
                }
            }
            return totalCount;
        }
    }
}