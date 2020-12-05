using System.Text;

namespace AdventOfCode2016.Solvers.Day16Classes
{
    public class Day16DataGenerator
    {
        public virtual string ExpandData(string inputData, int targetLength)
        {
            while (inputData.Length < targetLength)
                inputData = ExpandDataOneIteration(inputData);
            return inputData;
        }

        private string ExpandDataOneIteration(string inputData)
        {
            var reversedData = ReverseAndFlip(inputData);
            return $"{inputData}0{reversedData}";
        }

        private string ReverseAndFlip(string inputData)
        {
            var reversedData = new StringBuilder(inputData.Length);
            for (int i = inputData.Length - 1; i >= 0; i--)
            {
                reversedData.Append(FlipBit(inputData[i]));
            }
            return reversedData.ToString();
        }

        private char FlipBit(char c)
        {
            return c == '1' ? '0' : '1';
        }
    }
}
