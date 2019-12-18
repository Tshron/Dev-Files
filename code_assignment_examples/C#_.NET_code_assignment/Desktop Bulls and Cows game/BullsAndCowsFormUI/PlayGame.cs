using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    internal class PlayGame
    {
        private int m_NumberOfChances = 0;
        private BoardForm m_Board;

        public void Game()
        {
            Start();
        }
        
        public void Start()
        {
            Row.NumberOfExistingRows = 0;
            StartForm start = new StartForm();
            start.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            start.ShowDialog();
            m_NumberOfChances = start.NumberOfChances;

            if(start.DialogResult == DialogResult.OK)
            {
                m_Board = new BoardForm(m_NumberOfChances);
                m_Board.Width = m_Board.GetWidth + 10;
                m_Board.Height = m_Board.GetHeight * (m_NumberOfChances + 2) + 35;
                m_Board.BackColor = System.Drawing.Color.FromArgb(76, 98, 95);
                m_Board.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                m_Board.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                m_Board.ShowDialog();
            }
        }
    }
}
