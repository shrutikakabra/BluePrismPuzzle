using System;
using System.Collections.Generic;
using System.Text;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.BL
{
    public class PuzzleSolver : IPuzzleSolver
    {        
        private ISearchStrategy _searchStrategy;
        public PuzzleSolver(ISearchStrategy searchStrategy)
        {
           
            _searchStrategy = searchStrategy;
        }
        public List<string> Solve(string start, string end,ICollection<string> wordCollection)
        {

            List<string> result = _searchStrategy.GetShortestWordLadder(start, end, wordCollection);            

            return result;
        }
                
    }
}
