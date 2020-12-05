using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Searchers;

namespace AdventOfCodeTests
{
    internal class Any
    {
        private static Random _random = new Random();

        public static string AlphaNumericString(int length = 10)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charArray = Enumerable.Range(1, length)
                .Select(i =>
                {
                    var index = IntBetween(0, chars.Length - 1);
                    return chars[index];
                })
                .ToArray();
            return new string(charArray);
        }

        public static bool Boolean()
        {
            return _random.Next(0, 100) < 50;
        }

        public static CartesianNode CartesianNode()
        {
            return new CartesianNode(IntBetween(0, 100), IntBetween(0, 100));
        }

        public static char DirectionCharacter()
        {
            return Boolean() ? 'L' : 'R';
        }

        public static int Integer()
        {
            return _random.Next();
        }

        public static int IntBetween(int min, int max)
        {
            return _random.Next(min, max + 1);
        }

        public static int MovementDistance()
        {
            return IntBetween(1, 1000);
        }

        public static IList<T> ListOf<T>(Func<T> generator, int length)
        {
            return Enumerable.Range(1, length)
                .Select(i => generator())
                .ToList();
        }
    }
}