using System.Drawing;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public abstract class Cell
    {
        protected int m_Width = 25;
        protected int m_Height = 25;
        protected int m_XAxis = 0;
        protected int m_YAxis = 0;
        protected PictureBox m_Picture = new PictureBox();
        public PictureBox Picture
        {
            get { return m_Picture; }
        }

        protected int m_RowNumber;
        protected int m_ColNumber;
        protected string m_ImagePath = "../../img/button.png";

        private string m_Id;
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private string m_Color;
        public string Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public Cell(int i_Width, int i_Height)
        {
            m_Width = i_Width;
            m_Height = i_Height;
            m_Color = null;
        }

        protected abstract void setPosition(int i_XAxis, int iYAxis);

        protected virtual void setPicture(string i_ImagePath)
        {
            m_Picture.Name = m_RowNumber + string.Empty + m_ColNumber;
            m_Picture.Image = Image.FromFile(i_ImagePath);
            m_Picture.Size = new Size(m_Width, m_Height);
            m_Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            m_Picture.Location = new Point(m_XAxis, m_YAxis);
            if (m_ColNumber == 0)
            {
                m_Picture.Enabled = true;
            }
            else
            {
                m_Picture.Enabled = false;
            }
        }

        public abstract void ChangePicture(string i_NewColor);
    }
}
