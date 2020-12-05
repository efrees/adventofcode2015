using System;
using System.Collections.Generic;
using AdventOfCode2016.Solvers.Day12Classes;

namespace AdventOfCode2016.Solvers
{
    public class Day25Solver
    {
        // Visual analysis/simplification of the input program:
        // d = input + 11*231
        // repeat forever
        //   a=d
        //   repeat
        //     b=a%2
        //     a=a/2
        //     output b
        //   until a == 0

        public static Day25Solver Create()
        {
            return new Day25Solver();
        }

        public int GetSolution(string fileText)
        {
            var startingAValue = 1;
            while (NotAlternating(startingAValue + 11 * 231))
            {
                startingAValue++;
            }
            return startingAValue;
        }

        private bool NotAlternating(int number)
        {
            var firstOutput = number % 2;
            var nextOutput = firstOutput;
            while (number > 0)
            {
                if (nextOutput != number % 2)
                {
                    return true;
                }

                number /= 2;
                nextOutput = (nextOutput + 1) % 2;
            }
            return nextOutput != firstOutput;
        }
    }
}
