namespace AdventOfCode2016.Searchers
{
    internal interface ISearchHeuristic<T>
    {
        double EstimateSearchCost(T startNode, T targetNode);
    }
}