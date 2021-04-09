using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordLadderPuzzle.BL.Core
{
    public static class FileOperations
    {
        #region Public Members
        public static string[] GetFileLines(string filePath)
        {
            string[] fileLines = null;
            try
            {
                if (File.Exists(filePath))
                {

                    fileLines = File.ReadAllLines(filePath);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in loading {0} file" + filePath);
                throw ex;
            }
            return fileLines;
        }

        public static bool GenerateResultFile(string filePath,List<string> wordLadder)
        {

            try
            {
                if (IsValidPath(filePath))
                {

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.WriteAllLines(filePath, wordLadder);

                    return true;
                }
                else
                {
                    Console.WriteLine("");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in {0} file generation." + filePath);
                throw ex;
            }
        }

        public static bool IsValidPath(string path, bool allowRelativePaths = false)
        {
            bool isValid;
            try
            {
                string fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                {
                    isValid = Path.IsPathRooted(path);
                }
                else
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in validating {0} file" + path);
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
