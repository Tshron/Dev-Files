using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAppServer
{
    public abstract class Settings
    {
        internal static readonly string sr_Folder = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
        internal string m_FileName;

        public string FileName()
        {
            return m_FileName;
        }

        public List<FieldMessage<string, string>> Messages { get; set; }
        
        internal void ErrorEventHandler(Settings i_Settings)
        {
            LoggerUtils.BrokenFile(i_Settings, m_FileName);
        }
        
        public abstract void LoadFromFile();

        public abstract void SaveToFile();
    }
}
