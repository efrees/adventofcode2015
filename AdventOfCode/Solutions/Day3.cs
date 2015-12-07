using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    public class Day3
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day3input.txt");
        }
        
        /// <param name="problemNum">must be zero-based indication of problem</param>
        public static void ProcessInput(StreamReader inputReader, int problemNum = 1)
        {
            HashSet<Tuple<int, int>> visitedHouses = new HashSet<Tuple<int, int>>();

            string inputLine = null;
            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                int[] Xs = { 0, 0 };
                int[] Ys = { 0, 0 };
                var turn = 0; //0 = santa, 1 = robo

                visitedHouses.Add(Tuple.Create(Xs[turn], Ys[turn]));

                foreach (var ch in inputLine)
                {
                    switch (ch)
                    {
                        case '<': Xs[turn]--; break;
                        case '>': Xs[turn]++; break;
                        case '^': Ys[turn]++; break;
                        case 'v': Ys[turn]--; break;
                        default:
                            //other character
                            break;
                    }

                    visitedHouses.Add(Tuple.Create(Xs[turn], Ys[turn]));

                    turn = (turn + problemNum) % 2;
                }
            }

            Console.WriteLine("Houses visited: " + visitedHouses.Count);
            //Console.WriteLine("Total ribbon needed: " + totalRibbon);
        }
    }
}
