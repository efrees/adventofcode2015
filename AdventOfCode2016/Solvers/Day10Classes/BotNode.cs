using System.Linq;

namespace AdventOfCode2016.Solvers.Day10Classes
{
    internal class BotNode : BotNetworkNode
    {
        public BotNode(int number)
        {
            Identifier = number;
        }

        public override void PassValueIfKnown()
        {
            if (ReceivedValues.Count() == 2 && Targets.Count() == 2)
            {
                Targets[0].AcceptValue(ReceivedValues.Min());
                Targets[1].AcceptValue(ReceivedValues.Max());
            }
        }
    }
}