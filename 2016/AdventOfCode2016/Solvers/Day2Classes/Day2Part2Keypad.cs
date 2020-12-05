namespace AdventOfCode2016.Solvers.Day2Classes
{
    internal class Day2Part2Keypad : Day2KeyPad
    {
        private int _currentX = 0;
        private int _currentY = 2;

        public char CurrentKey => keys[_currentY][_currentX];

        private string[] keys = new[]
        {
            "..1..",
            ".234.",
            "56789",
            ".ABC.",
            "..D.."
        };

        public void MoveUp()
        {
            if (_currentY > 0 && keys[_currentY - 1][_currentX] != '.')
                _currentY -= 1;
        }

        public void MoveDown()
        {
            if (_currentY < 4 && keys[_currentY + 1][_currentX] != '.')
                _currentY += 1;
        }

        public void MoveLeft()
        {
            if (_currentX > 0 && keys[_currentY][_currentX - 1] != '.')
                _currentX -= 1;
        }

        public void MoveRight()
        {
            if (_currentX < 4 && keys[_currentY][_currentX + 1] != '.')
                _currentX += 1;
        }
    }
}