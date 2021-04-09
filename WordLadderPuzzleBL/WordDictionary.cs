using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordLadderPuzzle.BL.Core;
using WordLadderPuzzle.Types;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.BL
{
    public class WordDictionary : BaseDictionary
    {  
        public WordDictionary()
        {
            WordSet = new HashSet<string>();
        }

        public override void LoadDictionary(string filePath)
        {
            try
            {
                string[] lines = FileOperations.GetFileLines(filePath);
                if (lines != null)
                {
                    foreach (string line in lines)
                    {
                        //Perform some formatting on each loaded line - remove any unwanted characters
                        string formatted = line.ToLower();

                        if (!formatted.All(Char.IsLetter))
                        {
                            //Unexpected characters, will handle by skipping this line for simplicity.
                            continue;
                        }                        

                        WordSet.Add(formatted);
                    }                    

                    IsLoaded = true;
                }
                
            }
            catch (System.IO.IOException e)
            {

                Console.WriteLine("Exception reading word list from filepath: " + filePath + ", exception details: " + e.Message);

            }
        }

    }
}
