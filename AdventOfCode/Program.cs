using AdventOfCode.Solutions;
using System;
using System.IO;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileInfo = InputFiles.GetInputFileInfo("Day19");

            if (File.Exists(fileInfo.FullName))
            {
                using (var fileStream = fileInfo.OpenRead())
                {
                    using (var fileReader = new StreamReader(fileStream))
                    {
                        Day19.ProcessInput(fileReader);
                    }
                }
            }
            else
            {
                Console.WriteLine("File Not Found");
            }
            Console.ReadKey();
        }
    }
}
