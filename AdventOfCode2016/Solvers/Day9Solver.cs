using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal class Day9Solver
    {
        public static Day9Solver CreateForPart1()
        {
            return new Day9Solver();
        }

        public static Day9Solver CreateForPart2()
        {
            return new Day9Solver()
            {
                ExpandMarkersInDecryptedData = true
            };
        }

        public bool ExpandMarkersInDecryptedData { get; set; }
        private long _expandedFileLength;

        public long GetSolution(string fileText)
        {
            fileText = fileText.Trim();

            return GetSizeOfDecryptedText(fileText);
        }

        private long GetSizeOfDecryptedText(string fileText)
        {
            var calculatedSize = 0L;
            while (fileText.Length > 0)
            {
                var startOfMarker = fileText.IndexOf('(');
                if (startOfMarker >= 0)
                {
                    calculatedSize += startOfMarker;
                    fileText = fileText.Substring(startOfMarker + 1);

                    var endOfMarker = fileText.IndexOf(')');
                    var markerContents = fileText.Substring(0, endOfMarker);
                    var operands = markerContents.Split('x').Select(int.Parse).ToArray();

                    fileText = fileText.Substring(endOfMarker + 1);

                    var charactersToRepeat = operands[0];
                    var numberOfRepeats = operands[1];

                    var repeatedSection = fileText.Substring(0, charactersToRepeat);
                    var lengthFromRepeatedSection = ExpandMarkersInDecryptedData
                        ? GetSizeOfDecryptedText(repeatedSection)
                        : repeatedSection.Length;

                    calculatedSize += numberOfRepeats * lengthFromRepeatedSection;

                    fileText = fileText.Length > charactersToRepeat
                        ? fileText.Substring(charactersToRepeat)
                        : string.Empty;
                }
                else
                {
                    calculatedSize += fileText.Length;
                    fileText = string.Empty;
                }
            }
            return calculatedSize;
        }
    }
}