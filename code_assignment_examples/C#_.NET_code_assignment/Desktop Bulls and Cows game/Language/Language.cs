using System;

namespace Language
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Language
    {
        public static bool LetterBelongToLanguage(char i_Character)
        {
            bool isBelong = false;
            if (Enum.IsDefined(typeof(eBullsAndCowsCharacter), i_Character.ToString()))
            {
                isBelong = true;
            }
            else
            {
                throw new UndefinedLetterException(i_Character);
            }

            return isBelong;
        }

        public static bool WordBelongToLanguage(string i_InputAsString)
        {
            bool b = true;
            foreach (char letter in i_InputAsString)
            {
                b = LetterBelongToLanguage(letter) == false ? false : b;
            }

            return b;
        }

        public static bool WordBelongToLanguage(string[] i_InputAsStringArray)
        {
            bool b = true;
            foreach (string str in i_InputAsStringArray)
            {
                b = Enum.IsDefined(typeof(eBullsAndCowsCharacter), str);
            }

            return b;
        }

        public static bool NoDuplicates(string i_InputAsString)
        {
            bool notIncludeDuplicates = i_InputAsString.Distinct().Count() == i_InputAsString.Length;
            if (!notIncludeDuplicates)
            {
                throw new DuplicateLettersException();
            }

            return notIncludeDuplicates;
        }

        public static bool NoDuplicates(string[] i_InputAStringArray, int i_CellNumber, string i_stringToBeChecked)
        {
            bool notIncludeDuplicates = false;
            foreach (string str in i_InputAStringArray)
            {
                if(str == (i_stringToBeChecked) && i_CellNumber != Array.IndexOf(i_InputAStringArray, str))
                {
                    notIncludeDuplicates = true;
                }
            }

            return notIncludeDuplicates;
        }

        public static List<string> RandomWordWithoutRepeatLetters(int i_Length)
        {
            List<string> words = new List<string>();
            string word = string.Empty;
            List<string> allWords = Enum.GetNames(typeof(eBullsAndCowsCharacter)).ToList();
            Random random = new Random();

            for (int i = 0; i < i_Length; i++)
            {
                word = allWords[random.Next(0, allWords.Count)];
                words.Add(word.ToLower());
                allWords.Remove(word);
            }

            return words;
        }
    }
}
