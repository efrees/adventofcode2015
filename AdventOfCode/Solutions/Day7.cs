using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public class Day7
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day7input.txt");
        }

        /// <param name="problemNum">must be zero-based indication of problem</param>
        public static void ProcessInput(StreamReader inputReader, int problemNum = 1)
        {
            var wires = new Helpers.WireTable();

            string inputLine = null;
            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                var regex = new Regex("^(?<gate>.*)->(?<output>.*)$");

                var match = regex.Match(inputLine);
                var gateString = match.Groups["gate"].Value.Trim();
                var outputString = match.Groups["output"].Value.Trim();

                //gateString could be a gate, a signal, or another wire.
                if (gateString.Contains(' '))
                {
                    var gate = ParseGate(gateString, wires);

                    wires.SetWireSignal(outputString.ToLower(), gate);
                }
                else
                {
                    var wire = ParseOperand(gateString, wires);

                    wires.SetWireSignal(outputString.ToLower(), wire);
                }
            }

            //wires.PrintAllValues();

            //Problem 1
            var aSignal = wires.GetWire("a").GetSignal();
            Console.WriteLine("Original signal on a: " + aSignal);

            //Problem 2
            wires.SetWireSignal("b", aSignal.Value);
            wires.RecalculateWires();
            aSignal = wires.GetWire("a").GetSignal();
            Console.WriteLine("New signal on a: " + aSignal);
            //Console.WriteLine("Total ribbon needed: " + totalRibbon);
        }

        private static CircuitGate ParseGate(string gateString, WireTable wires)
        {
            var gate = new CircuitGate();

            if (gateString.Contains("NOT"))
            {
                gate.Operator = CircuitGateType.NOT;

                gate.Wire1 = ParseOperand(gateString.Substring(3).Trim(), wires);
            }
            else
            {
                //Two operands separated by an operator
                var parsed = Regex.Match(gateString, "(?<in1>\\w+)\\s+(?<op>\\w+)\\s+(?<in2>\\w+)");

                if (parsed.Groups.Count <= 3)
                {
                    Console.WriteLine(string.Format("Problem parsing gate: '{0}'.", gateString));
                }

                //Match the operator
                foreach (var oper in Enum.GetNames(typeof(CircuitGateType)))
                {
                    if (oper.ToUpper() == parsed.Groups["op"].Value.Trim())
                    {
                        gate.Operator = (CircuitGateType)Enum.Parse(typeof(CircuitGateType), oper);
                        break;
                    }
                }

                //Parse the operands
                gate.Wire1 = ParseOperand(parsed.Groups["in1"].Value, wires);
                gate.Wire2 = ParseOperand(parsed.Groups["in2"].Value, wires);
            }

            return gate;
        }

        private static CircuitWire ParseOperand(string operand, WireTable wires)
        {
            ushort value;

            if (ushort.TryParse(operand, out value))
            {
                return new CircuitWire("signal") { Value = value };
            }
            else
            {
                return wires.GetWire(operand);
            }
        }
    }
}
