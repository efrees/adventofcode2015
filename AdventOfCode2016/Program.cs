using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day1input_small.txt");
            var directions = fileText.Split(',').Select(d => d.Trim());

            var finalLocation = GetRelativeLocationFromDirections(directions);
            var result = GetDistanceToRelativeLocation(finalLocation);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        internal static BlockLocation GetRelativeLocationFromDirections(IEnumerable<string> instructions)
        {
            var currentLocation = new BlockLocation();
            var currentDirection = Direction.North;

            foreach (var instruction in instructions)
            {
                currentDirection = instruction[0] == 'R'
                    ? currentDirection.GetRightTurnDirection()
                    : currentDirection.GetLeftTurnDirection();

                var distance = int.Parse(instruction.Substring(1));
                currentLocation = currentLocation.Move(currentDirection.GetMovement(distance));
            }

            return currentLocation;
        }

        private static int GetDistanceToRelativeLocation(BlockLocation finalLocation)
        {
            return Math.Abs(finalLocation.X) + Math.Abs(finalLocation.Y);
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("./InputFiles/" + filename);
        }
    }
}
