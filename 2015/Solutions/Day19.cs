using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCode.Solutions
{
    internal class Day19
    {
        public static void ProcessInput(StreamReader fileReader)
        {
            var fileLines = fileReader.ReadToEnd()
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var targetMolecule = fileLines.Last();

            var replacements = fileLines.Take(fileLines.Count - 1)
                .Select(line => Regex.Match(line, "(\\w+) => (\\w+)"))
                .Where(match => match.Success)
                .ToLookup(match => match.Groups[1].Value, match => match.Groups[2].Value);

            var setReachableInOneReplacement = GetSetReachableInOneReplacement(targetMolecule, replacements);

            Console.WriteLine($"Distinct molecule count from calibration (P1): {setReachableInOneReplacement.Count}");

            var rootProduction = DoEarleyParse(targetMolecule, replacements);
            //Console.WriteLine($"Steps to reach target (P2): {foundDepth}");
            Console.WriteLine($"Parse depth (P2): {rootProduction?.Depth}");
        }

        private static HashSet<string> GetSetReachableInOneReplacement(string startMolecule, ILookup<string, string> replacements)
        {
            var setReachableInOneReplacement = new HashSet<string>();
            for (var i = 0; i < startMolecule.Length; i++)
            {
                //Relies on there being no ambiguous keys in input
                var possibleKey = startMolecule.Substring(i, 1);
                foreach (var target in replacements[possibleKey])
                {
                    var replacementResult = ReplaceAtIndex(startMolecule, i, possibleKey.Length, target);
                    setReachableInOneReplacement.Add(replacementResult);
                }

                if (i + 1 < startMolecule.Length)
                {
                    possibleKey = startMolecule.Substring(i, 2);
                    foreach (var target in replacements[possibleKey])
                    {
                        var replacementResult = ReplaceAtIndex(startMolecule, i, possibleKey.Length, target);
                        setReachableInOneReplacement.Add(replacementResult);
                    }
                }
            }
            return setReachableInOneReplacement;
        }

        private static EarleyParseState DoEarleyParse(string targetMolecule, ILookup<string, string> replacements)
        {
            targetMolecule = "CRnFArThRnFAr";
            var tokens = Tokenize(targetMolecule).ToArray();
            var stateLists = Enumerable.Range(1, tokens.Length)
                .Select(x => new List<EarleyParseState>())
                .ToArray();
            var stateSets = Enumerable.Range(1, tokens.Length)
                .Select(x => new HashSet<EarleyParseState>())
                .ToArray();

            var seedState = new EarleyParseState
            {
                OriginPosition = 0,
                ProductionBase = "Start",
                ProductionOutput = "e",
                ParseCursor = 0,
                Depth = 0
            };
            stateLists[0].Add(seedState);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var k = 0; k < tokens.Length; k++)
            {
                for (var i = 0; i < stateLists[k].Count; i++)
                {
                    var currentStateSet = stateLists[k];
                    var state = currentStateSet[i];
                    if (state.IsFinished)
                    {
                        // completer rule
                        var matchingRules = stateLists[state.OriginPosition]
                            .Where(s => s.NextTokenMatches(state.ProductionBase));
                        foreach (var rule in matchingRules)
                        {
                            AddIfNotDuplicate(currentStateSet, stateSets[k], new EarleyParseState
                            {
                                OriginPosition = rule.OriginPosition,
                                ProductionBase = rule.ProductionBase,
                                ProductionOutput = rule.ProductionOutput,
                                ParseCursor = rule.ParseCursor + state.ProductionBase.Length,
                                Depth = rule.Depth + state.Depth
                            });
                        }
                    }
                    else
                    {
                        var nextToken = Tokenize(state.ProductionOutput.Substring(state.ParseCursor)).First();
                        foreach (var ruleOutput in replacements[nextToken])
                        {
                            // predictor rule
                            AddIfNotDuplicate(currentStateSet, stateSets[k], new EarleyParseState
                            {
                                OriginPosition = k,
                                ProductionBase = nextToken,
                                ProductionOutput = ruleOutput,
                                ParseCursor = 0,
                                Depth = state.Depth + 1
                            });
                        }
                        // scanner rule
                        if (k + 1 < stateLists.Length)
                        {
                            AddIfNotDuplicate(stateLists[k + 1], stateSets[k + 1], new EarleyParseState
                            {
                                OriginPosition = state.OriginPosition,
                                ProductionBase = state.ProductionBase,
                                ProductionOutput = state.ProductionOutput,
                                ParseCursor = state.ParseCursor + nextToken.Length,
                                Depth = state.Depth
                            });
                        }
                    }
                }
            }

            stopwatch.Stop();
            var rootProduction = stateLists[tokens.Length - 1]
                .FirstOrDefault(s => s.OriginPosition == 0
                          && s.ProductionBase == "Start"
                          && s.ProductionOutput == "e");
            Console.WriteLine($"Parse success? {rootProduction != null} in {stopwatch.Elapsed}");
            return rootProduction;
        }

        private static void AddIfNotDuplicate(List<EarleyParseState> currentOrderedList, HashSet<EarleyParseState> membership, EarleyParseState newState)
        {
            if (!membership.Contains(newState))
            {
                currentOrderedList.Add(newState);
                membership.Add(newState);
            }
        }

        private static IEnumerable<string> Tokenize(string targetMolecule)
        {
            for (var i = 0; i < targetMolecule.Length; i++)
            {
                if (i + 1 < targetMolecule.Length && char.IsLower(targetMolecule[i + 1]))
                {
                    yield return targetMolecule.Substring(i, 2);
                    i++;
                }
                else
                {
                    yield return targetMolecule.Substring(i, 1);
                }
            }
        }

        private static string ReplaceAtIndex(string calibrationMolecule, int i, int length, string replacement)
        {
            var sb = new StringBuilder(calibrationMolecule.Substring(0, i));
            sb.Append(replacement);

            if (calibrationMolecule.Length > i + length)
            {
                sb.Append(calibrationMolecule.Substring(i + length));
            }
            return sb.ToString();
        }

        private class EarleyParseState
        {
            public int OriginPosition { get; set; }
            public string ProductionBase { get; set; }
            public string ProductionOutput { get; set; }
            public int ParseCursor { get; set; }
            public bool IsFinished => ParseCursor >= ProductionOutput.Length;
            public int Depth { get; set; }

            public bool NextTokenMatches(string expectedToken)
            {
                if (ParseCursor + expectedToken.Length >= ProductionOutput.Length)
                    return false;

                for (int i = 0; i < expectedToken.Length; i++)
                {
                    if (ProductionOutput[ParseCursor + i] != expectedToken[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public override bool Equals(object obj)
            {
                var otherState = obj as EarleyParseState;
                if (otherState == null)
                {
                    return false;
                }
                return Equals(otherState);
            }

            private bool Equals(EarleyParseState other)
            {
                return OriginPosition == other.OriginPosition && string.Equals(ProductionBase, other.ProductionBase) && string.Equals(ProductionOutput, other.ProductionOutput) && ParseCursor == other.ParseCursor;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = OriginPosition;
                    hashCode = (hashCode * 397) ^ (ProductionBase != null ? ProductionBase.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (ProductionOutput != null ? ProductionOutput.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ ParseCursor;
                    return hashCode;
                }
            }
        }
    }
}