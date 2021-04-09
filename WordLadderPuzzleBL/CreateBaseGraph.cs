using System;
using System.Collections.Generic;
using System.Text;
using WordLadderPuzzle.Types;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.BL
{
    public class CreateBaseGraph : IBaseGraph
    {
        #region Class Members
        private IBaseDictionary _baseDictionary;

        public IDictionary<string, List<string>> WordGraph { get; set; }

        #endregion

        #region Constructor
        public CreateBaseGraph(IBaseDictionary baseDictionary)
        {
            _baseDictionary = baseDictionary;
            WordGraph ??= new Dictionary<string, List<string>>();
            WordGraph = InitialiseGraph(_baseDictionary.WordSet);
        }
        #endregion

        #region Public Members
        public List<string> GetEdges(string start, IDictionary<string, List<string>> graph)
        {           

            return graph[start];
        }        
      
        public IDictionary<string, List<string>> InitialiseGraph(ICollection<string> wordCollection)
        {
            IDictionary<string, List<string>> graph= new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> wordToBuckets = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> buckets = new Dictionary<string, List<string>>();

            foreach (string word in wordCollection)
            {
                //For a given word, we can create a bucket for each variation of the word without a given letter
                //ie., dog will be added to the buckets _og, d_g, do_, representing one transformation.
                //We then get each edge by collecting the entries in each bucket.
                //So edges(dog) = bucket[_og] ++ bucket[d_g] ++ bucket[do_]

                List<string> variations = new List<string>();

                for (int i = 0; i < word.Length; i++)
                {
                    string wordVariation = word.Remove(i, 1).Insert(i, "_");
                    variations.Add(wordVariation);

                    //initialise bucket if not already
                    if (!buckets.ContainsKey(wordVariation))
                        buckets[wordVariation] = new List<string> { word };

                    buckets[wordVariation].Add(word);
                }

                wordToBuckets[word] = variations;

                //Initialise graph
                graph[word] = new List<string>();
            }

            //Collect each bucket to create the edges
            foreach (string word in wordCollection)
            {
                List<string> edges = new List<string>();
                List<string> listBucketsForWord = wordToBuckets[word];

                foreach (string wordVariationBucket in listBucketsForWord)
                {
                    edges.AddRange(buckets[wordVariationBucket]);
                }

                //Remove occurences of own word
                edges.RemoveAll(s => s == word);

                graph[word] = edges;
            }

            return graph;
        }

        #endregion
    }
}
