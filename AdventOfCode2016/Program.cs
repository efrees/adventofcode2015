using System;
using System.IO;
using AdventOfCode2016.Solvers;

namespace AdventOfCode2016
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day4input.txt");
            var result = Day4Solver.Create().GetSolution(fileText);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("./InputFiles/" + filename);
        }
    }
}
