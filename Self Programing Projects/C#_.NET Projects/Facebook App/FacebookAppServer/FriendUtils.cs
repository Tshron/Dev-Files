using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FacebookAppServer
{
    internal class FriendUtils
    {
        internal static List<FBUser.FBUser> SetFriends(FacebookObjectCollection<User> i_Friends)
        {
            List<FBUser.FBUser> friendList = new List<FBUser.FBUser>();
            foreach (User friend in i_Friends)
            {
                friendList.Add(ServerUtils.CreateFriend(friend));
            }

            return friendList;
        }
    }
}
