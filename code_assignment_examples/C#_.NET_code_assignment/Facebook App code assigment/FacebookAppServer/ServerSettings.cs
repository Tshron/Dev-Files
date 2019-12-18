using System.IO;

namespace FacebookAppServer
{
    public sealed class ServerSettings
    {
        private static readonly object sr_Lock = new object();
        private static readonly string rs_FileName = "serverSettings.xml";

        private static ServerSettings s_Instance = null;

        public static ServerSettings ServerSetting
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_Lock)
                    {
                        if(s_Instance == null)
                        {
                            s_Instance = new ServerSettings();
                        }
                    }
                }

                return s_Instance;
            }
        }

        public string ApplicationId { get; set; }

        public string AppSettingsName { get; set; }

        public string AppSettingsLocation { get; set; }

        public string[] desiredFacebookPermissions { get; set; }

        public ServerSettings()
        {
            ApplicationId = null;
            AppSettingsName = null;
            AppSettingsLocation = null;
            desiredFacebookPermissions = null;
        }
        private static void errorEventHandler()
        {
            if (s_Instance.ApplicationId == null
                & s_Instance.AppSettingsLocation == null
                & s_Instance.AppSettingsName == null
                & s_Instance.desiredFacebookPermissions == null)
            {
                Server.m_Error = string.Format("Some problem occurred.\nPlease check if 'serverSettings.xml' file exists and under the following folder: {0}\nor make sure that the xml is valid", Server.sr_Folder);
            }
            else
            {
                if (s_Instance.AppSettingsName == null)
                {
                    Server.m_Error = string.Format("") +
                        "'AppSettingsName' field missing in serverSettings.xml\n" +
                        "Please add: '<AppSettingsName>[file_name].xml</AppSettingsName>' for serverSettings.xml file.\n" +
                        "You can change AppSettings's directory by add: '<AppSettingsLocation>[Directory]</AppSettingsLocation>'";
                }
            }
        }
        public static void LoadFromFile()
        {
            s_Instance = Server.LoadFromFile(ServerSetting, Path.Combine(Server.sr_Folder, rs_FileName));
            errorEventHandler();
        }

        public void SaveToFile()
        {
            Server.SaveToFile(this, Server.sr_Folder + rs_FileName);
        }
    }
}
