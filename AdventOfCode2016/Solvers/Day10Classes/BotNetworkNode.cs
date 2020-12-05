using System;
using System.Collections.Generic;

namespace AdventOfCode2016.Solvers.Day10Classes
{
    internal abstract class BotNetworkNode
    {
        public int Identifier { get; protected set; }

        public List<int> ReceivedValues = new List<int>();

        public IList<BotNetworkNode> Targets { get; set; } = new List<BotNetworkNode>();

        public static BotNetworkNode FromDescriptor(string nodeDescriptor)
        {
            if (nodeDescriptor.StartsWith("output"))
            {
                var number = Convert.ToInt32(nodeDescriptor.Substring(7));
                return new OutputNode { Identifier = number };
            }
            else if (nodeDescriptor.StartsWith("bot"))
            {
                var number = Convert.ToInt32(nodeDescriptor.Substring(4));
                return new BotNode(number);
            }

            return null;
        }

        public abstract void PassValueIfKnown();

        public void AddTarget(BotNetworkNode targetNode)
        {
            Targets.Add(targetNode);
        }

        public void AcceptValue(int value)
        {
            ReceivedValues.Add(value);
            PassValueIfKnown();
        }

        public override bool Equals(object other)
        {
            if (other != null && other.GetType() == GetType())
            {
                return ((BotNetworkNode)other).Identifier == Identifier;
            }
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() ^ Identifier;
        }
    }
}