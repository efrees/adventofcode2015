using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Solvers.Day14Classes;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class BufferedKeyQualifier_should_
    {
        [Test]
        public void qualify_key_if_it_has_a_triple_and_a_later_key_has_quintuple_of_same_character()
        {
            var sequence = new List<string> { "bb", "aaa", "aa", "abcdef", "baaaaa" };
            sequence.AddRange(Enumerable.Repeat("x", 1000));
            var qualifier = new BufferedKeyQualifier(sequence);

            var firstKey = qualifier.GetQualifiedKeyStream().First();
            Assert.AreEqual(1, firstKey.Item1, "Index");
            Assert.AreEqual("aaa", firstKey.Item2, "Key");
        }

        [Test]
        public void not_qualify_key_if_it_has_no_triple()
        {
            var sequence = new List<string> { "bb", "aad", "223", "abcdef", "22222aaaaabbbbbccccc" };
            sequence.AddRange(Enumerable.Repeat("x", 1000));
            var qualifier = new BufferedKeyQualifier(sequence);

            var keyStream = qualifier.GetQualifiedKeyStream();
            CollectionAssert.IsEmpty(keyStream);
        }

        [Test]
        public void not_qualify_key_if_triple_matching_quintuple_is_not_first_in_string()
        {
            var sequence = new List<string> { "111bbb", "abcdef", "22222aaaaabbbbbccccc" };
            sequence.AddRange(Enumerable.Repeat("x", 1000));
            var qualifier = new BufferedKeyQualifier(sequence);

            var keyStream = qualifier.GetQualifiedKeyStream();
            CollectionAssert.IsEmpty(keyStream);
        }

        [Test]
        public void not_qualify_key_if_quintuple_not_in_next_thousand()
        {
            var sequence = new List<string> { "33333" };
            sequence.AddRange(Enumerable.Repeat("x", 1000));
            sequence.Add("33333");

            var qualifier = new BufferedKeyQualifier(sequence);

            var keyStream = qualifier.GetQualifiedKeyStream();
            CollectionAssert.IsEmpty(keyStream);
        }

        [Test]
        public void qualify_key_if_quintuple_is_exactly_one_thousand_away()
        {
            var sequence = new List<string> { "33333" };
            sequence.AddRange(Enumerable.Repeat("x", 999));
            sequence.Add("33333");
            sequence.Add("x");

            var qualifier = new BufferedKeyQualifier(sequence);

            var keyStream = qualifier.GetQualifiedKeyStream().ToList();
            CollectionAssert.AreEqual(new[] { Tuple.Create(0, "33333") }, keyStream);
        }
    }
}
