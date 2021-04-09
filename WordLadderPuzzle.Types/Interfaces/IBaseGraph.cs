using System;
using System.Collections.Generic;
using System.Text;

namespace WordLadderPuzzle.Types.Interfaces
{
    public interface IBaseGraph
    {
        IDictionary<string, List<string>> WordGraph { get; set; }
        List<string> GetEdges(string start, IDictionary<string , List<string>> graph);
        IDictionary<string, List<string>> InitialiseGraph(ICollection<string> collection);
    }
}
