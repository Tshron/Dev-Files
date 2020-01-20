using System;
using System.Collections.Generic;
using System.Drawing;
using Group = FBUser.Group;
using Post = FBUser.Post;

namespace FacebookAppServer
{
    internal class Feed
    {
        internal static List<Tuple<Post, string, Image>> BuildUserFeed(FBUser.FBUser i_User)
        {
            List<Tuple<Post, string, Image>> feedPostLinkedList = new List<Tuple<Post, string, Image>>();

            foreach(FBUser.Friend friend in i_User.m_FriendsList)
            {
                foreach(Post post in friend.FriendPosts)
                {
                    feedPostLinkedList.Add(new Tuple<Post, string, Image>(post, friend.m_About.m_Name, friend.m_About.m_ProfilePicture));
                }
            }

            foreach(Group group in i_User.m_UserGroups)
            {
                foreach(Post post in group.m_GroupPosts)
                {
                    feedPostLinkedList.Add(
                        new Tuple<Post, string, Image>(post, post.AuthorName, post.postProfileImage));
                }
            }

            return feedPostLinkedList;
        }
    }
}
