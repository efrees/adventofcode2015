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

        private string DecorateInputFile(string nodeConfiguration)
        {
            return "root@ebhq-gridcenter# df -h\nFilesystem Size  Used Avail  Use % \n"
                   + nodeConfiguration;
        }
    }
}
