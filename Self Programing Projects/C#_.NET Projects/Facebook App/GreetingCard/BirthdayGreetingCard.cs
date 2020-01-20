using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreetingCard
{
    public class BirthdayGreetingCard : GreetingCardForm, IGreetingCardBuilder
    {
        private PictureBox picture;
        
        public void BuildGreetingCardForm()
        {
            m_GreetingCard.SetGreetingCardForm(base.InitGreetingCardForm());
        }

        public void BuildGreetingCardBackGround()
        {
            m_GreetingCard.SetGreetingCardBackGround(Image.FromFile("../../../GreetingCard/img/birthdayBackground.png"));
        }

        public void BuildGreetingCardMainPictures(List<Image> i_Images)
        {
            int counter = 0;
            List<PictureBox> mainImages = new List<PictureBox>();
            foreach(Image image in i_Images)
            {
                picture = new PictureBox();
                picture.BackgroundImage = image;
                picture.Size = new Size(80, 80);
                picture.Bounds = Rectangle.Round(picture.Bounds);
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Region = GetCircle(0, 0, 80, 80);
                picture.Name = "BirthdayMainPicture" + counter;
                picture.Location = counter == 0 ? new Point(350, 150) : new Point(150, 340);
                mainImages.Add(picture);
                counter++;
            }

            m_GreetingCard.SetGreetingCardMainPhotos(mainImages);
        }

        public void BuildGreetingCardAddOnImages()
        {
            m_GreetingCard.SetGreetingCardAddOnImages(new List<PictureBox>());
        }

        public void BuildGreetingCardText(List<string> i_Names)
        {
            Label text = new Label();
            text.Name = "TextOnCard";
            text.Text = string.Format("{0} \n\n\nIt's your {2} Birthday!!\nI wish you the best \n\n\n\n\n                     {1}", i_Names[0], i_Names[1], i_Names[2]);
            text.Font = new Font("Arial", 14.8f);
            text.Location = new Point(130, 170);
            text.AutoSize = true;
            text.Enabled = true;
            text.ForeColor = Color.DeepPink;
            text.BackColor = Color.Transparent;
            m_GreetingCard.SetGreetingCardText(text);
        }
        
        public GreetingCard GetCard()
        {
            return this.m_GreetingCard;
        }
    }
}
