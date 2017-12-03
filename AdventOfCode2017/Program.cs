﻿using System;
using System.IO;
using AdventOfCode2017.Solvers;

namespace AdventOfCode2017
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day2input.txt");
            Day3Solver.Create().SolvePart2();

            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("../../InputFiles/" + filename);
        }
    }
}
