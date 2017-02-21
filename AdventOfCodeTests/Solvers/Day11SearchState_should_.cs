using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day11SearchState_should_
    {
        [Test]
        public void produce_same_state_identifier_for_structurally_equivalent_states()
        {
            var state1 = new Day11SearchState();
            state1.FirstFloor.Add(new Day11Component("micronium", "generator"));
            state1.ThirdFloor.Add(new Day11Component("plutonium", "microchip"));
            state1.ThirdFloor.Add(new Day11Component("plutonium", "generator"));

            var state2 = new Day11SearchState();
            state2.FirstFloor.Add(new Day11Component("geranium", "generator"));
            state2.ThirdFloor.Add(new Day11Component("silver", "generator"));
            state2.ThirdFloor.Add(new Day11Component("silver", "microchip"));

            Assert.AreEqual(state1.GetStateIdentifier(), state2.GetStateIdentifier());
        }
    }
}
