using System;
using System.Collections.Generic;
using System.Text;

namespace WordLadderPuzzle.Types.Interfaces
{
    public interface IPuzzleSolver
    {
        List<string> Solve(string start, string end, ICollection<string> wordCollection);
    }
}
