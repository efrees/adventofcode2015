using AdventOfCode2016.Solvers.Day16Classes;

namespace AdventOfCode2016.Solvers
{
    internal class Day16Solver
    {
        private readonly string _inputData;
        private readonly int _desiredDataLength;
        private readonly Day16DataGenerator _dataGenerator;
        private readonly Day16ChecksumCreator _checksumCreator;

        internal Day16Solver(string inputData, int desiredDataLength, Day16DataGenerator dataGenerator, Day16ChecksumCreator checksumCreator)
        {
            _inputData = inputData;
            _desiredDataLength = desiredDataLength;
            _dataGenerator = dataGenerator;
            _checksumCreator = checksumCreator;
        }

        public static Day16Solver CreateForPart1()
        {
            return new Day16Solver("10111100110001111", 272, new Day16DataGenerator(), new Day16ChecksumCreator());
        }

        public string GetSolution()
        {
            var expandedData = _dataGenerator.ExpandData(_inputData, _desiredDataLength);
            return _checksumCreator.GetChecksum(expandedData?.Substring(0, _desiredDataLength));
        }
    }
}