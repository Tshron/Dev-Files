using System.Collections.Generic;

namespace FacebookAppServer
{
    public sealed class AppSettings : Settings
    {
        private static readonly object sr_Lock = new object();

        public string AccessToken { get; set; }

        public string UserName { get; set; }

        public string UserPicture { get; set; }

        public List<string> FriendsToFollow { get; set; }

        internal AppSettings()
        {
            m_FileName = Server.m_Settings.AppSettingsName;
            AccessToken = null;
            UserName = null;
            UserPicture = null;
            FriendsToFollow = new List<string>();
            Messages = new List<FieldMessage<string, string>>();
        }

        private void errorEventHandler()
        {
            ErrorEventHandler(Server.AppSettings);
        }
        
        public override void LoadFromFile()
        {
            try
            {
                string path = ServerUtils.BuildPath(Server.m_Settings.AppSettingsLocation, Server.m_Settings.AppSettingsName);
                Server.m_AppSettings = XmlUtils.LoadFromFile(this, path);
            }
            finally
            {
                errorEventHandler();
            }
        }

        public override void SaveToFile()
        {
            try
            {
                string path = ServerUtils.BuildPath(Server.m_Settings.AppSettingsLocation, Server.m_Settings.AppSettingsName);
                Server.m_AppSettings.UserName = Server.User.m_About.Name.Split(' ')[0];
                Server.m_AppSettings.UserPicture = Server.User.m_About.ProfileUrl;
                Server.m_AppSettings.FriendsToFollow = Server.User.SaveTrackingOnFriends();
                XmlUtils.SaveToFile(Server.AppSettings, path);
            }
            finally
            {
                errorEventHandler();
            }
        }
    }
}
