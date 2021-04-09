using System;
using System.Collections.Generic;
using System.Text;

namespace WordLadderPuzzle.Types.Interfaces
{
    public interface ISearchStrategy
    {
        List<string> GetShortestWordLadder(string start, string end, ICollection<string> wordCollection);
    }
}
