using System;
using System.IO;
using System.Text;

namespace AdventOfCode.Solutions
{
    public class Day8
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day8input.txt");
        }
        
        public static void ProcessInput(StreamReader inputReader)
        {
            var totalCodeChars = 0;
            var totalMemoryChars = 0;
            var totalEncodedChars = 0;

            string inputLine;
            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                totalCodeChars += inputLine.Length;

                var memoryString = Unescape(inputLine.Trim());
                totalMemoryChars += memoryString.Length;

                var escapedString = Escape(inputLine.Trim());
                totalEncodedChars += escapedString.Length;
            }

            Console.WriteLine("Difference between code and memory chars: " + (totalCodeChars - totalMemoryChars));
            Console.WriteLine("Difference between encoded and original chars: " + (totalEncodedChars - totalCodeChars));
        }

        private static string Unescape(string inputLine)
        {
            //Expecting a " character on both the front and back of the string.
            var sb = new StringBuilder();
            for (int i = 1; i < inputLine.Length - 1; i++)
            {
                if (inputLine[i] == '\\')
                {
                    if (inputLine[i + 1] == '\\')
                    {
                        //the two become one
                        sb.Append('\\');
                        i++;
                    }
                    else if (inputLine[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else if (inputLine[i + 1] == 'x')
                    {
                        //Expect two hexadecimal digits after this
                        sb.Append('X'); //I don't actually care which character
                        i += 3;
                    }
                    else
                    {
                        //invalid string, but let's keep going.
                    }
                }
                else
                {
                    sb.Append(inputLine[i]);
                }
            }

            return sb.ToString();
        }

        private static string Escape(string inputLine)
        {
            //Expecting a " character on both the front and back of the string.
            var sb = new StringBuilder();

            sb.Append('"');
            for (int i = 0; i < inputLine.Length; i++)
            {
                if (inputLine[i] == '\\')
                {
                    sb.Append(@"\\");
                }
                else if (inputLine[i] == '"')
                {
                    sb.Append(@"\""");
                }
                else
                {
                    sb.Append(inputLine[i]);
                }
            }
            sb.Append('"');

            return sb.ToString();
        }
    }
}
