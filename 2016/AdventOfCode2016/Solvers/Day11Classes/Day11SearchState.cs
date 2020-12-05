using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Solvers.Day11Classes
{
    public class Day11SearchState
    {
        public Dictionary<int, HashSet<Day11Component>> Floors { get; } = new Dictionary<int, HashSet<Day11Component>>();

        public HashSet<string> PathToGetHere { get; } = new HashSet<string>();

        public ISet<Day11Component> FirstFloor => Floors[0];
        public ISet<Day11Component> SecondFloor => Floors[1];
        public ISet<Day11Component> ThirdFloor => Floors[2];
        public ISet<Day11Component> FourthFloor => Floors[3];

        public Day11SearchState()
        {
            Floors[0] = new HashSet<Day11Component>();
            Floors[1] = new HashSet<Day11Component>();
            Floors[2] = new HashSet<Day11Component>();
            Floors[3] = new HashSet<Day11Component>();
        }

        public bool AnyFloorIsInvalid()
        {
            foreach (var key in Floors.Keys)
            {
                if (FloorIsInvalid(Floors[key]))
                    return true;
            }
            return false;
        }

        private bool FloorIsInvalid(HashSet<Day11Component> floorComponents)
        {
            var generatorElements = floorComponents
                .Where(c => c.Type == "generator")
                .Select(c => c.Element);

            var microchipElements = floorComponents
                .Where(c => c.Type == "microchip")
                .Select(c => c.Element);

            return generatorElements.Any() && microchipElements.Except(generatorElements).Any();
        }

        public string GetStateIdentifier()
        {
            var sb = new StringBuilder();

            var nextElementColor = 91;
            var colors = new Dictionary<string, int>();
            for (var i = 0; i < 4; i++)
            {
                sb.Append("Floor" + i);
                foreach (var component in Floors[i].OrderBy(c => c.Element).ThenBy(c => c.Type))
                {
                    sb.Append(component.Type[0]);

                    if (!colors.ContainsKey(component.Element))
                    {
                        colors[component.Element] = nextElementColor;
                        nextElementColor++;
                    }

                    sb.Append(colors[component.Element]);
                }
            }

            return sb.ToString();
        }

        public bool AllComponentsAreOnFourthFloor()
        {
            return !Floors[0].Any()
                   && !Floors[1].Any()
                   && !Floors[2].Any();
        }

        public Day11SearchState DeepCopy()
        {
            var copiedComponentsByFloor = new Day11SearchState();
            foreach (var key in Floors.Keys)
            {
                copiedComponentsByFloor.Floors[key] = new HashSet<Day11Component>(Floors[key].AsEnumerable());
            }
            return copiedComponentsByFloor;
        }

        public static Day11SearchState ParseInputIntoComponents(string inputText)
        {
            var newSearchState = new Day11SearchState();

            var lines = inputText.Trim()
                    .Replace("\r\n", "\n")
                    .Replace("\r", "\n")
                    .Split('\n');

            for (var i = 0; i < lines.Length; i++)
            {
                newSearchState.Floors[i] = new HashSet<Day11Component>();

                var generatorMatches = Regex.Matches(lines[i], " (?<element>\\w+) generator");

                foreach (Match match in generatorMatches)
                {
                    newSearchState.Floors[i].Add(new Day11Component
                    {
                        Type = "generator",
                        Element = match.Groups["element"].Value
                    });
                }

                var microchipMatches = Regex.Matches(lines[i], " (?<element>\\w+)-compatible microchip");

                foreach (Match match in microchipMatches)
                {
                    newSearchState.Floors[i].Add(new Day11Component
                    {
                        Type = "microchip",
                        Element = match.Groups["element"].Value
                    });
                }
            }

            return newSearchState;
        }

        public void MoveItemFromFloorToFloor(int currentFloor, int nextFloor,
            Day11Component item)
        {
            Floors[currentFloor].Remove(item);
            Floors[nextFloor].Add(item);
        }
    }
}