using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreetingCard
{
    public class GreetingCardForm
    {
        internal GreetingCard m_GreetingCard;

        public GreetingCardForm()
        {
            m_GreetingCard = new GreetingCard();
        }

        public Form InitGreetingCardForm()
        {
            Form form = new Form();
            form.Size = new Size(600, 600);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.SizeGripStyle = SizeGripStyle.Hide;
            form.MaximumSize = form.Size;

            return form;
        }

        public void AssemblyCard()
        {
            m_GreetingCard.Form.BackgroundImage = m_GreetingCard.BackImage;
            foreach (PictureBox picture in m_GreetingCard.ProfileImages)
            {
                m_GreetingCard.Form.Controls.Add(picture);
            }

            foreach (PictureBox picture in m_GreetingCard.AddOnImages)
            {
                m_GreetingCard.Form.Controls.Add(picture);
            }

            m_GreetingCard.Form.Controls.Add(m_GreetingCard.Text);
        }

        public Region GetCircle(int i_X, int i_Y, int i_Width, int i_Height)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(i_X, i_Y, i_Width, i_Height);

            return new Region(gp);
        }
    }
}
