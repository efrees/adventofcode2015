using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Solvers
{
    internal abstract class Day19Solver
    {
        public static Day19Solver CreateForPart1()
        {
            return new Day19Part1Strategy(3005290);
        }

        public static Day19Solver CreateForPart2()
        {
            return new Day19Part2Strategy(3005290);
        }

        public abstract int GetSolution();
    }

    internal class Day19Part1Strategy : Day19Solver
    {
        private readonly int _numberOfElves;

        public Day19Part1Strategy(int numberOfElves)
        {
            _numberOfElves = numberOfElves;
        }

        public override int GetSolution()
        {
            var circle = new Queue<int>(Enumerable.Range(1, _numberOfElves));
            var remainingCount = _numberOfElves;
            while (remainingCount > 1)
            {
                circle.Enqueue(circle.Dequeue());
                circle.Dequeue();
                remainingCount--;
            }
            return circle.First();
        }
    }

    internal class Day19Part2Strategy : Day19Solver
    {
        private readonly int _numberOfElves;

        public Day19Part2Strategy(int numberOfElves)
        {
            _numberOfElves = numberOfElves;
        }

        public override int GetSolution()
        {
            var circle = new LinkedList<int>(Enumerable.Range(1, _numberOfElves));
            var remainingCount = _numberOfElves;
            var currentElfNode = circle.First;
            var numberToSkip = (remainingCount - 2) / 2;
            var oppositeSideNode = SkipForwardByCount(currentElfNode, numberToSkip + 1);
            while (remainingCount > 1)
            {
                var nodeToRemove = oppositeSideNode;
                oppositeSideNode = GetNextOpposite(oppositeSideNode, remainingCount);
                circle.Remove(nodeToRemove);
                remainingCount--;
            }
            return circle.First();
        }

        private LinkedListNode<int> GetNextOpposite(LinkedListNode<int> oppositeSideNode, int remainingCount)
        {
            if (remainingCount % 2 == 1)
            {
                oppositeSideNode = MoveNextCircularly(oppositeSideNode);
            }
            return MoveNextCircularly(oppositeSideNode);
        }

        private LinkedListNode<int> SkipForwardByCount(LinkedListNode<int> currentElfNode, int numberToSkip)
        {
            while (numberToSkip > 0)
            {
                currentElfNode = MoveNextCircularly(currentElfNode);
                numberToSkip--;
            }
            return currentElfNode;
        }

        private LinkedListNode<int> MoveNextCircularly(LinkedListNode<int> currentNode)
        {
            return currentNode.Next ?? currentNode.List.First;
        }
    }
}