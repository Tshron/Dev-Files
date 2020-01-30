using System;
using System.Collections.Generic;
using FBUser;

namespace PanelBar
{
    public class LastDaysCommand : IFilterable<Feed>
    {
        public Feed Filter(Feed i_Filter)
        {
            List<Post> fromLastDays = new List<Post>();

            foreach (Post friend in i_Filter)
            {
                if(friend.PostCreateTime != null)
                {
                    if((DateTime.Now - friend.PostCreateTime).TotalDays <= 10)
                    {
                        fromLastDays.Add(friend);
                    }
                }
            }

            return new Feed(fromLastDays);
        }
    }
}
