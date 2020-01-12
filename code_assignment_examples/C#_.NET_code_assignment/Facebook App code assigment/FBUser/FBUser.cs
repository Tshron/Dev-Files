using System.Drawing;
using System;
using System.Collections.Generic;

namespace FBUser
{
    public class FBUser
    {
        public PersonalDetails m_About; 
        public List<Album> m_Album;
        public List<FBUser> m_FriendsList;
        public List<Post> m_UserPosts;
        public List<Group> m_UserGroups = new List<Group>();
        public List<Tuple<Post, string, Image>> m_Feed = new List<Tuple<Post, string, Image>>();

        public bool Follow { get; set; }

        public string UserId{ get; set; }

        public FBUser(string i_UserId)
        {
            UserId = i_UserId;
        }

        public void StartTrackingOnFriends(List<string> i_FriendsIds)
        {
            foreach(string id in i_FriendsIds)
            {
                foreach (FBUser friend in m_FriendsList)
                {
                    if (friend.m_About.Id == id)
                    {
                        friend.Follow = true;
                    }
                }
            }
        }

        public List<string> SaveTrackingOnFriends()
        {
            List<string> followFriend = new List<string>();
            foreach (FBUser friend in m_FriendsList)
            {
                if(friend.Follow)
                {
                    followFriend.Add(friend.m_About.Id);
                }
            }

            return followFriend;
        }
    }
}
