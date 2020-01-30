using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using FBUser;
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

        internal static string m_Error = string.Empty;

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

        public static List<string> GetAllCardsOptions()
        {
            return GreetingCardHandler.GetAllCardsOptions();
        }

        public static Post GetPost(int i_PostId)
        {
            return PostUtils.GetPost(i_PostId);
        }

        public static void ActOnLike(Post i_Post)
        {
            PostUtils.ActOnLike(i_Post);
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
    }
}
