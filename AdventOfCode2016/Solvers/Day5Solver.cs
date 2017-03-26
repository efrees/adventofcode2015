using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016.Solvers
{
    internal abstract class Day5Solver
    {
        protected const int PasswordLength = 8;
        private readonly Md5Hasher _md5Hasher = new Md5Hasher();

        public static Day5Solver CreateForPart1()
        {
            return new Day5Part1Solver();
        }

        public static Day5Solver CreateForPart2()
        {
            return new Day5Part2Solver();
        }

        public string GetSolution(string inputLine)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var nextIntegerToTry = 0;
            while (PasswordIsNotComplete())
            {
                var testData = inputLine + nextIntegerToTry;
                var hashedData = _md5Hasher.HashDataAsHexString(testData);
                if (HashIsInteresting(hashedData))
                {
                    AppendToPasswordFromHash(hashedData);
                }
                nextIntegerToTry++;
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());

            return GetCompletedPassword();
        }

        protected abstract bool PasswordIsNotComplete();
        protected abstract void AppendToPasswordFromHash(string hashedData);
        protected abstract string GetCompletedPassword();

        protected virtual bool HashIsInteresting(string hashedData)
        {
            return hashedData.StartsWith("00000");
        }
    }

    internal class Day5Part1Solver : Day5Solver
    {
        protected StringBuilder _stringBuilder = new StringBuilder();

        protected override bool PasswordIsNotComplete()
        {
            return _stringBuilder.Length < Day5Solver.PasswordLength;
        }

        protected override void AppendToPasswordFromHash(string hashedData)
        {
            _stringBuilder.Append(hashedData[5]);
        }

        protected override string GetCompletedPassword()
        {
            return _stringBuilder.ToString();
        }
    }

    internal class Day5Part2Solver : Day5Solver
    {
        private readonly char[] _passwordScratch = Enumerable.Repeat('*', Day5Solver.PasswordLength).ToArray();

        protected override bool PasswordIsNotComplete()
        {
            return _passwordScratch.Any(c => c == '*');
        }

        private static int GetPositionFromHash(string hashedData)
        {
            return (int)(hashedData[5] - '0');
        }

        protected override void AppendToPasswordFromHash(string hashedData)
        {
            var positionInPassword = GetPositionFromHash(hashedData);
            if (positionInPassword < _passwordScratch.Length
                && _passwordScratch[positionInPassword] == '*')
            {
                _passwordScratch[positionInPassword] = GetNextCharacterFromHash(hashedData);
            }
        }

        private char GetNextCharacterFromHash(string hashedData)
        {
            return hashedData[6];
        }

        protected override string GetCompletedPassword()
        {
            return new string(_passwordScratch);
        }
    }
}