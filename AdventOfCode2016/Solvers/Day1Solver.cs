using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    public class Day1Solver
    {
        private static HashSet<Tuple<int, int>> _visitedLocations = new HashSet<Tuple<int, int>>();

        public static int GetSolution(string fileText)
        {
            var directions = Enumerable.Select(fileText.Split(','), d => d.Trim());

            var finalLocation = GetRelativeLocationFromDirections(directions);
            var result = GetDistanceToRelativeLocation(finalLocation);
            return result;
        }

        public static void ResetSolver()
        {
            _visitedLocations.Clear();
        }

        internal static BlockLocation GetRelativeLocationFromDirections(IEnumerable<string> instructions)
        {
            var currentLocation = new BlockLocation();
            var currentDirection = Direction.North;

            Visit(currentLocation);

            foreach (var instruction in instructions)
            {
                currentDirection = instruction[0] == 'R'
                    ? currentDirection.GetRightTurnDirection()
                    : currentDirection.GetLeftTurnDirection();

                var distance = int.Parse(instruction.Substring(1));
                var previousLocation = currentLocation;
                currentLocation = currentLocation.Move(currentDirection.GetMovement(distance));

                if (Program.EnablePart2Rule)
                {
                    var earlyMatch = LogAndCheckForPart2(previousLocation, currentDirection, distance);

                    if (earlyMatch != null) return earlyMatch;
                }
            }

            return currentLocation;
        }

        private static BlockLocation LogAndCheckForPart2(BlockLocation previousLocation, Direction direction, int distance)
        {
            var incrementalMovement = direction.GetMovement(1);
            var incrementalDestination = previousLocation;
            for (int i = 0; i < distance; i++)
            {
                incrementalDestination = incrementalDestination.Move(incrementalMovement);
                if (HaveVisited(incrementalDestination))
                {
                    return incrementalDestination;
                }
                Visit(incrementalDestination);
            }
            return null;
        }

        private static void Visit(BlockLocation location)
        {
            _visitedLocations.Add(Tuple.Create(location.X, location.Y));
        }

        internal static bool HaveVisited(BlockLocation location)
        {
            return _visitedLocations.Contains(Tuple.Create(location.X, location.Y));
        }

        private static int GetDistanceToRelativeLocation(BlockLocation finalLocation)
        {
            return Math.Abs(finalLocation.X) + Math.Abs(finalLocation.Y);
        }
    }
}