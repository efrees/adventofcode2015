using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class RingDefinition_should_
    {
        [Test]
        public void parse_input_into_ring_configurations()
        {
            var inputLine = "Disc #2 has 13 positions; at time=0, it is at position 9.";
            var ringConfiguration = RingDefinition.Create(inputLine);
            Assert.AreEqual(2, ringConfiguration.DiscNumber);
            Assert.AreEqual(13, ringConfiguration.Positions);
            Assert.AreEqual(9, ringConfiguration.StartingOffset);
        }

        [Test]
        public void compute_position_at_any_time()
        {
            var ringConfiguration = new RingDefinition
            {
                StartingOffset = 4,
                Positions = 5
            };

            var time = 1;
            var expectedPosition = 0;
            var actualPosition = ringConfiguration.GetPositionAtTime(time);

            Assert.AreEqual(expectedPosition, actualPosition);
        }
    }
}
