using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreetingCard
{
    public class GreetingCard : IGreetingCardPlan
    {
        public Form Form { get; set; }

        public Image BackImage { get; set; }

        public List<PictureBox> ProfileImages { get; set; }

        public List<PictureBox> AddOnImages { get; set; }

        public Label Text { get; set; }

        public void SetGreetingCardForm(Form i_Form)
        {
            Form = i_Form;
        }

        public void SetGreetingCardBackGround(Image i_BackImage)
        {
            BackImage = i_BackImage;
        }

        public void SetGreetingCardMainPhotos(List<PictureBox> i_ProfilePhotos)
        {
            ProfileImages = i_ProfilePhotos;
        }

        public void SetGreetingCardAddOnImages(List<PictureBox> i_AddOnImages)
        {
            AddOnImages = i_AddOnImages;
        }

        public void SetGreetingCardText(Label i_Text)
        {
            Text = i_Text;
        }
    }
}
