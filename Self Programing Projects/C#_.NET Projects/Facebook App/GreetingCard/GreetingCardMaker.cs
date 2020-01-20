using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingCard
{
    public class GreetingCardMaker
    {
        private IGreetingCardBuilder m_GreetingCardBuilder;
        private List<Image> m_Images;
        private List<string> m_Names;

        public GreetingCardMaker(IGreetingCardBuilder i_GreetingCardBuilder, List<Image> i_Images, List<string> i_Names)
        {
            this.m_GreetingCardBuilder = i_GreetingCardBuilder;
            this.m_Images = i_Images;
            this.m_Names = i_Names;
        }

        public GreetingCard GetGreetingCard()
        {
            return m_GreetingCardBuilder.GetCard();
        }

        public void AssemblyCard()
        {
            m_GreetingCardBuilder.BuildGreetingCardBackGround();
            m_GreetingCardBuilder.BuildGreetingCardMainPictures(m_Images);
            m_GreetingCardBuilder.BuildGreetingCardAddOnImages();
            m_GreetingCardBuilder.BuildGreetingCardText(m_Names);
            m_GreetingCardBuilder.BuildGreetingCardForm();
            m_GreetingCardBuilder.AssemblyCard();
        }
    }
}
