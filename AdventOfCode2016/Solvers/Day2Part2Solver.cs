using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Solvers
{
    public class Day2Part2Solver
    {
        protected int IntegerPaddedMaxValue = int.MaxValue - 100; //avoids overflow

        public int GetSolution(string inputText)
        {
            ParsedComponentsByFloor = new Dictionary<int, HashSet<Day2Component>>();

            ParseInputIntoComponents(inputText);

            AddPart2Components();

            var stepsRequired = StartAtFirstFloorAndSolve();

            return stepsRequired;
        }

        private void AddPart2Components()
        {
            ParsedComponentsByFloor[0].Add(new Day2Component {Type = "generator", Element = "elerium"});
            ParsedComponentsByFloor[0].Add(new Day2Component {Type = "microchip", Element = "elerium"});
            ParsedComponentsByFloor[0].Add(new Day2Component {Type = "generator", Element = "dilithium"});
            ParsedComponentsByFloor[0].Add(new Day2Component {Type = "microchip", Element = "dilithium"});
        }

        public HashSet<string> PathToGetHere { get; } = new HashSet<string>();

        public Dictionary<string, int> RecordedResults { get; } = new Dictionary<string, int>();

        private int StartAtFirstFloorAndSolve()
        {
            var currentFloor = 0;
            var currentState = DeepCopy(ParsedComponentsByFloor);
            return FindCostOfBestMove(currentFloor, currentState);
        }

        private int FindCostOfBestMove(int currentFloor, Dictionary<int, HashSet<Day2Component>> currentState)
        {
            if (currentFloor == 3 && AllComponentsAreOnFourthFloor(currentState))
                return 0;

            var stateIdentifier = currentFloor + GetStateIdentifier(currentState);

            if (PathToGetHere.Contains(stateIdentifier))
                return IntegerPaddedMaxValue;

            if (RecordedResults.ContainsKey(stateIdentifier))
            {
                return RecordedResults[stateIdentifier];
            }

            if (AnyFloorIsInvalid(currentState))
            {
                return IntegerPaddedMaxValue;
            }

            PathToGetHere.Add(stateIdentifier);

            var bestCostFromAllMoves = TryAllMovesToFindBest(currentFloor, currentState);

            PathToGetHere.Remove(stateIdentifier);

            RecordedResults[stateIdentifier] = bestCostFromAllMoves;

            return bestCostFromAllMoves;
        }

        private bool AnyFloorIsInvalid(Dictionary<int, HashSet<Day2Component>> currentState)
        {
            foreach (var key in currentState.Keys)
            {
                if (FloorIsInvalid(currentState[key]))
                    return true;
            }
            return false;
        }

        private bool FloorIsInvalid(HashSet<Day2Component> floorComponents)
        {
            var generatorElements = floorComponents
                .Where(c => c.Type == "generator")
                .Select(c => c.Element);

            var microchipElements = floorComponents
                .Where(c => c.Type == "microchip")
                .Select(c => c.Element);

            return generatorElements.Any() && microchipElements.Except(generatorElements).Any();
        }

        private int TryAllMovesToFindBest(int currentFloor, Dictionary<int, HashSet<Day2Component>> currentState)
        {
            var bestCostSoFar = int.MaxValue;
            var floorComponents = currentState[currentFloor].ToList();
            for (var index = 0; index < floorComponents.Count; index++)
            {
                var item = floorComponents[index];
                var currentCost = GetCostOfMovingItemUp(currentFloor, currentState, item);

                if (currentCost < bestCostSoFar)
                {
                    bestCostSoFar = currentCost;
                }

                currentCost = GetCostOfMovingItemDown(currentFloor, currentState, item);

                if (currentCost < bestCostSoFar)
                {
                    bestCostSoFar = currentCost;
                }

                for (var j = index + 1; j < floorComponents.Count; j++)
                {
                    var otherItem = floorComponents[j];

                    if (ItemsAreCompatibleForMove(item, otherItem))
                    {
                        currentCost = GetCostOfMovingItemPairUp(currentFloor, currentState, item, otherItem);

                        if (currentCost < bestCostSoFar)
                        {
                            bestCostSoFar = currentCost;
                        }
                    }
                }
            }
            return bestCostSoFar;
        }

        private bool ItemsAreCompatibleForMove(Day2Component item, Day2Component otherItem)
        {
            return item.Type == otherItem.Type
                   || item.Element == otherItem.Element;
        }

        private string GetStateIdentifier(Dictionary<int, HashSet<Day2Component>> currentState)
        {
            var sb = new StringBuilder();

            foreach (var key in currentState.Keys.OrderBy(k => k))
            {
                sb.Append(key);
                foreach (var component in currentState[key].OrderBy(c => c.Type).ThenBy(c => c.Element))
                {
                    sb.Append(component.Type);
                    sb.Append(component.Element);
                }
            }

            return sb.ToString();
        }

        private bool AllComponentsAreOnFourthFloor(Dictionary<int, HashSet<Day2Component>> currentState)
        {
            return !currentState[0].Any()
                   && !currentState[1].Any()
                   && !currentState[2].Any();
        }

        private int GetCostOfMovingItemUp(int currentFloor, Dictionary<int, HashSet<Day2Component>> currentState, Day2Component item)
        {
            if (currentFloor >= 3) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor + 1;

            return CostOfMovingItemToFloor(currentFloor, nextFloor, currentState, item);
        }

        private int GetCostOfMovingItemDown(int currentFloor, Dictionary<int, HashSet<Day2Component>> currentState, Day2Component item)
        {
            if (currentFloor <= 0) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor - 1;

            return CostOfMovingItemToFloor(currentFloor, nextFloor, currentState, item);
        }

        private int CostOfMovingItemToFloor(int currentFloor, int nextFloor, Dictionary<int, HashSet<Day2Component>> currentState, Day2Component item)
        {
            currentState[currentFloor].Remove(item);
            currentState[nextFloor].Add(item);

            var currentCost = 1 + FindCostOfBestMove(nextFloor, currentState);

            currentState[nextFloor].Remove(item);
            currentState[currentFloor].Add(item);
            return currentCost;
        }

        private int GetCostOfMovingItemPairUp(int currentFloor, Dictionary<int, HashSet<Day2Component>> currentState, Day2Component item, Day2Component compatibleItem)
        {
            if (currentFloor >= 3) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor + 1;

            return CostOfMovingItemPairToFloor(currentFloor, nextFloor, currentState, item, compatibleItem);
        }

        private int GetCostOfMovingItemPairDown(int currentFloor, Dictionary<int, HashSet<Day2Component>> currentState, Day2Component item, Day2Component compatibleItem)
        {
            if (currentFloor <= 0) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor - 1;

            return CostOfMovingItemPairToFloor(currentFloor, nextFloor, currentState, item, compatibleItem);
        }

        private int CostOfMovingItemPairToFloor(int currentFloor, int nextFloor, Dictionary<int, HashSet<Day2Component>> currentState, Day2Component item,
            Day2Component compatibleItem)
        {
            currentState[currentFloor].Remove(item);
            currentState[currentFloor].Remove(compatibleItem);
            currentState[nextFloor].Add(item);
            currentState[nextFloor].Add(compatibleItem);

            var currentCost = 1 + FindCostOfBestMove(nextFloor, currentState);

            currentState[nextFloor].Remove(item);
            currentState[nextFloor].Remove(compatibleItem);
            currentState[currentFloor].Add(item);
            currentState[currentFloor].Add(compatibleItem);
            return currentCost;
        }

        private Dictionary<int, HashSet<Day2Component>> DeepCopy(Dictionary<int, HashSet<Day2Component>> componentsByFloor)
        {
            var copiedComponentsByFloor = new Dictionary<int, HashSet<Day2Component>>();
            foreach (var key in componentsByFloor.Keys)
            {
                copiedComponentsByFloor[key] = new HashSet<Day2Component>(componentsByFloor[key].AsEnumerable());
            }
            return copiedComponentsByFloor;
        }

        private void ParseInputIntoComponents(string inputText)
        {
            var lines = inputText.Trim()
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Split('\n');

            for (var i = 0; i < lines.Length; i++)
            {
                ParsedComponentsByFloor[i] = new HashSet<Day2Component>();

                var generatorMatches = Regex.Matches(lines[i], " (?<element>\\w+) generator");

                foreach (Match match in generatorMatches)
                {
                    ParsedComponentsByFloor[i].Add(new Day2Component
                    {
                        Type = "generator",
                        Element = match.Groups["element"].Value
                    });
                }

                var microchipMatches = Regex.Matches(lines[i], " (?<element>\\w+)-compatible microchip");

                foreach (Match match in microchipMatches)
                {
                    ParsedComponentsByFloor[i].Add(new Day2Component
                    {
                        Type = "microchip",
                        Element = match.Groups["element"].Value
                    });
                }
            }
        }

        public Dictionary<int, HashSet<Day2Component>> ParsedComponentsByFloor { get; set; }
    }
}