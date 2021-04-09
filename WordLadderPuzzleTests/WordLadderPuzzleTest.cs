using WordLadderPuzzle.BL;
using NUnit.Framework;
using Moq;
using WordLadderPuzzle.Types.Interfaces;
using System.Collections.Generic;

namespace WordLadderPuzzle.BL.Tests
{
    [TestFixture()]
    public class WordLadderPuzzleTest
    {
        #region Private Members
        private Mock<IBaseDictionary> mockBaseDictionary;
        private Mock<IBaseGraph> mockBaseGraph;
        private Mock<IPuzzleSolver> mockPuzzleSolver;
        private SearchStrategy searchStrategy;
        private WordLadderPuzzle wordLadderPuzzle;
        private WordDictionary wordDict;
        #endregion

        #region Tests
        [SetUp]
        public void Setup()
        {
            mockBaseDictionary = new Mock<IBaseDictionary>();
            mockBaseGraph = new Mock<IBaseGraph>();
            mockPuzzleSolver = new Mock<IPuzzleSolver>();
            
        }

        [TestCase("same", "sold", @"D:\BluePrism\words-english\words-english.txt")]
        public void WordLadderTest(string startWord,string endWord,string dictionaryPath)
        {            

            wordDict = new WordDictionary();
            wordDict.LoadDictionary(dictionaryPath);

            mockBaseDictionary.Setup(x => x.WordSet).Returns(wordDict.WordSet);

            var createBaseGraph = new CreateBaseGraph(mockBaseDictionary.Object);
            //arrange
            mockBaseGraph.Setup(x=>x.WordGraph).Returns(createBaseGraph.WordGraph);
            
            searchStrategy = new SearchStrategy(mockBaseGraph.Object);
            //act
            var result = searchStrategy.GetShortestWordLadder(startWord, endWord,wordDict.WordSet);
                                 
            Assert.AreEqual(new List<string> { "same", "some", "sole", "sold" },result);
            
        }

        [Test]
        public void DifferentLengthValidationTest()
        {
            var inputArgs = new string[] { "sae", "sold", @"D:\BluePrism\words-english\words-english.txt", @"D:\BluePrism\words-english\result.txt" };
            wordDict = new WordDictionary();

            wordLadderPuzzle = new WordLadderPuzzle(wordDict, mockPuzzleSolver.Object);
            var result = wordLadderPuzzle.CreateWordLadder(inputArgs);
            Assert.AreEqual(string.Format("Error, {0} and {1} are of different lengths.", inputArgs[0], inputArgs[1]), result);

        }

        [Test]
        public void NotInDictionaryValidationTest()
        {
            var inputArgs = new string[] { "saeeeaaaee", "saeeeaaaee", @"D:\BluePrism\words-english\words-english.txt", @"D:\BluePrism\words-english\result.txt" };
            wordDict = new WordDictionary();

            wordLadderPuzzle = new WordLadderPuzzle(wordDict, mockPuzzleSolver.Object);
            var result = wordLadderPuzzle.CreateWordLadder(inputArgs);
            Assert.AreEqual(string.Format("Error, {0} is not contained in the dictionary.", inputArgs[0]), result);

        }
        #endregion
    }
}
