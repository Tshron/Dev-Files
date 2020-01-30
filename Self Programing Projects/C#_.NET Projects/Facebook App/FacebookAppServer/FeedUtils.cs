using System.Collections.Generic;
using Group = FBUser.Group;
using Post = FBUser.Post;

namespace FacebookAppServer
{
    internal class FeedUtils
    {        
        internal static List<Post> BuildUserFeed(FBUser.FBUser i_User)
        {
            List<Post> feedPostsList = new List<Post>();

            foreach(FBUser.FBUser friend in i_User.m_FriendsList)
            {
                foreach(Post post in friend.m_UserPosts)
                {
                    feedPostsList.Add(post);
                }
            }

            foreach(Group group in i_User.m_UserGroups)
            {
                foreach(Post post in group.m_GroupPosts)
                {
                    feedPostsList.Add(post);
                }
            }

            return feedPostsList;
        }
    }
}
