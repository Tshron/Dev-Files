using System;
using System.Drawing;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public partial class WinLoseForm : Form
    {
        public WinLoseForm(bool i_isWinMode)
        {
            InitializeComponent();
            if(i_isWinMode)
            {
                this.textbox.Text = "    You Won !! (:";
                this.textbox.ForeColor = Color.Chartreuse;
            }
        }

        private void goNextButtons_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
