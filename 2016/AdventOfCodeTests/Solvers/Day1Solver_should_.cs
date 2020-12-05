using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day1Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day1Solver_should_
    {
        [SetUp]
        public void ResetStaticState()
        {
            Day1Solver.ResetSolver();
        }

        [Test]
        public void get_correct_final_location_for_single_instruction_with_right_turn()
        {
            var movementDistance = Any.MovementDistance();
            var instruction = $"R{movementDistance}";

            var actualResult = Day1Solver.GetRelativeLocationFromDirections(new[] { instruction });

            Assert.AreEqual(movementDistance, actualResult.X);
            Assert.AreEqual(0, actualResult.Y);
        }

        [Test]
        public void get_correct_final_location_for_single_instruction_with_left_turn()
        {
            var movementDistance = Any.MovementDistance();
            var instruction = $"L{movementDistance}";

            var actualResult = Day1Solver.GetRelativeLocationFromDirections(new[] { instruction });

            Assert.AreEqual(-movementDistance, actualResult.X);
        }

        [Test]
        public void get_correct_final_location_for_given_example()
        {
            var instructions = new[] { "R2", "R2", "R2" };

            var actualResult = Day1Solver.GetRelativeLocationFromDirections(instructions);

            Assert.AreEqual(-2, actualResult.Y);
            Assert.AreEqual(0, actualResult.X);
        }

        [Test]
        public void log_visited_location_when_moving_if_part_2_is_enabled()
        {
            Day1Solver.EnablePart2Rule = true;

            var firstStop = new BlockLocation(2, 0);
            var secondStop = new BlockLocation(2, 4);

            var instructions = new[] { "R2", "L4" };

            Day1Solver.GetRelativeLocationFromDirections(instructions);

            Assert.IsTrue(Day1Solver.HaveVisited(firstStop), "First Stop was logged");
            Assert.IsTrue(Day1Solver.HaveVisited(firstStop), "Second Stop was logged");
        }

        [Test]
        public void log_intermediate_block_locations_when_moving_if_part_2_is_enabled()
        {
            Day1Solver.EnablePart2Rule = true;

            var instructions = new[] { "R2" };

            Day1Solver.GetRelativeLocationFromDirections(instructions);

            Assert.IsTrue(Day1Solver.HaveVisited(new BlockLocation()), "Initial Position logged");
            Assert.IsTrue(Day1Solver.HaveVisited(new BlockLocation(1, 0)), "Intermediate Position logged");
        }

        [Test]
        public void return_first_location_visited_twice_if_movements_intersect_and_part_2_is_enabled()
        {
            Day1Solver.EnablePart2Rule = true;

            var instructions = new[] { "R8", "R4", "R4", "R8" };

            var actualResult = Day1Solver.GetRelativeLocationFromDirections(instructions);

            Assert.AreEqual(4, actualResult.X, "X");
            Assert.AreEqual(0, actualResult.Y, "Y");
        }

        [Test]
        public void return_first_location_visited_twice_if_looping_to_start()
        {
            Day1Solver.EnablePart2Rule = true;

            var instructions = new[] { "L4", "L4", "L4", "L4", "R11" };

            var actualResult = Day1Solver.GetRelativeLocationFromDirections(instructions);

            Assert.AreEqual(0, actualResult.X, "X");
            Assert.AreEqual(0, actualResult.Y, "Y");
        }
    }
}
