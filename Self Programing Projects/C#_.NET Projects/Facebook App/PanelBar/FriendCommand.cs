using System.Collections.Generic;
using FBUser;

namespace PanelBar
{
    public class FriendCommand : IFilterable<Feed>
    {
        public Feed Filter(Feed i_Filter)
        {
            List<Post> followedFriendsFeedList = new List<Post>();

            foreach (Post friend in i_Filter)
            {
                if (i_Filter.FriendsId[friend.Author.UserId])
                {
                    followedFriendsFeedList.Add(friend);
                }
            }
            
            return new Feed(followedFriendsFeedList);
        }
    }
}
