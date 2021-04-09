using System;
using System.Collections.Generic;
using System.Text;
using WordLadderPuzzle.Types;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.BL
{
    public static class BFSStrategy
    {
        public static (bool found, List<string>searchGraph) GraphSearchBFS(string start, string end,IDictionary<string, List<string>> graph)
        {

            HashSet<string> visitedNode = new HashSet<string>();

            Dictionary<string, string> parentDict = new Dictionary<string, string>();

            Queue<string> toExplore = new Queue<string>();

            toExplore.Enqueue(start);

            //start == end => we found a simple path
            if (start.Equals(end)) return (true, new List<string> { start });

            bool done = false;

            //BFS which also ends when end is found
            while (toExplore.Count > 0 && !done)
            {
                string currentNode = toExplore.Dequeue();

                List<string> nextNodes = graph[currentNode];

                nextNodes = nextNodes.FindAll(s => !visitedNode.Contains(s));

                foreach (string n in nextNodes)
                {
                    if (end.Equals(n))
                    {
                        //End node found, so we can exit early
                        done = true;

                        //Create the path, backwards, and return it.
                        List<string> path = new List<string> { end };
                        string parentNode = currentNode;

                        while (!parentNode.Equals(start))
                        {
                            //Prepend every element until we get back to the start
                            path.Insert(0, parentNode);
                            parentNode = parentDict[parentNode];
                        }

                        path.Insert(0, start);

                        return (true, path);
                    }

                    visitedNode.Add(n);
                    toExplore.Enqueue(n);
                    parentDict[n] = currentNode;
                }
            }

            //All nodes explored, and end was never reached. Hence we return false.
            return (false, new List<string>());
        }

    }
}
