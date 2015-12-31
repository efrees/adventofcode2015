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
    public class Day11
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day11input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            string inputLine = inputReader.ReadLine();

            //Input has 8 characters. Unless there is already a pair or a straight in the first four characters,
            // then we'll have to at least touch the last four, since the most compact way to have two pairs and
            // a straight takes 5 characters. It's possible we'll have to touch more, only if the 5th from the end
            // cannot be used without the 4th from the end having to wrap.
            string nextPassword = GetNextPassword(inputLine);

            Console.WriteLine("Next password: " + nextPassword);

            nextPassword = GetNextPassword(nextPassword);

            Console.WriteLine("Next password (2): " + nextPassword);
        }

        private static string GetNextPassword(string inputLine)
        {
            //Not all cases for the generic problem have been implemented.
            // I've visually inspected the known input and there isn't a pair or a straight to start with.
            string nextPassword = "n/a";

            if (inputLine[3] <= 'x')
            {
                //Check whether we can double this character and base a straight off of it.
                nextPassword = inputLine.Substring(0, 4)
                                    + inputLine[3] + NextChar(inputLine[3]) + NextChar(NextChar(inputLine[3])) //straight
                                    + NextChar(NextChar(inputLine[3])); //double the last char

                if (IntValue(nextPassword) <= IntValue(inputLine))
                {
                    //we've already passed this one. Search further.
                    if (inputLine[3] < 'x')
                    {
                        //We can still increment at this point
                        var tempLine = SkipCharacter(inputLine, 3);
                        return GetNextPassword(tempLine);
                    }
                    else
                    {
                        //we're gonna need to increment the third character to wrap this one around
                        var tempLine = SkipCharacter(inputLine, 2);
                        return GetNextPassword(tempLine);
                    }
                }
            }

            return nextPassword;
        }

        private static long IntValue(string input)
        {
            long value = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //move through the string backwards
                var cVal = input[input.Length - i - 1] - 'a'; //count 'a' as zero

                value += cVal * (long)Math.Pow(26, i);
            }

            return value;
        }

        private static int FindStraight(string input, int length = 3)
        {
            for (int i = 0; i <= input.Length - length; i++)
            {
                var isStraight = true;
                var curC = input[i];

                for (int j = i + 1; j < i + length; j++)
                {
                    if (curC - input[j] != 1)
                    {
                        //No straight for this start char. Break out one level.
                        isStraight = false;
                        break;
                    }
                    else
                    {
                        //Keep checking.
                        curC = input[j];
                    }
                }

                if (isStraight)
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindDouble(string input, int startSearch = 0)
        {
            for (int i = startSearch; i < input.Length - 1; i++)
            {
                if (input[i] == input[i + 1])
                {
                    return i;
                }
            }
            return -1;
        }

        private static string SkipCharacter(string input, int index)
        {
            return input.Substring(0, index) + NextChar(input[index]) + new string('a', input.Length - index - 1);
        }

        private static char NextChar(char c)
        {
            return (char)(((int)(c - 'a') + 1) % 26 + 'a');
        }
    }
}
