using System;
using System.IO;

namespace AdventOfCode
{
    public class Day1
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day1input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            var floor = 0;
            int? firstTimeInBasement = null;

            var input = inputReader.ReadLine();

            var charNumber = 0;
            foreach(var ch in input)
            {
                charNumber++;

                if (ch == '(')
                {
                    floor++;
                }
                else if (ch == ')')
                {
                    floor--;
                }
                else
                {
                    //meaningless character
                }

                if (floor == -1 && !firstTimeInBasement.HasValue)
                {
                    firstTimeInBasement = charNumber;
                }
            }

            Console.WriteLine("End on floor: " + floor);
            Console.WriteLine("First time in basement: " + firstTimeInBasement);
        }
    }
}
