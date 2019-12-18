using System.Drawing;

namespace FBUser
{
    public class PersonalDetails
    {
        public string m_Name { get; set; }
        public string m_Email { get; set; }
        public string m_Id { get; set; }
        public string m_Birthday { get; set; }
        public string m_Hometown { get; set; }
        public string m_Cover { get; set; }
        public string m_ProfileUrl { get; set; }
        public Image m_ProfilePicture { get; set; }

        public PersonalDetails(string i_Name, string i_Email)
        {
            m_Name = i_Name;
            m_Email = i_Email;
            m_Id = null;
            m_Birthday = null;
            m_Cover = null;
            m_ProfileUrl = null;
            m_ProfilePicture = null;
            m_Hometown = null;
        }
    }
}
