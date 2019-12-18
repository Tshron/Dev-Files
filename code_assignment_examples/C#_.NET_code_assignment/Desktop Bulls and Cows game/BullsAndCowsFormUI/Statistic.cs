using System.Drawing;

namespace BullsAndCowsFormUI
{
    public class Statistic : Cell 
    {
        private static int s_Width = 12;
        private static int s_Height = 12;

        public Statistic(int i_XAxis, int i_YAxis)
            : base(s_Width, s_Height)
        {
            setPosition(i_XAxis, i_YAxis);
            setPicture(m_ImagePath);
        }
        public override void ChangePicture(string i_NewPicture)
        {
            if (Color == null)
            {
                m_Picture.Left -= 1;
                m_Picture.Top -= 1;
            }
            
            m_Picture.Image = Image.FromFile(i_NewPicture);
            m_Picture.Width = s_Width + 3;
            m_Picture.Height = s_Width + 3;
        }
        protected override void setPosition(int i_XAxis, int i_YAxis)
        {
            m_RowNumber = i_XAxis;
            m_ColNumber = i_YAxis;
            switch(i_XAxis)
            {
                case 0:
                    m_XAxis = 307;
                    m_YAxis = 73 + (i_YAxis * 64);
                    break;
                case 1:
                    m_XAxis = 335;
                    m_YAxis = 73 + (i_YAxis * 64);
                    break;
                case 2:
                    m_XAxis = 307;
                    m_YAxis = 103 + (i_YAxis * 64);
                    break;
                case 3:
                    m_XAxis = 335;
                    m_YAxis = 103 + (i_YAxis * 64);
                    break;
            }
        }
    }
}