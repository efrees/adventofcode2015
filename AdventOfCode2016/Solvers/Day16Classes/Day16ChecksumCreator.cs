using System.Text;

namespace AdventOfCode2016.Solvers.Day16Classes
{
    internal class Day16ChecksumCreator
    {
        public virtual string GetChecksum(string inputData)
        {
            while (inputData.Length % 2 == 0)
            {
                inputData = ReducePairsForChecksum(inputData);
            }

            return inputData;
        }

        private static string ReducePairsForChecksum(string inputData)
        {
            var checksum = new StringBuilder();
            for (var i = 0; i < inputData.Length; i += 2)
            {
                var isMatchingPair = inputData[i] == inputData[i + 1];
                checksum.Append(isMatchingPair ? '1' : '0');
            }
            return checksum.ToString();
        }
    }
}
