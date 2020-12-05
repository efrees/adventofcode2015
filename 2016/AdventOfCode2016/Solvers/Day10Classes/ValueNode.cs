using System.Linq;

namespace AdventOfCode2016.Solvers.Day10Classes
{
    internal class ValueNode : BotNetworkNode
    {
        public ValueNode(int value)
        {
            Identifier = value;
            ReceivedValues.Add(value);
        }

        public override void PassValueIfKnown()
        {
            if (ReceivedValues.Any() && Targets.Any())
            {
                Targets.First().AcceptValue(ReceivedValues.First());
            }
        }
    }
}