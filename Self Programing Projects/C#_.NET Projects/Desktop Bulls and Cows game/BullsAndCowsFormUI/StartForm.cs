using System;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public partial class StartForm : Form
    {
        private int m_NumberOfChances = 4;

        public int NumberOfChances
        {
            get
            {
                return m_NumberOfChances;
            }
        }

        public StartForm()
        {
            InitializeComponent();
        }

        private void buttonNumberOfChances_Click(object sender, EventArgs e)
        {
            m_NumberOfChances = (sender as Button).Name == "PlusButton"
                                    ?
                                    m_NumberOfChances <= 9 ? ++m_NumberOfChances : m_NumberOfChances
                                    : m_NumberOfChances > 4
                                        ? --m_NumberOfChances
                                        : m_NumberOfChances;
                                       
            NumberOfChancesCounter.Text = m_NumberOfChances == 10
                                              ? m_NumberOfChances.ToString()
                                              : " " + m_NumberOfChances.ToString();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}