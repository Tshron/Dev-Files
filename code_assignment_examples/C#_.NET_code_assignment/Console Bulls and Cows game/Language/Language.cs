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
            foreach(char letter in i_InputAsString)
            {
                b = LetterBelongToLanguage(letter) == false ? false : b;
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
        
        public static string RandomWordWithoutRepeatLetters(int i_Length)
        {
            string word = string.Empty;
            string nextLetter = string.Empty;
            List<string> letters = Enum.GetNames(typeof(eBullsAndCowsCharacter)).ToList();
            Random random = new Random();

            for (int i = 0; i < i_Length; i++)
            {
                nextLetter = letters[random.Next(0, letters.Count)];
                word += nextLetter;
                letters.Remove(nextLetter);
            }

            return word;
        }
    }
}
