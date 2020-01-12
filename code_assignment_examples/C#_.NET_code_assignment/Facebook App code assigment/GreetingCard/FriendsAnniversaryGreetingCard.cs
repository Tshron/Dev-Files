using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreetingCard
{
    public class FriendsAnniversaryGreetingCard : GreetingCardForm, IGreetingCardBuilder 
    {
        private PictureBox picture;

        public void BuildGreetingCardForm()
        {
            m_GreetingCard.SetGreetingCardForm(base.InitGreetingCardForm()); 
        }

        public void BuildGreetingCardBackGround()
        {
            m_GreetingCard.SetGreetingCardBackGround(Image.FromFile("../../../GreetingCard/img/blueBackground.png"));
        }

        public void BuildGreetingCardAddOnImages()
        {
            List<PictureBox> addOnImages = new List<PictureBox>();
            m_GreetingCard.SetGreetingCardAddOnImages(addOnImages);
        }

        public void BuildGreetingCardMainPictures(List<Image> i_Images)
        {
            int counter = 0;
            List<PictureBox> mainImages = new List<PictureBox>();
            foreach (Image image in i_Images)
            {
                if(counter > 4)
                {
                    break;
                }

                picture = new PictureBox();
                picture.BackgroundImage = image;
                if(counter < 2)
                {
                    picture.Bounds = Rectangle.Round(picture.Bounds);
                    picture.Region = GetCircle(0, 0, 80, 80);
                    picture.Size = new Size(80, 80);
                    picture.Location = new Point(100 + (350 * counter), 50);
                }
                else
                {
                    picture.Size = new Size(100, 100);
                    picture.Location = new Point(190 + (110 * counter), 300);
                }

                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Name = "FacebookMainImg" + counter;
               
                mainImages.Add(picture);
                counter++;
            }

            m_GreetingCard.SetGreetingCardMainPhotos(mainImages);
        }

        public void BuildGreetingCardText(List<string> i_Names)
        {
            Label text = new Label();
            text.Name = "TextOnCard";
            i_Names.Add(
                m_GreetingCard.ProfileImages.Count <= 2 ? string.Empty :
                m_GreetingCard.ProfileImages.Count > 3 ? "Here are some photos of you together" :
                " Here are a photo of you together");
            text.Text = string.Format("{0}\n\nYou and {1} are friends for {2} years!\n\nIts time to celebrate!\n{3}", i_Names.ToArray());
            text.Font = new Font("Arial", 13.8f);
            text.Location = new Point(50, 150);
            text.AutoSize = true;
            text.BackColor = Color.Transparent;
            text.Enabled = true;
            text.ForeColor = System.Drawing.Color.MidnightBlue;
            m_GreetingCard.SetGreetingCardText(text);
        }

        public GreetingCard GetCard()
        {
            return this.m_GreetingCard;
        }
    }
}   