using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    public class Day2
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day2input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            long totalArea = 0;
            long totalRibbon = 0;

            string inputLine = null;
            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                var match = Regex.Match(inputLine, "^(\\d+)x(\\d+)x(\\d+)$");

                if (match.Success && match.Groups.Count == 4)
                {
                    int x, y, z;

                    if (int.TryParse(match.Groups[1].Value, out x)
                        && int.TryParse(match.Groups[2].Value, out y)
                        && int.TryParse(match.Groups[3].Value, out z))
                    {
                        var area = (2 * x * y) + (2 * y * z) + (2 * z * x);

                        //Add the smallest side area once
                        var largest = Math.Max(x, Math.Max(y, z));
                        area += x * y * z / largest;

                        totalArea += area;

                        var ribbonLen = 2 * (x + y + z) - 2 * largest;

                        //Add the length corresponding to the cubic volume
                        ribbonLen += x * y * z;

                        totalRibbon += ribbonLen;
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Failed to parse ints from '{0}', '{1}', and '{2}'", match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Failed to parse '{0}'.", inputLine));
                }
            }

            Console.WriteLine("Total area needed: " + totalArea);
            Console.WriteLine("Total ribbon needed: " + totalRibbon);
        }
    }
}
