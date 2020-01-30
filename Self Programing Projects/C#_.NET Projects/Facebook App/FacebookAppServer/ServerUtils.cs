using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FBUser;

namespace FacebookAppServer
{
    internal sealed class ServerUtils
    {
        internal static Random s_Random = new Random();
        private static readonly int sr_SleepFor = 100;
        private static string s_StartingStatus = "Start loading {0}";
        private static string s_FinishStatus = "Loading {0} success!";
        private static object s_lock = new object();

        internal static void InitEntity(User i_User)
        {
            Server.LastUpdateData = i_User.UpdateTime;
            Server.m_User = CreateUser(i_User);
        }

        internal static FBUser.FBUser CreateUser(User i_User)
        {
            FBUser.FBUser user = new FBUser.FBUser(i_User.Id);
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => 
            {
                UpdateStatus(string.Format(s_StartingStatus, "albums"));
                user.m_Album = AlbumUtils.SetAlbums(i_User);
                UpdateStatus(string.Format(s_FinishStatus, "albums"));
            }));

            tasks.Add(Task.Run(() =>
            {
                UpdateStatus(string.Format(s_StartingStatus, "personal details"));
                user.m_About = AboutUtils.BuildPersonalDetails(i_User);
                UpdateStatus(string.Format(s_FinishStatus, "personal details"));
            }));
            tasks.Add(Task.Run(() =>
            {
                while((user.m_About == null) || (user.m_Album == null))
                {
                    Thread.Sleep(sr_SleepFor);
                }

                UpdateStatus(string.Format(s_StartingStatus, "cover photo"));
                user.m_About.Cover = AlbumUtils.SetCoverPhoto(user.m_Album);
                UpdateStatus(string.Format(s_FinishStatus, "cover photo"));
            }));
            tasks.Add(Task.Run(() =>
            {
                while (user.m_About == null)
                {
                    Thread.Sleep(sr_SleepFor);
                }

                UpdateStatus(string.Format(s_StartingStatus, "post"));
                user.m_UserPosts = PostUtils.SetPosts(ref user, i_User.Posts, i_User.ImageSmall, user.m_About.Name);
                UpdateStatus(string.Format(s_FinishStatus, "post"));
            }));
            tasks.Add(Task.Run(() =>
            {
                UpdateStatus(string.Format(s_StartingStatus, "friends"));
                user.m_FriendsList = FriendUtils.SetFriends(i_User.Friends);
                UpdateStatus(string.Format(s_FinishStatus, "friends"));
            }));
            tasks.Add(Task.Run(() =>
            {
                UpdateStatus(string.Format(s_StartingStatus, "groups"));
                user.m_UserGroups = GroupUtils.SetGroups(i_User.Groups);
                UpdateStatus(string.Format(s_FinishStatus, "group"));
            }));
            tasks.Add(Task.Run(() =>
            {
                while ((user.m_FriendsList == null) || (user.m_UserPosts == null))
                {
                    Thread.Sleep(sr_SleepFor);
                }

                UpdateStatus(string.Format(s_StartingStatus, "feed"));
                user.m_Feed = new Feed(FeedUtils.BuildUserFeed(user));
                UpdateStatus(string.Format(s_FinishStatus, "feed"));
            }));
            
            Task.WaitAll(tasks.ToArray());
            
            return user;
        }

        internal static FBUser.FBUser CreateFriend(User i_User)
        {
            FBUser.FBUser user = new FBUser.FBUser(i_User.Id);
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => user.m_Album = AlbumUtils.SetAlbums(i_User)));
            tasks.Add(Task.Run(() => user.m_About = AboutUtils.BuildPersonalDetails(i_User)));
            tasks.Add(Task.Run(() =>
            {
                while ((user.m_About == null) || (user.m_Album == null))
                {
                    Thread.Sleep(sr_SleepFor);
                }

                user.m_About.Cover = AlbumUtils.SetCoverPhoto(user.m_Album);
            }));
            tasks.Add(Task.Run(() =>
            {
                while (user.m_About == null)
                {
                    Thread.Sleep(sr_SleepFor);
                }

                user.m_UserPosts = PostUtils.SetPosts(ref user, i_User.Posts, i_User.ImageSmall, user.m_About.Name);
            }));

            Task.WaitAll(tasks.ToArray());

            return user;
        }

        internal static void UpdateStatus(string i_Status)
        {
            lock (s_lock)
            {
                Server.m_Status.Add(i_Status);
            }            
        }

        internal static string BuildPath(string i_Folder, string i_Name)
        {
            string path;
            if (i_Folder == null)
            {
                path = Path.Combine(Settings.sr_Folder, i_Name);
            }
            else
            {
                path = Path.Combine(i_Folder, i_Name);
            }

            return path;
        }

        internal static int RandomAmountOf()
        {
            return s_Random.Next(1, 50);
        }
    }
}
