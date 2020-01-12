using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreetingCard
{
    public class LoveGreetingCard : GreetingCardForm, IGreetingCardBuilder
    {
        private PictureBox picture;

        public void BuildGreetingCardAddOnImages()
        {
            List<PictureBox> addOnImages = new List<PictureBox>();

            for (int i = 0; i < 3; i++)
            {
                picture = new PictureBox();
                picture.ImageLocation = "../../../GreetingCard/img/LoveCardImg" + i + ".png";
                picture.Bounds = Rectangle.Round(picture.Bounds);
                picture.Region = GetCircle(2, -1, 75, 75);
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Size = new Size(78, 79);
                picture.Location = i < 2 ? new Point(10, 5 + (90 * i)) : new Point(370, 470); 
                picture.Name = "LoveSubPicture" + i;
                addOnImages.Add(picture);
            }

            m_GreetingCard.SetGreetingCardAddOnImages(addOnImages);
        }

        public void BuildGreetingCardBackGround()
        {
            m_GreetingCard.SetGreetingCardBackGround(Image.FromFile("../../../GreetingCard/img/LoveBackground.png"));
        }

        public void BuildGreetingCardForm()
        {
            m_GreetingCard.SetGreetingCardForm(base.InitGreetingCardForm());
        }

        public void BuildGreetingCardMainPictures(List<Image> i_Images)
        {
            int counter = 0;
            List<PictureBox> mainImages = new List<PictureBox>();
            foreach (Image image in i_Images)
            {
                picture = new PictureBox();
                picture.BackgroundImage = image;
                picture.Bounds = Rectangle.Round(picture.Bounds);
                picture.Region = GetCircle(0, 0, 80, 80);
                picture.Size = new Size(80, 80);
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Name = "FacebookMainImg" + counter;
                picture.Location = counter == 0 ? new Point(415, 115) : new Point(320, 85); 
                mainImages.Add(picture);

                counter++;
            }

            m_GreetingCard.SetGreetingCardMainPhotos(mainImages);
        }

        public void BuildGreetingCardText(List<string> i_Names)
        {
            Label text = new Label();
            text.Name = "TextOnCard";
            text.BackColor = Color.Transparent;
            text.Text = string.Format("Roses are red,\nViolets are blue\n\n{0} \n\nMy Love, I just wanted to say\nI LOVE YOU", i_Names[0]);
            text.Font = new Font("Arial", 16.8f, System.Drawing.FontStyle.Bold);
            text.AutoSize = true;
            text.ForeColor = Color.DarkMagenta;
            text.Location = new Point(88, 150);
            text.Enabled = true;
            m_GreetingCard.SetGreetingCardText(text);
        }

        public GreetingCard GetCard()
        {
            return this.m_GreetingCard;
        }
    }
}
