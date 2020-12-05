namespace AdventOfCode2016.Solvers.Day2Classes
{
    internal interface Day2KeyPad
    {
        char CurrentKey { get; }
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
    }
}