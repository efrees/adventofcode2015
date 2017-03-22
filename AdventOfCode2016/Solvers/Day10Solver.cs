using System;
using AdventOfCode2016.Solvers.Day10Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day10Solver
    {
        public static Day10Solver CreateForPart1()
        {
            var solver = new Day10Solver();
            solver._getAnswerAfterProcessing = () => solver.BotNetwork.FindBotThatDidComparison(61, 17);
            return solver;
        }

        public static Day10Solver CreateForPart2()
        {
            var solver = new Day10Solver();
            solver._getAnswerAfterProcessing = () =>
            {
                var network = solver.BotNetwork;
                return network.GetOutput(0) * network.GetOutput(1) * network.GetOutput(2);
            };
            return solver;
        }

        public BotNetwork BotNetwork { get; private set; }

        private Func<int> _getAnswerAfterProcessing = () => -1;

        private int _firstValueOfInterest = 61;
        private int _secondValueOfInterest = 17;

        public int GetSolution(string fileText)
        {
            BotNetwork = BotNetwork.CreateFromInstructions(fileText.SplitIntoLines());
            BotNetwork.PassValues();
            return _getAnswerAfterProcessing();
        }

        public int GetOutputValue(int outputNumber)
        {
            return BotNetwork.GetOutput(outputNumber);
        }
    }
}