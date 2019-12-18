using System;
using System.Drawing;
using System.Windows.Forms;
using Language;

namespace BullsAndCowsFormUI
{
    public partial class ColorPickerForm : Form
    {
        private Player m_TheButtonWhoOpendedThePicker;
        private Row m_RowOfButtonWhoOpenedThePicker;
        private int m_NumberOfButtonToSet;

        public ColorPickerForm(int i_TheButtonWhoOpenedThePicker, Row i_RowOfButtonWhoOpenedThePicker)
        {
            m_TheButtonWhoOpendedThePicker = i_RowOfButtonWhoOpenedThePicker.Cells[i_TheButtonWhoOpenedThePicker];
            m_RowOfButtonWhoOpenedThePicker = i_RowOfButtonWhoOpenedThePicker;
            m_NumberOfButtonToSet = i_TheButtonWhoOpenedThePicker;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(200, 100);
            this.StartPosition = FormStartPosition.CenterParent;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            buttonCreation(Enum.GetNames(typeof(eBullsAndCowsCharacter)));
        }

        private void buttonCreation(String[] i_ColorNamesArray)
        {
            int i = 0;
            foreach(string color in i_ColorNamesArray)
            {
                Button colorButton = new Button();
                colorButton.Size = new Size(50, 50);
                colorButton.Name = color.ToLower();
                colorButton.BackgroundImage = Image.FromFile("../../img/" + color.ToLower() + ".png");
                colorButton.BackgroundImageLayout = ImageLayout.Stretch;
                
                colorButton.Location = new Point(((i % 4) * 50), ((i / 4) * 50));
                colorButton.Click += colorButton_Click;
                i++;
                this.Controls.Add(colorButton);
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            string color = (sender as Button).Name;
            if (Language.Language.NoDuplicates(m_RowOfButtonWhoOpenedThePicker.ColorsInRow, m_NumberOfButtonToSet, color))
            {
                ErrorForm error = new ErrorForm();
                error.ShowDialog();
            }
            else
            {
                color = (sender as Button).Name;
                m_TheButtonWhoOpendedThePicker.ChangePicture(color);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
