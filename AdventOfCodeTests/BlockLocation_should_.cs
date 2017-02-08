using System;
using AdventOfCode2016;
using NUnit.Framework;

namespace AdventOfCodeTests
{
    internal class BlockLocation_should_
    {
        private static Random _random = new Random();

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
