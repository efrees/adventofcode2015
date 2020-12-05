using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2016.Searchers;

namespace AdventOfCode2016.Solvers.Day24Classes
{
    internal class Day24Graph
    {
        private int[,] _distances;

        private Day24Graph() { }

        public static Day24Graph ParseFromInput(string inputMazeText)
        {
            var inputGrid = inputMazeText.SplitIntoLines().ToArray();
            var graph = new Day24Graph();

            AddNodesToGraphFromInput(graph, inputGrid);

            graph._distances = InitializeDistancesBetweenNodes(graph, inputGrid);

            return graph;
        }

        private static void AddNodesToGraphFromInput(Day24Graph graph, string[] inputGrid)
        {
            for (int i = 0; i < inputGrid.Length; i++)
            {
                for (int j = 0; j < inputGrid[i].Length; j++)
                {
                    if (char.IsDigit(inputGrid[i][j]))
                    {
                        graph.Nodes.Add(new Day24Node(inputGrid[i][j] - '0', i, j));
                    }
                }
            }
        }

        private static int[,] InitializeDistancesBetweenNodes(Day24Graph graph, string[] inputGrid)
        {
            var distances = new int[graph.Nodes.Count, graph.Nodes.Count];

            foreach (var node in graph.Nodes)
            {
                foreach (var otherNode in graph.Nodes)
                {
                    distances[node.Identifier, otherNode.Identifier]
                        = distances[otherNode.Identifier, node.Identifier]
                            = FindShortestDistanceBetweenNodes(inputGrid, node, otherNode);
                }
            }
            return distances;
        }

        private static int FindShortestDistanceBetweenNodes(string[] inputGrid, Day24Node node, Day24Node otherNode)
        {
            var startNode = new Day24InputGridNode(inputGrid, node.GridCol, node.GridRow);
            var searcher = new BreadthFirstSearcher<CartesianNode>(startNode);

            var targetNode = new CartesianNode(otherNode.GridCol, otherNode.GridRow);
            var path = searcher.GetShortestPathToNode(targetNode);
            return path.Count - 1;
        }

        public IList<Day24Node> Nodes { get; } = new List<Day24Node>();

        public int DistanceBetween(int node1, int node2)
        {
            return _distances[node1, node2];
        }

        public int GetCostOfPath(IList<Day24Node> orderOfNodes)
        {
            var sum = 0;
            if (orderOfNodes.Count < 2)
                return sum;

            for (int i = 0; i < orderOfNodes.Count - 1; i++)
            {
                var currentNode = orderOfNodes[i];
                var nextNode = orderOfNodes[i + 1];
                sum += DistanceBetween(currentNode.Identifier, nextNode.Identifier);
            }
            return sum;
        }

        public IList<Day24Node> GetBestOrderToTourAllNodes()
        {
            var unvisitedNodes = Nodes.ToList();

            var startNode = unvisitedNodes.First(n => n.Identifier == 0);

            Func<IList<Day24Node>, int> getCostForTour = pathOrder =>
            {
                var distanceToFinishTour = DistanceBetween(pathOrder.Last().Identifier, startNode.Identifier);
                return GetCostOfPath(pathOrder) + distanceToFinishTour;
            };

            var order = GetBestOrderToVisitAllNodesFromStartNode(Nodes, startNode, getCostForTour);
            order.Add(startNode);
            return order;
        }
        
        public IList<Day24Node> GetBestOrderToVisitAllNodesFromZero()
        {
            var unvisitedNodes = Nodes.ToList();

            var startNode = unvisitedNodes.First(n => n.Identifier == 0);

            return GetBestOrderToVisitAllNodesFromStartNode(Nodes, startNode, GetCostOfPath);
        }

        private IList<Day24Node> GetBestOrderToVisitAllNodesFromStartNode(IList<Day24Node> nodes, Day24Node startNode, Func<IList<Day24Node>, int> pathCostFunction)
        {
            if (nodes.Count == 1)
            {
                return nodes.ToList();
            }

            var remainingNodes = nodes.Where(n => n != startNode).ToList();

            var minCost = int.MaxValue;
            IList<Day24Node> orderForMinCost = new List<Day24Node>();

            foreach (var possibleNextNode in remainingNodes)
            {
                var orderOfRemainingNodes = GetBestOrderToVisitAllNodesFromStartNode(remainingNodes, possibleNextNode, pathCostFunction);
                orderOfRemainingNodes.Insert(0, startNode);

                var costForOrder = pathCostFunction(orderOfRemainingNodes);

                if (costForOrder < minCost)
                {
                    minCost = costForOrder;
                    orderForMinCost = orderOfRemainingNodes;
                }
            }

            return orderForMinCost;
        }
    }
}
