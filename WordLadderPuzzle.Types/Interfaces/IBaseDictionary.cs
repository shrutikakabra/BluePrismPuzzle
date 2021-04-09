using System;
using System.Collections.Generic;
using System.Text;

namespace WordLadderPuzzle.Types.Interfaces
{
 
    public interface IBaseDictionary
    {
        bool IsInitialised();
        bool IsInDictionary(string word);

        ICollection<string> WordSet { get; set; }

    }
}
