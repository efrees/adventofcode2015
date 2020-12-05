using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers
{
    internal class WireTable
    {
        private Dictionary<string, CircuitWire> wires = new Dictionary<string, CircuitWire>();

        internal void SetWireSignal(string wire, ushort value)
        {
            var wireObj = GetWire(wire);

            wireObj.Value = value;
        }

        internal void SetWireSignal(string wire, CircuitWire wire2Obj)
        {
            var wireObj = GetWire(wire);

            wireObj.OtherWire = wire2Obj;
        }

        internal void SetWireSignal(string wire, CircuitGate gate)
        {
            var wireObj = GetWire(wire);

            wireObj.Gate = gate;
        }

        internal CircuitWire GetWire(string wire)
        {
            if (!wires.ContainsKey(wire))
            {
                wires[wire] = new CircuitWire(wire);
            }

            return wires[wire];
        }

        internal void PrintAllValues()
        {
            foreach (var key in wires.Keys.OrderBy(x => x))
            {
                Console.WriteLine(string.Format("{0}: {1}", key, wires[key].GetSignal()));
            }
        }

        internal void RecalculateWires()
        {
            foreach(var wire in wires.Values)
            {
                wire.Reset();
            }
        }
    }

    internal enum CircuitGateType
    {
        Uninitialized,
        NOT,
        AND,
        OR,
        LSHIFT,
        RSHIFT,
    }

    internal class CircuitGate
    {
        internal CircuitGateType Operator { get; set; }
        internal CircuitWire Wire1 { get; set; }
        internal CircuitWire Wire2 { get; set; }

        internal ushort? GetOutput()
        {
            switch (Operator)
            {
                case CircuitGateType.NOT:
                    return (ushort?)~Wire1.GetSignal();
                case CircuitGateType.AND:
                    return (ushort?)(Wire1.GetSignal() & Wire2.GetSignal());
                case CircuitGateType.OR:
                    return (ushort?)(Wire1.GetSignal() | Wire2.GetSignal());
                case CircuitGateType.LSHIFT:
                    return (ushort?)(Wire1.GetSignal() << Wire2.GetSignal());
                case CircuitGateType.RSHIFT:
                    return (ushort?)(Wire1.GetSignal() >> Wire2.GetSignal());
                default:
                    Console.WriteLine("Invalid operator.");
                    return null;
            }
        }
    }

    internal class CircuitWire
    {
        internal string Name { get; set; }
        internal ushort? Value { private get; set; }
        internal CircuitWire OtherWire { private get; set; }
        internal CircuitGate Gate { private get; set; }

        private ushort? computedValue = null;

        public CircuitWire(string name)
        {
            Name = name;
        }

        internal void Reset()
        {
            computedValue = null;
        }
        
        internal ushort? GetSignal()
        {
            if (computedValue.HasValue)
            {
                return computedValue;
            }

            if (Value.HasValue)
                computedValue = Value;
            else if (OtherWire != null)
                computedValue = OtherWire.GetSignal();
            else if (Gate != null)
                computedValue = Gate.GetOutput();
            else {
                Console.WriteLine(string.Format("{0} does not have a signal.", Name));
                computedValue = null;
            }

            return computedValue;
        }
    }
}
