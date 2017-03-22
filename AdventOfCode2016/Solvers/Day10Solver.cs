using AdventOfCode2016.Solvers.Day10Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day10Solver
    {
        public static Day10Solver Create()
        {
            return new Day10Solver();
        }

        public BotNetwork BotNetwork { get; private set; }

        private int _firstValueOfInterest = 61;
        private int _secondValueOfInterest = 17;

        public int GetSolution(string fileText)
        {
            BotNetwork = BotNetwork.CreateFromInstructions(fileText.SplitIntoLines());
            BotNetwork.PassValues();
            return BotNetwork.FindBotThatDidComparison(_firstValueOfInterest, _secondValueOfInterest);
        }

        public int GetOutputValue(int outputNumber)
        {
            return BotNetwork.GetOutput(outputNumber);
        }
    }
}