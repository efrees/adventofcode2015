using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions
{
    public class Day10
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day10input.txt");
        }
        
        public static void ProcessInput(StreamReader inputReader)
        {
            var totalCodeChars = 0;
            var totalMemoryChars = 0;
            var totalEncodedChars = 0;

            string inputLine = inputReader.ReadLine();
            string outputLine = inputLine;

            int repetitions = 40;
            for (int i = 0; i < repetitions; i++)
            {
                outputLine = Encode(outputLine);
            }

            Console.WriteLine("Length of string after 40 reps: " + outputLine.Length);

            for (int i = 0; i < 10; i++)
            {
                outputLine = Encode(outputLine);
            }

            Console.WriteLine("Length of string after 50 reps: " + outputLine.Length);

            //Console.WriteLine("Difference between encoded and original chars: " + (totalEncodedChars - totalCodeChars));
        }

        public static string Encode(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return inputString;

            var sb = new StringBuilder();

            //Read string
            char c = inputString.First();
            int curCount = 1;

            for(int i = 1; i < inputString.Length; i++)
            {
                if (inputString[i] == c)
                {
                    curCount++;
                }
                else
                {
                    sb.Append(curCount);
                    sb.Append(c);

                    curCount = 1;
                    c = inputString[i];
                }
            }

            //The last one won't have been added, since the character never changed.
            sb.Append(curCount);
            sb.Append(c);

            return sb.ToString();
        }
    }
}
