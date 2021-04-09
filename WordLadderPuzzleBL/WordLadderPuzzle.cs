using System;
using System.IO;
using WordLadderPuzzle.BL.Core;
using WordLadderPuzzle.Types;
using WordLadderPuzzle.Types.Interfaces;

namespace WordLadderPuzzle.BL
{
    public class WordLadderPuzzle : IWordLadderPuzzle
    {
        #region Class Members
        private WordDictionary _wordDictionary;
        private IPuzzleSolver _puzzleSolver;

        private string _startWord = string.Empty;
        private string _endWord = string.Empty;
        private string _dictionaryFilePath = string.Empty;
        private string _resultFilePath = string.Empty;

        #endregion

        #region Public Members

        public WordLadderPuzzle(WordDictionary wordDictionary,IPuzzleSolver puzzleSolver)
        {
            _wordDictionary = wordDictionary;
            _puzzleSolver = puzzleSolver;

        }

        public string CreateWordLadder(string[] inputArgs)
        {
            
            if (ValidateAndInitiliseInputData(inputArgs,out string returnMessage))
            {
                if (!_wordDictionary.IsInitialised())
                {
                    _wordDictionary.LoadDictionary(_dictionaryFilePath);

                }

                if (!_wordDictionary.IsInDictionary(_startWord))
                {
                    return string.Format("Error, {0} is not contained in the dictionary.", _startWord);
                    
                }

                if (!_wordDictionary.IsInDictionary(_endWord))
                {
                    return string.Format("Error, {0} is not contained in the dictionary.", _endWord);
                    
                }

                Console.WriteLine("Solving....");

                var wordLadder = _puzzleSolver.Solve(_startWord, _endWord,_wordDictionary.WordSet);
                if (wordLadder != null)
                {
                    if (FileOperations.GenerateResultFile(_resultFilePath, wordLadder))
                    {
                        return string.Format("The word ladder puzzle is solved. Pleased find result at {0}", _resultFilePath);
                    }
                    else
                    {
                        return string.Format("Error while creating the result file.");
                    }
                }
                else
                {
                    return string.Format("Could not solve the word puzzle.");
                }  
                
            }

            return returnMessage;

        }

        #endregion

        #region Private Members
        private void InitialiseData(string[] inputArgs)
        {      
                _startWord = inputArgs[0].ToLower();
                _endWord = inputArgs[1].ToLower();
                _dictionaryFilePath = inputArgs[2];
                _resultFilePath = inputArgs[3];                
        }

        private bool ValidateAndInitiliseInputData(string[] inputArgs, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (inputArgs != null && inputArgs.Length >= 4)
            {

                if (inputArgs[0].Length != inputArgs[1].Length)
                {
                    errorMessage = string.Format("Error, {0} and {1} are of different lengths.", inputArgs[0], inputArgs[1]);
                    return false;
                }

                if (!File.Exists(inputArgs[2]))
                {
                    errorMessage = string.Format("Dictionary file is not-accessible/ Invalid.");
                    return false;
                }

                if (!FileOperations.IsValidPath(inputArgs[3]))
                {
                    errorMessage = string.Format("Error, {0} is not a valid file path", inputArgs[3]);
                    return false;
                }

                InitialiseData(inputArgs);
            }
            else
            {
                errorMessage = "Error, Insufficient input parameters provided.";
                return false;
            }

            return true;


        }
        #endregion

    }
}
