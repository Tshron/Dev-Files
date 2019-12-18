using System.Drawing;

namespace BullsAndCowsFormUI
{
    public class Player : Cell
    {
        private static int s_Width = 25;
        private static int s_Height = 25;

        public Player(int i_XAxis, int i_YAxis)
            : base(s_Width, s_Height)
        {
            setPosition(i_XAxis, i_YAxis);
            setPicture(m_ImagePath);
        }

        public override void ChangePicture(string i_NewColor)
        {            
            if(Color == null)
            {
                m_Picture.Left -= 7;
                m_Picture.Top -= 7;
            }

            string colorPath = "../../img/" + i_NewColor + "Head.png";
            m_Picture.Image = Image.FromFile(colorPath);
            Color = i_NewColor;
            m_Picture.Width = 40;
            m_Picture.Height = 40;
        }

        protected override void setPosition(int i_XAxis, int i_YAxis)
        {
            m_RowNumber = i_XAxis;
            m_ColNumber = i_YAxis;
            m_YAxis = i_YAxis == 0 ? 83 : 83 + (64 * i_YAxis);
            m_XAxis = i_XAxis == 0 ? 30 * (i_XAxis + 1) : 30 * (i_XAxis + 1) + (35 * i_XAxis);
        }
    }
}
