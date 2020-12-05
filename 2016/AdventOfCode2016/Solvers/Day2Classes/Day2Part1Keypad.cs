namespace AdventOfCode2016.Solvers.Day2Classes
{
    internal class Day2Part1Keypad : Day2KeyPad
    {
        private int _currentKeyValue = 5;
        public char CurrentKey => _currentKeyValue.ToString()[0];

        public void MoveUp()
        {
            if (_currentKeyValue > 3)
                _currentKeyValue -= 3;
        }

        public void MoveDown()
        {
            if (_currentKeyValue < 7)
                _currentKeyValue += 3;
        }

        public void MoveLeft()
        {
            if (_currentKeyValue % 3 != 1)
                _currentKeyValue -= 1;
        }

        public void MoveRight()
        {
            if (_currentKeyValue % 3 != 0)
                _currentKeyValue += 1;
        }
    }
}