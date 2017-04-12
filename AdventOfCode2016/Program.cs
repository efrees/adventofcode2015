using System;
using System.IO;

namespace AdventOfCode2016
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var fileText = GetInputFromFile("day24input.txt");
            var result = Day17Solver.CreateForPart2().GetSolution();

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("./InputFiles/" + filename);
        }
    }
}
