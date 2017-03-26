using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Searchers
{
    internal class BreadthFirstSearcher<T> where T : SearchNode<T>
    {
        public T StartNode { get; private set; }
        public Queue<T> SearchQueue { get; } = new Queue<T>();
        public ISet<T> VisitedNodes = new HashSet<T>();

        public BreadthFirstSearcher(T startNode)
        {
            StartNode = startNode;
        }

        public virtual IList<T> GetShortestPathToNode(T targetNode)
        {
            SearchQueue.Enqueue(StartNode);

            while (SearchQueue.Any())
            {
                var currentNode = SearchQueue.Dequeue();

                if (VisitedNodes.Contains(currentNode))
                    continue;

                VisitedNodes.Add(currentNode);

                if (currentNode.Equals(targetNode))
                {
                    return currentNode.GetPathToHere();
                }

                var children = currentNode.GetChildren();

                foreach (var child in children)
                {
                    child.StoreParentNode(currentNode);
                    SearchQueue.Enqueue(child);
                }
            }

            return new List<T>();
        }
    }
}