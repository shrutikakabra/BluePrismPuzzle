using System;
using System.Collections.Generic;
using System.Text;
using WordLadderPuzzle.Types;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.BL
{
    public class SearchStrategy : ISearchStrategy
    {
        private IBaseGraph _createBaseGraph;

        public SearchStrategy(IBaseGraph createBaseGraph)
        {
            _createBaseGraph = createBaseGraph;
        }        

        public List<string> GetShortestWordLadder(string start, string end,ICollection<string> wordCollection)
        {           
            IDictionary<string, List<string>> graph;
            if (_createBaseGraph.WordGraph == null || _createBaseGraph.WordGraph.Count == 0)
            {
                graph = _createBaseGraph.InitialiseGraph(wordCollection);
            }
            else
            {
                graph = _createBaseGraph.WordGraph;
            }
            var result = BFSStrategy.GraphSearchBFS(start, end, graph);

            return result.found ? result.searchGraph : null;
        }
    }
}
