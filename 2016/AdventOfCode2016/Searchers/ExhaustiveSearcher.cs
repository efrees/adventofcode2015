using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Searchers
{
    internal class ExhaustiveSearcher<T> where T : SearchNode<T>
    {
        public T StartNode { get; }

        public ExhaustiveSearcher(T startNode)
        {
            StartNode = startNode;
        }

        public ExhaustiveSearcher(T startNode, int depthLimit)
        {
            StartNode = startNode;
        }

        public virtual IEnumerable<IList<T>> FindAllPaths(T targetNode)
        {
            return FindAllPathsDepthFirst(StartNode, targetNode)
                .Select(pathEnd => pathEnd.GetPathToHere());
        }

        private IEnumerable<T> FindAllPathsDepthFirst(T startNode, T targetNode)
        {
            var children = startNode.GetChildren();

            foreach (var child in children)
            {
                child.StoreParentNode(startNode);
                if (child.Equals(targetNode))
                {
                    yield return child;
                }
                else
                {
                    var pathsFromChild = FindAllPathsDepthFirst(child, targetNode);
                    foreach (var pathEnds in pathsFromChild)
                    {
                        yield return pathEnds;
                    }
                }
            }
        }
    }
}