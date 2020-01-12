using System.Collections.Generic;
using System.Text;

namespace FacebookAppServer
{
    internal class LoggerUtils
    {
        internal static void BrokenFile<T>(T i_Type, string i_FileName)
        {
            string message = string.Format("Some problem occurred.\n" +
                                    "Please check if {0} file exists" +
                                    " and under the following folder: {1}\nor make sure that the xml is valid", i_FileName, Settings.sr_Folder);
            if(emptyObject(i_Type))
            {
                Server.m_Error = message;
            }
        }

        private static bool emptyObject<T>(T i_Type)
        {
            bool emptyObject = true;
            foreach (var prop in i_Type.GetType().GetProperties())
            {
                if (i_Type.GetType().GetProperty(prop.Name).GetValue(i_Type, null) != null)
                {
                    emptyObject = false;
                }
            }

            return emptyObject;
        }

        internal static void EmptyField(Settings i_Type, List<FieldMessage<string, string>> i_Fields)
        {
            StringBuilder message = new StringBuilder();
            foreach(FieldMessage<string,string> field in i_Type.Messages)
            {
                if (i_Type.GetType().GetProperty(field.Key).GetValue(i_Type, null) == null)
                {
                    message.AppendFormat("{0}", field.Value);
                }
            }
            
            Server.m_Error = message.ToString();
        }
    }
}
