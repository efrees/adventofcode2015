using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    public class Day9
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day9input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            //This algorithm is not designed for very large numbers of cities.
            var distances = new DistanceMatrix<string, string>();

            string inputLine;
            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                var parsed = Regex.Match(inputLine, "(\\w+) to (\\w+) = (\\d+)");

                if (parsed.Groups.Count > 3)
                {
                    var city1 = parsed.Groups[1].Value;
                    var city2 = parsed.Groups[2].Value;
                    var distanceString = parsed.Groups[3].Value;

                    int distance = int.Parse(distanceString);

                    distances[city1, city2] = distance;
                    distances[city2, city1] = distance;
                }
                else
                {
                    //Doesn't look like valid input to me.
                }
            }

            Console.WriteLine(string.Format("{0} cities in total.", distances.Keys.Count));

            var shortest = FindShortestPath(distances.Keys.ToList(), distances);
            var longest = FindLongestPath(distances.Keys.ToList(), distances);

            Console.WriteLine("Shortest path length: " + shortest);
            Console.WriteLine("Longest path length: " + longest);
        }

        private static int FindShortestPath(List<string> cities, DistanceMatrix<string, string> distances)
        {
            var shortestLength = int.MaxValue;

            foreach (var city1 in cities)
            {
                //Try starting at this city
                var length = FindShortestPathRecursive(city1, cities, distances);

                if (length < shortestLength)
                {
                    shortestLength = length;
                }
            }

            return shortestLength;
        }

        private static int FindShortestPathRecursive(string city1, List<string> cities, DistanceMatrix<string, string> distances)
        {
            var shortestLength = int.MaxValue;

            //This algorithm is not extremely efficient in either memory or processing time.
            var citiesCopy = cities.ToList();
            citiesCopy.Remove(city1);

            if (citiesCopy.Count == 1)
            {
                if (distances.ContainsPair(city1, citiesCopy.First()))
                {
                    shortestLength = distances[city1, citiesCopy.First()];
                }
            }
            else
            {
                //loop to find the shortest
                foreach (var city2 in citiesCopy)
                {
                    if (!distances.ContainsPair(city1, city2))
                    {
                        //Not an option.
                        continue;
                    }

                    var length = distances[city1, city2]
                        + FindShortestPathRecursive(city2, citiesCopy, distances);

                    //negative values would indicate we added MaxValue
                    if (length > 0 && length < shortestLength)
                    {
                        shortestLength = length;
                    }
                }
            }

            return shortestLength;
        }

        private static int FindLongestPath(List<string> cities, DistanceMatrix<string, string> distances)
        {
            var longestLength = 0;

            foreach (var city1 in cities)
            {
                //Try starting at this city
                var length = FindLongestPathRecursive(city1, cities, distances);

                if (length > longestLength)
                {
                    longestLength = length;
                }
            }

            return longestLength;
        }

        private static int FindLongestPathRecursive(string city1, List<string> cities, DistanceMatrix<string, string> distances)
        {
            var longestLength = 0;

            //This algorithm is not extremely efficient in either memory or processing time.
            var citiesCopy = cities.ToList();
            citiesCopy.Remove(city1);

            if (citiesCopy.Count == 1)
            {
                if (distances.ContainsPair(city1, citiesCopy.First()))
                {
                    longestLength = distances[city1, citiesCopy.First()];
                }
            }
            else
            {
                //loop to find the longest
                foreach (var city2 in citiesCopy)
                {
                    if (!distances.ContainsPair(city1, city2))
                    {
                        //Not an option.
                        continue;
                    }

                    var length = distances[city1, city2]
                        + FindLongestPathRecursive(city2, citiesCopy, distances);
                    
                    if (length > longestLength)
                    {
                        longestLength = length;
                    }
                }
            }

            return longestLength;
        }

        private class DistanceMatrix<TKey1, TKey2> : Dictionary<TKey1, Dictionary<TKey2, int>>
        {
            public bool ContainsPair(TKey1 key1, TKey2 key2)
            {
                return ContainsKey(key1) && this[key1].ContainsKey(key2);
            }

            public int this[TKey1 key1, TKey2 key2]
            {
                get
                {
                    if (this[key1] == null)
                        throw new KeyNotFoundException();

                    return this[key1][key2];
                }
                set
                {
                    if (!ContainsKey(key1) || this[key1] == null)
                    {
                        this[key1] = new Dictionary<TKey2, int>();
                    }

                    this[key1][key2] = value;
                }
            }
        }
    }
}
