using System;
using System.Collections.Generic;
using System.IO;

namespace FacebookAppServer
{
    public sealed class ServerSettings : Settings
    {
        private static readonly string rs_FileName = "serverSettings.xml";
        
        public string ApplicationId { get; set; }

        public string AppSettingsName { get; set; }

        public string AppSettingsLocation { get; set; }

        public string[] desiredFacebookPermissions { get; set; }
        
        internal ServerSettings()
        {
            m_FileName = rs_FileName;
            ApplicationId = null;
            AppSettingsName = null;
            AppSettingsLocation = null;
            desiredFacebookPermissions = null;
            Messages = new List<FieldMessage<string, string>>();
        }
        
        private void ErrorEventHandler()
        {
            ErrorEventHandler(Server.m_Settings);
            LoggerUtils.EmptyField(Server.m_Settings, Messages);
        }

        public override void LoadFromFile()
        {
            try
            {
                Server.m_Settings = XmlUtils.LoadFromFile(this, Path.Combine(sr_Folder, m_FileName));
            }
            finally
            {
                ErrorEventHandler();
            }
        }

        public override void SaveToFile()
        {
            try
            {
                string path = ServerUtils.BuildPath(sr_Folder, m_FileName);
                XmlUtils.SaveToFile(Server.m_Settings, path);
            }
            finally
            {
                ErrorEventHandler();
            }
        }
    }
}
