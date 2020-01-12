using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreetingCard
{
    public interface IGreetingCardBuilder
    {
        void BuildGreetingCardForm();

        void BuildGreetingCardBackGround();

        void BuildGreetingCardMainPictures(List<Image> i_Images);

        void BuildGreetingCardAddOnImages();

        void BuildGreetingCardText(List<string> i_Names);

        void AssemblyCard();

        GreetingCard GetCard();
    }
}