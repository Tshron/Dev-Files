using System;
using System.Collections.Generic;

using StringUtils;

namespace BullsAndCows
{
    using Language = Language.Language;
    public class Guess
    {
        public static int m_NumberOfGuesses = 0;

        public static List<string> m_ComputerPattern { get; set; }

        private List<string> m_UserGuess;

        public List<string> UserGuess
        {
            get
            {
                return m_UserGuess;
            }

            set
            {
                m_UserGuess = value;
            }
        }

        public char[] m_Match = new char[4] { ' ', ' ', ' ', ' ' };

        public Guess(List<string> i_CurrentUserGuess)
        {
            m_UserGuess = i_CurrentUserGuess;
            getGuessScoreString();
        }

        private void getGuessScoreString()
        {
            int rightPosition = ComparisonStatistics.HowManyCharactersInRightPlace(m_ComputerPattern, m_UserGuess);
            for (int i = 0; i < rightPosition; i++)
            {
                m_Match[i] = 'V';
            }

            int rightCharacters = ComparisonStatistics.HowManyRightCharacters(m_ComputerPattern, m_UserGuess) - rightPosition;
            for (int i = 0; i < rightCharacters; i++)
            {
                m_Match[i + rightPosition] = 'X';
            }
        }

        public static bool GuessValidation(string i_UserGuess)
        {
            bool valid = false;
            if (i_UserGuess == "Q")
            {
                valid = true;
            }
            else
            {
                if (!(Language.WordBelongToLanguage(i_UserGuess) && Language.NoDuplicates(i_UserGuess)))
                {
                    throw new Exception();
                }
                else
                {
                    if (i_UserGuess.Length != m_ComputerPattern.Count)
                    {
                        if (i_UserGuess.Length > m_ComputerPattern.Count)
                        {
                            throw new Exception();
                        }
                        else
                        {
                           throw new Exception();
                        }
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }

            return valid;
        }
    }
}