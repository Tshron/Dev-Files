using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BullsAndCows;

namespace BullsAndCowsFormUI
{
    public class Board
    {
        private BoardForm m_boardFormOfThisBoard;

        private List<string> m_ComputerLuckyPattern;
        public List<string> ComputerLuckyPattern
        {
            get
            {
                return m_ComputerLuckyPattern;
            }
        }
        private int m_PatternLength = 4;

        public int Height => m_Rows.Length > 1 ? m_Rows.Length * m_Rows[0].Height : 0;

        public int Width => m_Rows.Length > 1 ? m_Rows[0].m_RowWidth : 0;

        public Row[] m_Rows;

        public Board(int i_Rows, BoardForm i_BoardForm)
        {
            m_Rows = new Row[i_Rows];
            for (int i = 0; i < m_Rows.Length; i++)
            {
                m_Rows[i] = new Row(m_PatternLength);
            }

            m_boardFormOfThisBoard = i_BoardForm;
            randomWordToComputer(m_PatternLength);
        }

        public void EnableStatistic(int i_RowNumber)
        {
            m_Rows[i_RowNumber].Statistic.Image = Image.FromFile("../../img/lampon.jpg");
            m_Rows[i_RowNumber].Statistic.Enabled = true;
        }

        private void disableStatistic(int i_RowNumber)
        {
            m_Rows[i_RowNumber].Statistic.Image = Image.FromFile("../../img/statistic.jpg");
            m_Rows[i_RowNumber].Statistic.Enabled = false;
        }

        public void ChangeRowActivity(int i_RowNumber)
        {
            if (i_RowNumber < m_Rows.Length)
            {
                disableStatistic(i_RowNumber);
                for (int i = 0; i < m_Rows[i_RowNumber].Cells.Length; i++)
                {
                    m_Rows[i_RowNumber].Cells[i].Picture.Enabled = false;
                    if(i_RowNumber < m_Rows.Length - 1)
                    {
                        m_Rows[i_RowNumber + 1].Cells[i].Picture.Enabled = true;
                    }
                }
            }
        }

        private void randomWordToComputer(int i_LengthOfPattern)
        {
            m_ComputerLuckyPattern = Language.Language.RandomWordWithoutRepeatLetters(i_LengthOfPattern);
            Guess.m_ComputerPattern = m_ComputerLuckyPattern;
        }

        public void GetUserGuessResult(List<string> i_UserGuess, int i_RowNumber)
        {
            Guess result = new Guess(i_UserGuess);
            char[] guessResult = result.m_Match;

            createResultPicture(guessResult, i_RowNumber);
            if (new string(guessResult).Equals("VVVV"))
            {
                winLoseMode(true);
            }
            else
            {
                if (i_RowNumber == m_Rows.Length - 1)
                {
                    winLoseMode(false);
                }
            }
        }

        private void createResultPicture(char[] i_GuessResultChar, int i_RowNumber)
        {
            for (int i = 0; i < i_GuessResultChar.Length; i++)
            {
                if (!i_GuessResultChar[i].Equals(' '))
                {
                    string resultSymbol = i_GuessResultChar[i].Equals('V')
                                              ? "../../img/perfect.png"
                                              : "../../img/hit.png";
                    m_Rows[i_RowNumber].Hits[i].ChangePicture(resultSymbol);
                    m_Rows[i_RowNumber].Hits[i].Picture.Visible = true;
                }
            }
            updateStatistic(i_RowNumber);
        }

        private void updateStatistic(int i_RowNumber)
        {
            for (int j = 0; j < m_Rows[i_RowNumber].Hits.Length; j++)
            {
                PictureBox pictureBox = m_Rows[i_RowNumber].Hits[j].Picture;
                m_boardFormOfThisBoard.Controls.Add(pictureBox);
                pictureBox.BringToFront();
            }
        }

        private void winLoseMode(bool i_isWinMode)
        {
            gameOver();
            WinLoseForm winLose = new WinLoseForm(i_isWinMode);
            winLose.ShowDialog();
        }
        
        private void gameOver()
        {
            Control[] computerGuessHider = m_boardFormOfThisBoard.Controls.Find("ComputerGuessHider", false);
            Control[] computerGuessButtons = m_boardFormOfThisBoard.Controls.Find("GuessRevelElement", true);
            computerGuessHider[0].Visible = false;
            foreach(Control element in computerGuessButtons)
            {
                element.Visible = true;
            }
        }
    }
}