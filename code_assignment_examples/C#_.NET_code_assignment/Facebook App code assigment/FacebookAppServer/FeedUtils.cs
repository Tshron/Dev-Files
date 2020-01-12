using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Group = FBUser.Group;
using Post = FBUser.Post;

namespace FacebookAppServer
{
    internal class FeedUtils
    {
        internal static Tuple<bool, bool> m_FilterBarStatus = new Tuple<bool, bool>(false, false);
        
        internal static List<Tuple<Post, string, Image>> BuildUserFeed(FBUser.FBUser i_User)
        {
            SetFilterBarStatus("None");
            List<Tuple<Post, string, Image>> feedPostLinkedList = new List<Tuple<Post, string, Image>>();

            foreach(FBUser.FBUser friend in i_User.m_FriendsList)
            {
                foreach(Post post in friend.m_UserPosts)
                {
                    feedPostLinkedList.Add(new Tuple<Post, string, Image>(post, friend.m_About.Name, friend.m_About.ProfilePicture));
                }
            }

            foreach(Group group in i_User.m_UserGroups)
            {
                foreach(Post post in group.m_GroupPosts)
                {
                    feedPostLinkedList.Add(
                        new Tuple<Post, string, Image>(post, post.AuthorName, post.PostProfileImage));
                }
            }

            return feedPostLinkedList;
        }

        internal static List<Tuple<Post, string, Image>> BuildFeedListByFollowedFriends(FBUser.FBUser i_User)
        {
            SetFilterBarStatus("friendFilter");
            List<Tuple<Post, string, Image>> followedFriendsFeedList = new List<Tuple<Post, string, Image>>();
            foreach(FBUser.FBUser friend in i_User.m_FriendsList)
            {
                if(!friend.Follow)
                {
                    continue;
                }

                foreach (Post post in friend.m_UserPosts)
                {
                    followedFriendsFeedList.Add(new Tuple<Post, string, Image>(post, friend.m_About.Name, friend.m_About.ProfilePicture));
                }
            }

            return followedFriendsFeedList;
        }

        internal static List<Tuple<Post, string, Image>> SortFeedByLikes()
        {
            SetFilterBarStatus("likeFilter");
            return Server.User.m_Feed.OrderByDescending(o => o.Item1.AmountOfLikes).ToList();
        }

        internal static void SetFilterBarStatus(string i_FilterToActivate)
        {
            if(i_FilterToActivate.Equals("None"))
            {
                m_FilterBarStatus = new Tuple<bool, bool>(false, false);
            }
            else
            {
                m_FilterBarStatus = i_FilterToActivate.Equals("likeFilter")
                                               ? new Tuple<bool, bool>(true, false)
                                               : new Tuple<bool, bool>(false, true);
            }
        }

        internal static Tuple<bool, bool> GetFilterBarStatus()
        {
            return m_FilterBarStatus;
        }
    }
}
