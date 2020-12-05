using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    public class Day15
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day15input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            const int totalTeaspoonsLimit = 100;
            var ingredientList = new List<Ingredient>();

            while (!inputReader.EndOfStream)
            {
                var inputLine = inputReader.ReadLine();

                ingredientList.Add(Ingredient.ParseFromLine(inputLine));
            }

            var maxScoreSoFar = 0;
            var maxScoreWith500Calories = 0;
            var countPerIngredient = new int[ingredientList.Count];
            while (true)
            {
                var ingredientToIncrement = ingredientList.Count - 2;
                countPerIngredient[ingredientToIncrement]++;
                countPerIngredient[countPerIngredient.Length - 1] =
                    100 - countPerIngredient.Take(countPerIngredient.Length - 1).Sum();

                var score = ScoreForCounts(ingredientList, countPerIngredient);

                if (score > maxScoreSoFar)
                {
                    PrintCounts(countPerIngredient);
                    maxScoreSoFar = score;
                }

                if (score > maxScoreWith500Calories && CalorieCount(ingredientList, countPerIngredient) == 500)
                {
                    maxScoreWith500Calories = score;
                }

                while (ingredientToIncrement > 0 && countPerIngredient[ingredientToIncrement] >= 100)
                {
                    countPerIngredient[ingredientToIncrement] = 0;

                    ingredientToIncrement--;
                    countPerIngredient[ingredientToIncrement]++;
                }

                if (ingredientToIncrement == 0 && countPerIngredient[0] == 100)
                {
                    break;
                }
            }

            Console.WriteLine($"Best score (P1): {maxScoreSoFar}"); //18965440 - for my input
            Console.WriteLine($"Best score with 500 calories (P2): {maxScoreWith500Calories}");
        }
        
        private static void PrintCounts(int[] counts)
        {
            Console.WriteLine($"{counts[0]}, {counts[1]}, {counts[2]}, {counts[3]}");
        }

        private static int ScoreForCounts(List<Ingredient> ingredientList, int[] countPerIngredient)
        {
            var propertyTotals = PropertyTotalsFromCounts(ingredientList, countPerIngredient);

            return propertyTotals.Aggregate((total, value) => total * (value < 0 ? 0 : value));
        }

        private static int[] PropertyTotalsFromCounts(List<Ingredient> ingredientList, int[] countPerIngredient)
        {
            var propertyTotals = new int[4];

            for (var i = 0; i < ingredientList.Count; i++)
            {
                for (var p = 0; p < propertyTotals.Length; p++)
                {
                    propertyTotals[p] += countPerIngredient[i] * ingredientList[i].Properties[p];
                }
            }
            return propertyTotals;
        }

        private static int CalorieCount(List<Ingredient> ingredientList, int[] countPerIngredient)
        {
            var calories = 0;
            for (var i = 0; i < ingredientList.Count; i++)
            {
                calories += ingredientList[i].Calories * countPerIngredient[i];
            }
            return calories;
        }

        private class Ingredient
        {
            private static readonly Regex IngredientPattern =
                new Regex("(?<ingredient>\\w+): capacity (?<capacity>[-\\d]+), durability (?<durability>[-\\d]+)," +
                          " flavor (?<flavor>[-\\d]+), texture (?<texture>[-\\d]+), calories (?<calories>[-\\d]+)");

            public static Ingredient ParseFromLine(string inputLine)
            {
                var parsedInput = IngredientPattern.Match(inputLine);
                Debug.Assert(parsedInput.Success);

                return new Ingredient
                {
                    Name = parsedInput.Groups["ingredient"].Value,
                    Properties = new[]
                    {
                        int.Parse(parsedInput.Groups["capacity"].Value),
                        int.Parse(parsedInput.Groups["durability"].Value),
                        int.Parse(parsedInput.Groups["flavor"].Value),
                        int.Parse(parsedInput.Groups["texture"].Value)
                    },
                    Calories = int.Parse(parsedInput.Groups["calories"].Value)
                };
            }

            public string Name { get; set; }
            public int[] Properties { get; private set; }
            public int Calories { get; private set; }
        }
    }
}