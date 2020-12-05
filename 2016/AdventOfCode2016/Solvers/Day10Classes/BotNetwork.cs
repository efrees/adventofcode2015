using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Solvers.Day10Classes
{
    internal class BotNetwork
    {
        public static BotNetwork CreateFromInstructions(IEnumerable<string> instructionLines)
        {
            var network = new BotNetwork();
            foreach (var instruction in instructionLines)
            {
                network.AddFromInstruction(instruction);
            }
            return network;
        }

        private void AddFromInstruction(string instruction)
        {
            var valuePattern = "value (?<value>\\d+) goes to (?<targetDescriptor>.+)";
            var botPattern = "bot (?<sourceBot>\\d+) gives low to (?<lowTargetDescriptor>(bot|output) \\d+)" +
                             " and high to (?<highTargetDescriptor>(bot|output) \\d+)";

            var valueInstructionMatch = Regex.Match(instruction, valuePattern);
            var botInstructionMatch = Regex.Match(instruction, botPattern);
            if (valueInstructionMatch.Success)
            {
                var value = Convert.ToInt32(valueInstructionMatch.Groups["value"].Value);
                var valueNode = new ValueNode(value);
                Nodes.Add(valueNode);

                var targetDescriptor = valueInstructionMatch.Groups["targetDescriptor"].Value;
                var targetNode = GetOrCreateNodeMatchingDescriptor(targetDescriptor);

                valueNode.AddTarget(targetNode);
            }
            else if (botInstructionMatch.Success)
            {
                var sourceBotIdentifier = Convert.ToInt32(botInstructionMatch.Groups["sourceBot"].Value);
                BotNetworkNode sourceNode = new BotNode(sourceBotIdentifier);
                if (Nodes.Contains(sourceNode))
                {
                    sourceNode = Nodes.First(n => n.Equals(sourceNode));
                }
                else
                {
                    Nodes.Add(sourceNode);
                }

                var lowTargetDescriptor = botInstructionMatch.Groups["lowTargetDescriptor"].Value;
                var highTargetDescriptor = botInstructionMatch.Groups["highTargetDescriptor"].Value;

                var lowTargetNode = GetOrCreateNodeMatchingDescriptor(lowTargetDescriptor);
                var highTargetNode = GetOrCreateNodeMatchingDescriptor(highTargetDescriptor);

                sourceNode.AddTarget(lowTargetNode);
                sourceNode.AddTarget(highTargetNode);
            }
        }

        private BotNetworkNode GetOrCreateNodeMatchingDescriptor(string targetDescriptor)
        {
            var targetNode = BotNetworkNode.FromDescriptor(targetDescriptor);
            if (Nodes.Contains(targetNode))
            {
                targetNode = Nodes.First(n => n.Equals(targetNode));
            }
            else
            {
                Nodes.Add(targetNode);
            }
            return targetNode;
        }

        public void PassValues()
        {
            Nodes.Where(n => n is ValueNode)
                .ToList()
                .ForEach(n => n.PassValueIfKnown());
        }

        public int FindBotThatDidComparison(int chipValueOne, int chipValueTwo)
        {
            var botNode = Nodes.FirstOrDefault(n => n.ReceivedValues.Contains(chipValueOne)
                                           && n.ReceivedValues.Contains(chipValueTwo));
            return botNode?.Identifier ?? -1;
        }

        public int GetOutput(int outputNumber)
        {
            return Nodes.Where(n => n is OutputNode)
                .First(n => n.Identifier == outputNumber)
                .ReceivedValues.FirstOrDefault();
        }

        public ISet<BotNetworkNode> Nodes { get; set; } = new HashSet<BotNetworkNode>();
    }
}