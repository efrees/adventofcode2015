using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AdventOfCode.Solutions
{
    public class Day4
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day4input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            string secretKeyString = inputReader.ReadLine();
            
            int addendum = 1;

            int firstAnswer = 0;
            int secondAnswer = 0;

            var hasher = MD5.Create();
            byte[] byteResult;

            do
            {
                var combined = secretKeyString + addendum;
                var bytes = Encoding.ASCII.GetBytes(combined);

                byteResult = hasher.ComputeHash(bytes);

                if (firstAnswer == 0 && StartsWithFiveHexZeros(byteResult))
                    firstAnswer = addendum;

                if (secondAnswer == 0 && StartsWithSixHexZeros(byteResult))
                    secondAnswer = addendum;

                addendum++;
            } while (firstAnswer == 0 || secondAnswer == 0);
            
            Console.WriteLine("Addendum producing 5 leading zeros: " + firstAnswer);
            Console.WriteLine("Addendum producing 6 leading zeros: " + secondAnswer);
        }

        private static bool StartsWithFiveHexZeros(byte[] byteResult)
        {
            if (byteResult.Length < 3)
                return false;

            //each byte is two hex digits
            return byteResult[0] == 0
                && byteResult[1] == 0
                && byteResult[2] < 0x10;
        }

        private static bool StartsWithSixHexZeros(byte[] byteResult)
        {
            if (byteResult.Length < 3)
                return false;

            //each byte is two hex digits
            return byteResult[0] == 0
                && byteResult[1] == 0
                && byteResult[2] == 0;
        }
    }
}
