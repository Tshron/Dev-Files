using System.Drawing;
using System;
using System.Collections.Generic;

namespace FBUser
{
    public class FBUser 
    {
        public PersonalDetails m_About; 
        public List<Album> m_Album = new List<Album>();
        public List<Friend> m_FriendsList = new List<Friend>();
        public List<Post> m_UserPosts = new List<Post>();
        public List<Group> m_UserGroups = new List<Group>();
        public List<Tuple<Post, string, Image>> m_Feed = new List<Tuple<Post, string, Image>>();

        public void StartTrackingOnFriends(List<string> i_FriendsIds)
        {
            foreach(string id in i_FriendsIds)
            {
                foreach (Friend friend in m_FriendsList)
                {
                    if (friend.m_About.m_Id == id)
                    {
                        friend.m_Follow = true;
                    }
                }
            }
        }

        public List<string> SaveTrackingOnFriends()
        {
            List<string> followFriend = new List<string>();
            foreach (Friend friend in m_FriendsList)
            {
                if(friend.m_Follow)
                {
                    followFriend.Add(friend.m_About.m_Id);
                }
            }

            return followFriend;
        }
    }
}
