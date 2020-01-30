using System;
using System.Windows.Forms;

namespace FacebookAppUI
{
    public partial class commentForm : Form
    {
        public commentForm()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
