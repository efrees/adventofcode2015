using System.IO;

namespace AdventOfCode.Solutions
{
    internal class InputFiles
    {
        public static FileInfo GetInputFileInfo(string whichDay)
        {
            return new FileInfo($"..\\..\\Inputs\\{whichDay}input.txt");
        }
    }
}