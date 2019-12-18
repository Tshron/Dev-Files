using System;
using System.IO;
using System.Reflection;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookAppServer
{
    public static class Server
    {
        internal static readonly string sr_Folder = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
        internal static FBUser.FBUser m_User = new FBUser.FBUser();
        internal static string m_Error;

        public static FBUser.FBUser User
        {
            get
            {
                return m_User;
            }
        }
        public static string Error
        {
            get
            {
                return m_Error;
            }
        }
        public static LoginResult Login()
        {
            return Authentication.checkPermissions();
        }

        public static LoginResult Connect(string i_AccessToken)
        {
            return Authentication.Connect(i_AccessToken);
        }

        public static void InitEntity(User i_User)
        {
            ServerUtils.SetAlbums(i_User);
            ServerUtils.SetPersonalDetails(i_User);
            m_User.m_UserPosts = ServerUtils.SetPosts(i_User.Posts, i_User.ImageSmall, m_User.m_About.m_Name);
            ServerUtils.SetFriends(i_User.Friends);
            m_User.m_UserGroups = ServerUtils.SetGroups(i_User.Groups);
            m_User.m_Feed = Feed.BuildUserFeed(Server.m_User);
        }

        public static void SaveToFile<T>(T i_Type, string i_Path)
        {
            try
            {
                XmlUtils.SaveToFile(i_Type, i_Path);
            }
            catch (Exception e)
            {
                m_Error = "Unable save to file";
            }
        }

        public static T LoadFromFile<T>(T i_Type, string i_Path)
        {
            try
            {
                i_Type = XmlUtils.LoadFromFile(i_Type, i_Path);
            }
            catch (Exception e)
            {
                m_Error = "Unable load from file";
            }

            return i_Type;
        }

        public static string BuildPath(string i_Folder, string i_Name)
        {
            string path;
            if (i_Folder == null)
            {
                path = Path.Combine(sr_Folder, i_Name);
            }
            else
            {
                path = Path.Combine(i_Folder, i_Name);
            }

            return path;
        }
    }
}
