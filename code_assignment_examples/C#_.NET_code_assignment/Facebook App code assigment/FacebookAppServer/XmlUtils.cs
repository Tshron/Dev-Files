using System.IO;
using System.Xml.Serialization;

namespace FacebookAppServer
{
    internal class XmlUtils
    {
        internal static void SaveToFile<T>(T i_Type, string i_Path)
        {
            using (Stream stream = new FileStream(i_Path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, i_Type);
            }            
        }

        internal static T LoadFromFile<T>(T i_Type, string i_Path)
        {
            if (File.Exists(i_Path))
            {
                using (Stream stream = new FileStream(i_Path, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    i_Type = (T)serializer.Deserialize(stream);
                }
            }

            return i_Type;
        }
    }
}
