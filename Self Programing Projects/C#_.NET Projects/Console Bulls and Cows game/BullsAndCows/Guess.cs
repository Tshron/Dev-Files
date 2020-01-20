namespace BullsAndCows
{
    using System;
    using System.Text;

    using StringUtils;
    using Language = Language.Language;

    public class Guess
    {
        public static int m_NumberOfGuesses = 0;

        public static string m_ComputerPattern { get; set; }

        private string m_UserGuess;

        public string UserGuess
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
        
        public Guess()
        {
            m_UserGuess = new StringBuilder().Insert(0, " ", m_ComputerPattern.Length).ToString();
        }

        public Guess(string i_CurrentUserGuess)
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
                    throw new Exception(Messages.GetMessage(13));
                }
                else
                {
                    if (i_UserGuess.Length != m_ComputerPattern.Length)
                    {
                        if (i_UserGuess.Length > m_ComputerPattern.Length)
                        {
                            throw new Exception(Messages.GetMessage(11));
                        }
                        else
                        {
                            throw new Exception(Messages.GetMessage(12));
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