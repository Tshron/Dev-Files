using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using Post = FBUser.Post;

namespace FacebookAppServer
{
    public class Server
    {
        private static readonly object sr_Lock = new object();

        internal static ServerSettings m_Settings;

        public static ServerSettings ServerSettings
        {
            get
            {
                if (m_Settings == null)
                {
                    lock (sr_Lock)
                    {
                        if (m_Settings == null)
                        {
                            m_Settings = new ServerSettings();
                        }
                    }
                }

                return m_Settings;
            }
        }

        internal static AppSettings m_AppSettings;

        public static AppSettings AppSettings
        {
            get
            {
                if (m_AppSettings == null)
                {
                    lock (sr_Lock)
                    {
                        if (m_AppSettings == null)
                        {
                            m_AppSettings = new AppSettings();
                        }
                    }
                }

                return m_AppSettings;
            }
        }

        internal static FBUser.FBUser m_User;

        public static FBUser.FBUser User
        {
            get
            {
                return m_User;
            }
        }

        internal static DateTime? LastUpdateData;
        
        private static readonly int sr_ProcessesAtLoading = 14;
        public static int ProcessesAtLoading
        {
            get
            {
                return sr_ProcessesAtLoading;
            }
        }

        internal static List<string> m_Status = new List<string>();

        public static int CurrentProcess
        {
            get
            {
                return m_Status.Count;
            }
        }

        internal static string m_Error;

        public static string Error
        {
            get
            {
                return m_Error;
            }
        }

        public static LoginResult Login()
        {
            return Authentication.Login();
        }

        public static LoginResult Connect(string i_AccessToken)
        {
            return Authentication.Connect(i_AccessToken);
        }

        public static void SetServer(User i_User)
        {
            ServerUtils.InitEntity(i_User);
        }

        public static List<Tuple<FBUser.Post, string, Image>> GetFollowedFriendsFeed()
        {
            return FeedUtils.BuildFeedListByFollowedFriends(m_User);
        }

        public static List<Tuple<FBUser.Post, string, Image>> GetSortedFeedList()
        {
            return FeedUtils.SortFeedByLikes();
        }

        public static void SetFilterBarStatus(string i_FilterToActivate)
        {
            FeedUtils.SetFilterBarStatus(i_FilterToActivate);
        }

        public static Tuple<bool, bool> GetFilterBarStatus()
        {
            return FeedUtils.GetFilterBarStatus();
        }
        
        public static List<string> GetAllCardsOptions()
        {
            return GreetingCardHandler.GetAllCardsOptions();
        }

        public static Tuple<FBUser.Post, string, Image> GetPost(int i_PostId)
        {
            return PostUtils.GetPost(i_PostId);
        }

        public static void NotifyAboutLike(Post i_Post)
        {
            PostUtils.NotifyAboutLike(i_Post);
        }

        public static void NotifyAboutComment(Post i_Post)
        {
            PostUtils.NotifyAboutComment(i_Post);
        }

        public static List<Action<int, string>> UpdateFields()
        {
            return AlbumUtils.UpdateFields();
        }

        public static Form GetGreetingCard(string i_CardName, FBUser.FBUser i_Friend)
        {
            return GreetingCardsUtils.GetGreetingCard(i_CardName, i_Friend);
        }

        public static void SendCardByMail(Form i_CardToSend, FBUser.FBUser i_Friend)
        {
            GreetingCardsUtils.SendCardByEmail(i_CardToSend, i_Friend.m_About.Email);
        }
    }
}
