using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public class BoardForm : Form
    {
        public Board m_Board;

        public int GetWidth
        {
            get
            {
                return m_Board.Width;
            }
        }

        private int m_Height = 64;
        public int GetHeight
        {
            get { return m_Height; }
        }

        public BoardForm(int i_NumberOfRows)
        {
            m_Board = new Board(i_NumberOfRows, this);
            showComputerGuess(m_Board.ComputerLuckyPattern);
            buildBoard();
            menuButtons();
        }
        private void showComputerGuess(List<string> i_ComputerGuess)
        {
            foreach (string color in i_ComputerGuess)
            {
                PictureBox guessElement = new PictureBox();
                guessElement.Name = "GuessRevelElement";
                guessElement.Image = Image.FromFile("../../img/" + color + "Head.png");
                guessElement.Size = new Size(50, 50);
                guessElement.SizeMode = PictureBoxSizeMode.StretchImage;
                guessElement.Location = new Point((17 + (i_ComputerGuess.IndexOf(color) * 65)), 6);
                //TODO : TURN TO FALSE BEFORE SUBMIT
                guessElement.Visible = false;
                this.Controls.Add(guessElement);
            }
        }
        
        private void buildBoard()
        {
            addHeader();
            addRows();
            addButtons();
        }
        private void addHeader()
        {
            PictureBox computerPictureBox = Row.setComputerRow();
            computerPictureBox.Name = "ComputerGuessHider";
            this.Controls.Add(computerPictureBox);

            PictureBox logo = new PictureBox();
            logo.Image = Image.FromFile(@"../../img/logo.jpg");
            logo.Size = new Size((Width - 200), 64);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.Location = new Point(278, 0);
            this.Controls.Add(logo);
        }
        private void addRows()
        {
            for (int i = 1; i < m_Board.m_Rows.Length + 1; i++)
            {
                PictureBox row = new PictureBox();
                row = m_Board.m_Rows[i - 1].Picture;
                this.Controls.Add(row);
                row = m_Board.m_Rows[i - 1].Statistic;
                row.MouseClick += arrowButtonClickForGuessEval_MouseClick;
                this.Controls.Add(row);
            }
        }
        private void addButtons()
        {
            for (int i = 0; i < m_Board.m_Rows.Length; i++)
            {
                for (int j = 0; j < m_Board.m_Rows[i].Columns; j++)
                {
                    PictureBox button = m_Board.m_Rows[i].Cells[j].Picture;
                    button.MouseClick += rowButtonClickForColorChange_MouseClick;
                    this.Controls.Add(button);
                    button.BringToFront();
                }
            }
        }

        private void arrowButtonClickForGuessEval_MouseClick(object sender, MouseEventArgs e)
        {
            int row = int.Parse((sender as PictureBox).Name) - 1;
            List<string> guessString = m_Board.m_Rows[row].ColorsInRow.ToList();
            m_Board.ChangeRowActivity(row);
            m_Board.GetUserGuessResult(guessString, row);
        }
        private void rowButtonClickForColorChange_MouseClick(object sender, MouseEventArgs e)
        {
            int rowNumber = int.Parse((sender as PictureBox).Name.Substring(1, 1));
            int colNumber = int.Parse((sender as PictureBox).Name.Substring(0, 1));
            ColorPickerForm colorForm = new ColorPickerForm(colNumber, m_Board.m_Rows[rowNumber]);
            colorForm.ShowDialog();
            if (m_Board.m_Rows[rowNumber].IsRowReady())
            {
                m_Board.EnableStatistic(rowNumber);
            }
        }

        private void menuButtons()
        {
            int extraHeight = 75;
            addRerunButton(extraHeight);
            addExitButton(extraHeight);
        }
        private void addRerunButton(int i_ExtraHeight)
        {
            PictureBox rerun = new PictureBox();
            rerun.Name = "ReRunGame";
            rerun.Image = Image.FromFile("../../img/rerun.jpg");
            rerun.Size = new Size(108, 64);
            rerun.SizeMode = PictureBoxSizeMode.StretchImage;
            rerun.Location = new Point(0, (m_Board.m_Rows.Length * m_Height + i_ExtraHeight));
            rerun.Click += exitButton_Click;
            this.Controls.Add(rerun);
        }
        private void addExitButton(int i_ExtraHeight)
        {
            PictureBox exit = new PictureBox();
            exit.Name = "ExitGame";
            exit.Image = Image.FromFile("../../img/exit.jpg");
            exit.Size = new Size(108, 64);
            exit.SizeMode = PictureBoxSizeMode.StretchImage;
            exit.Location = new Point((GetWidth - 108), (m_Board.m_Rows.Length * m_Height + i_ExtraHeight));
            exit.Click += exitButton_Click;
            this.Controls.Add(exit);
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            if ((sender as PictureBox).Name.Equals("ReRunGame"))
            {
                this.Visible = false;
                this.Close();
                PlayGame game = new PlayGame();
                game.Game();
            }
        }
    }
}