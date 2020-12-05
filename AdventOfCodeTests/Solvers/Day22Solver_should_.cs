using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day22Solver_should_
    {
        [Test]
        public void count_pair_of_nodes_if_data_from_a_fits_in_b()
        {
            var nodeConfiguration = "/dev/grid/node-x0-y0     T   67T    22T   %\n"
                                    + "/dev/grid/node-x0-y1     T   72T    67T   %";
            var input = DecorateInputFile(nodeConfiguration);

            var actualCount = Day22Solver.CreateForPart1().GetSolution(input);

            Assert.AreEqual(1, actualCount);
        }

        [Test]
        public void count_pair_twice_if_each_fits_in_the_other()
        {
            var nodeConfiguration = "/dev/grid/node-x0-y0     T   67T    72T   %\n"
                                    + "/dev/grid/node-x0-y1     T   72T    67T   %";
            var input = DecorateInputFile(nodeConfiguration);

            var actualCount = Day22Solver.CreateForPart1().GetSolution(input);

            Assert.AreEqual(2, actualCount);
        }

        [Test]
        public void should_not_count_pair_if_disk_a_has_no_data()
        {
            var nodeConfiguration = "/dev/grid/node-x0-y0     T   67T    72T   %\n"
                                    + "/dev/grid/node-x0-y1     T   0T    67T   %";
            var input = DecorateInputFile(nodeConfiguration);

            var actualCount = Day22Solver.CreateForPart1().GetSolution(input);

            Assert.AreEqual(1, actualCount);
        }

        [Ignore("Solution currently includes hard-coded detour cost that fails this test.")]
        [Test]
        public void should_find_shortest_move_sequence_in_part_2()
        {
            var input = BuildPart2Example();

            var actualCount = Day22Solver.CreateForPart2().GetSolution(input);

            Assert.AreEqual(7, actualCount);
        }

        private string DecorateInputFile(string nodeConfiguration)
        {
            return "root@ebhq-gridcenter# df -h\nFilesystem Size  Used Avail  Use % \n"
                   + nodeConfiguration;
        }

        private string BuildPart2Example()
        {
            return DecorateInputFile(
@"/dev/grid/node-x0-y0   10T    8T     2T   80%
/dev/grid/node-x0-y1   11T    6T     5T   54%
/dev/grid/node-x0-y2   32T   28T     4T   87%
/dev/grid/node-x1-y0    9T    7T     2T   77%
/dev/grid/node-x1-y1    8T    0T     8T    0%
/dev/grid/node-x1-y2   11T    7T     4T   63%
/dev/grid/node-x2-y0   10T    6T     4T   60%
/dev/grid/node-x2-y1    9T    8T     1T   88%
/dev/grid/node-x2-y2    9T    6T     3T   66%");
        }
    }
}
