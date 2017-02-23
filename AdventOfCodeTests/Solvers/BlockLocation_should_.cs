using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day1Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class BlockLocation_should_
    {
        [Test]
        public void correctly_handle_movement()
        {
            var movement = new BlockLocation
            {
                X = 2,
                Y = 8
            };

            var location = new BlockLocation();

            var actualResult = location.Move(movement);

            Assert.AreEqual(2, actualResult.X);
            Assert.AreEqual(8, actualResult.Y);
        }
    }
}
