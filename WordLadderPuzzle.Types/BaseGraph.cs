using System;
using System.Collections.Generic;
using System.Text;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.Types
{
    public abstract class BaseGraph 
    {
        public IDictionary<string, List<string>> WordGraph { get; set; }
        
        public List<string> GetEdges(string start, IDictionary<string, List<string>> graph)
        {
            return graph[start];
        }

        protected abstract void onInitialise(ICollection<string> collection, IDictionary<string, List<string>> graph);

        
    }
}
