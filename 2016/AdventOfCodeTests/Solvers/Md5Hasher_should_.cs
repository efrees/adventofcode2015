using AdventOfCode2016.Solvers;
using NUnit.Framework;

namespace AdventOfCodeTests.Solvers
{
    internal class Md5Hasher_should_
    {
        [TestCase("abc18", ".*cc38887a5.*")]
        [TestCase("abc39", ".*eee.*")]
        [TestCase("abc92", ".*999.*")]
        [TestCase("abc200", ".*99999.*")]
        public void match_known_examples(string inputData, string hashPattern)
        {
            var hashedInput = new Md5Hasher().HashDataAsHexString(inputData);
            StringAssert.IsMatch(hashPattern, hashedInput.ToLower());
        }
    }
}
