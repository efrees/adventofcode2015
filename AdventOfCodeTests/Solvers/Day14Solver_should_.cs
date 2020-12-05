using System;
using System.Linq;
using AdventOfCode2016.Solvers;
using AdventOfCode2016.Solvers.Day14Classes;
using Moq;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Day14Solver_should_
    {
        [Test]
        public void create_key_qualifier_with_salted_hash_sequence()
        {
            //Logic in tests... should probably factor out the unhashed sequence generator for testability.
            // Or else independently compute the hashes for these few values.
            var md5Hasher = new Md5Hasher();
            var expectedSequence = new[] { "zpqevtbw0", "zpqevtbw1", "zpqevtbw2" }
                .Select(md5Hasher.HashDataAsHexString)
                .Select(s => s.ToLower());

            var solver = Day14Solver.CreateForPart1();
            var actualSequence = solver.KeyQualifier.SequenceGenerator.Take(3).ToList();

            CollectionAssert.AreEqual(expectedSequence, actualSequence);
        }

        [Test]
        public void return_index_of_nth_qualified_key()
        {
            var targetOrdinal = 3;
            var expectedIndex = Any.IntBetween(1, 1000);

            var qualifiedSequence = new[]
            {
                Tuple.Create(Any.Integer(), Any.AlphaNumericString()),
                Tuple.Create(Any.Integer(), Any.AlphaNumericString()),
                Tuple.Create(expectedIndex, Any.AlphaNumericString()),
                Tuple.Create(Any.Integer(), Any.AlphaNumericString())
            };

            var mockKeyQualifier = new Mock<BufferedKeyQualifier>(null);
            mockKeyQualifier.Setup(q => q.GetQualifiedKeyStream())
                .Returns(qualifiedSequence);

            var solver = new Day14Solver(mockKeyQualifier.Object, targetOrdinal);
            var actualIndex = solver.GetSolution();

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void solve_example_case_correctly()
        {
            var solver = Day14Solver.CreateForPart1Example();
            var actualSolution = solver.GetSolution();
            Assert.AreEqual(22728, actualSolution);
        }

        [Test]
        public void stretch_keys_correctly_for_part_2()
        {
            var salt = "abc";
            var sequence = Day14Solver.GetSequence(salt, new Md5Hasher());
            sequence = Day14Solver.GetKeyStretchedSequence(sequence, new Md5Hasher());
            var qualifier = new BufferedKeyQualifier(sequence);

            var firstKeys = qualifier.GetQualifiedKeyStream().Take(3).ToList();

            Assert.AreEqual(10, firstKeys[0].Item1);
            Assert.AreEqual(25, firstKeys[1].Item1);
        }
    }
}
