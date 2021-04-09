using System;
using System.Collections.Generic;
using System.Text;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.Types
{
    public abstract class BaseDictionary : IBaseDictionary
    {
        public ICollection<string> WordSet { get; set; }
        public bool IsLoaded { get; set; }        
        public bool IsInDictionary(string word)
        {
            return WordSet.Contains(word);
        }
        public bool IsInitialised()
        {
            return IsLoaded;
        }
        public abstract void LoadDictionary(string filePath);

    }
}
