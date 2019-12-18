using System.Collections.Generic;

namespace FacebookAppServer
{
    public sealed class AppSettings
    {
        private static readonly object sr_Lock = new object();
        private static AppSettings s_Instance = null;

        public string AccessToken { get; set; }

        public string UserName { get; set; }

        public string UserPicture { get; set; }

        public List<string> FriendsToFollow { get; set; }

        private AppSettings()
        {
            AccessToken = null;
            UserName = null;
            UserPicture = null;
            FriendsToFollow = new List<string>();
        }

        public static AppSettings AppSetting
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_Lock)
                    {
                        if(s_Instance == null)
                        {
                            s_Instance = new AppSettings();
                        }
                    }
                }

                return s_Instance;
            }
        }
        
        
        public static void SaveToFile()
        {
            string path = Server.BuildPath(ServerSettings.ServerSetting.AppSettingsLocation, ServerSettings.ServerSetting.AppSettingsName);
            s_Instance.UserName = Server.User.m_About.m_Name.Split(' ')[0];
            s_Instance.UserPicture = Server.User.m_About.m_ProfileUrl;
            s_Instance.FriendsToFollow = Server.User.SaveTrackingOnFriends();
            Server.SaveToFile(s_Instance, path);
        }

        public static void LoadFromFile()
        {
            string path = Server.BuildPath(ServerSettings.ServerSetting.AppSettingsLocation, ServerSettings.ServerSetting.AppSettingsName);
            s_Instance = Server.LoadFromFile(AppSetting, path);
        }
    }
}
