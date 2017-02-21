using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    public class Day11Part2Solver
    {
        protected int IntegerPaddedMaxValue = int.MaxValue - 100; //avoids overflow
        private Day11SearchState _currentSearchState;

        public Day11SearchState SearchStartState { get; set; }
        public Dictionary<string, int> RecordedResults { get; } = new Dictionary<string, int>();

        public int GetSolution(string inputText)
        {
            SearchStartState = Day11SearchState.ParseInputIntoComponents(inputText);

            AddPart2Components();

            var stepsRequired = StartAtFirstFloorAndSolve();

            return stepsRequired;
        }

        private void AddPart2Components()
        {
            SearchStartState.Floors[0].Add(new Day11Component { Type = "generator", Element = "elerium" });
            SearchStartState.Floors[0].Add(new Day11Component { Type = "microchip", Element = "elerium" });
            SearchStartState.Floors[0].Add(new Day11Component { Type = "generator", Element = "dilithium" });
            SearchStartState.Floors[0].Add(new Day11Component { Type = "microchip", Element = "dilithium" });
        }

        private int StartAtFirstFloorAndSolve()
        {
            var currentFloor = 0;
            _currentSearchState = SearchStartState.DeepCopy();
            return FindCostOfBestMove(currentFloor);
        }

        private int FindCostOfBestMove(int currentFloor)
        {
            if (currentFloor == 3 && _currentSearchState.AllComponentsAreOnFourthFloor())
                return 0;

            if (currentFloor == 3 && _currentSearchState.FourthFloor.Any())
            {
                Debug.WriteLine(_currentSearchState.FourthFloor.Count);
            }

            var stateIdentifier = currentFloor + _currentSearchState.GetStateIdentifier();

            if (_currentSearchState.PathToGetHere.Contains(stateIdentifier))
                return IntegerPaddedMaxValue;

            if (RecordedResults.ContainsKey(stateIdentifier))
            {
                return RecordedResults[stateIdentifier];
            }

            if (_currentSearchState.AnyFloorIsInvalid())
            {
                return IntegerPaddedMaxValue;
            }

            _currentSearchState.PathToGetHere.Add(stateIdentifier);

            var bestCostFromAllMoves = TryAllMovesToFindBest(currentFloor);

            _currentSearchState.PathToGetHere.Remove(stateIdentifier);

            RecordedResults[stateIdentifier] = bestCostFromAllMoves;

            return bestCostFromAllMoves;
        }

        private int TryAllMovesToFindBest(int currentFloor)
        {
            var bestCostSoFar = int.MaxValue;
            var floorComponents = _currentSearchState.Floors[currentFloor].ToList();
            for (var index = 0; index < floorComponents.Count; index++)
            {
                var item = floorComponents[index];
                var currentCost = GetCostOfMovingItemUp(currentFloor, item);

                if (currentCost < bestCostSoFar)
                {
                    bestCostSoFar = currentCost;
                }

                currentCost = GetCostOfMovingItemDown(currentFloor, item);

                if (currentCost < bestCostSoFar)
                {
                    bestCostSoFar = currentCost;
                }

                for (var j = index + 1; j < floorComponents.Count; j++)
                {
                    var otherItem = floorComponents[j];

                    if (ItemsAreCompatibleForMove(item, otherItem))
                    {
                        currentCost = GetCostOfMovingItemPairUp(currentFloor, item, otherItem);

                        if (currentCost < bestCostSoFar)
                        {
                            bestCostSoFar = currentCost;
                        }

                        //currentCost = GetCostOfMovingItemPairDown(currentFloor, _currentSearchState, item, otherItem);

                        //if (currentCost < bestCostSoFar)
                        //{
                        //    bestCostSoFar = currentCost;
                        //}
                    }
                }
            }
            return bestCostSoFar;
        }

        private bool ItemsAreCompatibleForMove(Day11Component item, Day11Component otherItem)
        {
            return item.Type == otherItem.Type
                   || item.Element == otherItem.Element;
        }

        private int GetCostOfMovingItemUp(int currentFloor, Day11Component item)
        {
            if (currentFloor >= 3) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor + 1;

            _currentSearchState.MoveItemFromFloorToFloor(currentFloor, nextFloor, item);

            var currentCost = 1 + FindCostOfBestMove(nextFloor);

            _currentSearchState.MoveItemFromFloorToFloor(nextFloor, currentFloor, item);

            return currentCost;
        }

        private int GetCostOfMovingItemDown(int currentFloor, Day11Component item)
        {
            if (currentFloor <= 0) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor - 1;

            _currentSearchState.MoveItemFromFloorToFloor(currentFloor, nextFloor, item);

            var currentCost = 1 + FindCostOfBestMove(nextFloor);

            _currentSearchState.MoveItemFromFloorToFloor(nextFloor, currentFloor, item);

            return currentCost;
        }

        private int GetCostOfMovingItemPairUp(int currentFloor, Day11Component item, Day11Component compatibleItem)
        {
            if (currentFloor >= 3) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor + 1;

            return CostOfMovingItemPairToFloor(currentFloor, nextFloor, item, compatibleItem);
        }

        private int GetCostOfMovingItemPairDown(int currentFloor, Day11Component item, Day11Component compatibleItem)
        {
            if (currentFloor <= 0) return IntegerPaddedMaxValue;
            var nextFloor = currentFloor - 1;

            return CostOfMovingItemPairToFloor(currentFloor, nextFloor, item, compatibleItem);
        }

        private int CostOfMovingItemPairToFloor(int currentFloor, int nextFloor, Day11Component item, Day11Component compatibleItem)
        {
            _currentSearchState.MoveItemFromFloorToFloor(currentFloor, nextFloor, item);
            _currentSearchState.MoveItemFromFloorToFloor(currentFloor, nextFloor, compatibleItem);

            var currentCost = 1 + FindCostOfBestMove(nextFloor);

            _currentSearchState.MoveItemFromFloorToFloor(nextFloor, currentFloor, item);
            _currentSearchState.MoveItemFromFloorToFloor(nextFloor, currentFloor, compatibleItem);

            return currentCost;
        }
    }
}