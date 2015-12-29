﻿using AdventOfCode.Solutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileInfo = Day11.GetInputFileInfo();

            if (File.Exists(fileInfo.FullName))
            {
                using (var fileStream = fileInfo.OpenRead())
                {
                    using (var fileReader = new StreamReader(fileStream))
                    {
                        Day11.ProcessInput(fileReader);
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
