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
    public class Day13
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day13input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            var regex = new Regex("(?<p1>\\w+) would (?<dir>gain|lose) (?<val>\\d+) happiness units by sitting next to (?<p2>\\w+).");

            var people = new Dictionary<string, int>();
            var happinessScores = new Dictionary<PairOfPersons, int>();

            int currentIndex = 0;
            string inputLine;

            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                var matches = regex.Match(inputLine);

                if (matches.Groups.Count > 3)
                {
                    var p1 = matches.Groups["p1"].Value;
                    var p2 = matches.Groups["p2"].Value;
                    var dir = matches.Groups["dir"].Value;
                    var val = matches.Groups["val"].Value;

                    if (!people.ContainsKey(p1))
                    {
                        people[p1] = currentIndex++;
                    }

                    if (!people.ContainsKey(p2))
                    {
                        people[p2] = currentIndex++;
                    }

                    var pairKey = new PairOfPersons(p1, p2);
                    int value = int.Parse(val);

                    if (dir == "lose")
                    {
                        value = -value;
                    }

                    if (happinessScores.ContainsKey(pairKey))
                    {
                        happinessScores[pairKey] += value;
                    }
                    else
                    {
                        happinessScores[pairKey] = value;
                    }
                }
                else
                {
                    //Bad input.
                }
            }

            //Try all permutations of the people list. Expecting a small list, so O(n!) is feasible (and quicker than me becoming smart).
            // Just kidding. We'll at least reduce it to O((n-1)!), since rotations of the list are equivalent.
            var peopleList = people.Keys.ToList();
            var firstPerson = peopleList.First();
            peopleList.RemoveAt(0);

            var maxHappiness = int.MinValue;
            var maxHappinessWithUs = int.MinValue;

            foreach (var perm in GetPermutations(peopleList))
            {
                var score = happinessScores[new PairOfPersons(firstPerson, perm.First())];
                var minScore = score;
                var happiness = score;

                score = happinessScores[new PairOfPersons(firstPerson, perm.Last())];
                happiness += score;

                //Track the minimum, since this is the one we'll zero out when we add ourself (problem 2)
                if (score < minScore) minScore = score;

                for (int i = 0; i < perm.Count - 1; i++)
                {
                    score = happinessScores[new PairOfPersons(perm[i], perm[i + 1])];
                    happiness += score;

                    if (score < minScore) minScore = score;
                }

                if (happiness > maxHappiness)
                {
                    maxHappiness = happiness;
                }

                if (happiness - minScore > maxHappinessWithUs)
                {
                    maxHappinessWithUs = happiness - minScore;
                }
            }

            Console.WriteLine("Optimal happiness result: " + maxHappiness);
            Console.WriteLine("Optimal happiness with us added (P2): " + maxHappinessWithUs);
        }

        private static IEnumerable<List<string>> GetPermutations(List<string> original)
        {
            //Strategy: Try each element (including the first) in the first spot. Recursively
            // try all permutations of the rest of the list.
            if (original.Count <= 1)
            {
                yield return original;
            }
            else
            {
                for (int i = 0; i < original.Count; i++)
                {
                    var head = original[i];
                    var tail = original.Where((s, x) => x != i).ToList();

                    foreach (var tailPerm in GetPermutations(tail))
                    {
                        tailPerm.Insert(0, head);
                        yield return tailPerm;
                    }
                }
            }
        }

        private class PairOfPersons
        {
            string person1;
            string person2;

            public PairOfPersons(string person1, string person2)
            {
                if (person1.CompareTo(person2) <= 0)
                {
                    this.person1 = person1;
                    this.person2 = person2;
                }
                else
                {
                    this.person1 = person2;
                    this.person2 = person1;
                }
            }

            public override bool Equals(object obj)
            {
                var pair2 = obj as PairOfPersons;

                if (pair2 != null)
                {
                    return pair2.person1 == person1 && pair2.person2 == person2;
                }

                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return (person1.GetHashCode() * 7) + person2.GetHashCode();
            }
        }
    }
}
