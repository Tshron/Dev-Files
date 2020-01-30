using System.Collections.Generic;
using FBUser;

namespace PanelBar
{
     public class FemaleCommand : IFilterable<Feed>
    {
        public Feed Filter(Feed i_Filter)
        {
            List<Post> ourWomen = new List<Post>();

            foreach (Post friend in i_Filter)
            {
                if (friend.Author.m_About.Gender == eGender.female)
                {
                    ourWomen.Add(friend);
                }
            }

            return new Feed(ourWomen);
        }
    }
}
