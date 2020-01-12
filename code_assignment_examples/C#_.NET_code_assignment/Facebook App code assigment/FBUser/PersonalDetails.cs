using System.Drawing;

namespace FBUser
{
    public class PersonalDetails
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string Birthday { get; set; }

        public string Hometown { get; set; }

        public string Cover { get; set; }

        public string ProfileUrl { get; set; }

        public Image ProfilePicture { get; set; }

        public PersonalDetails(string i_Name, string i_Email)
        {
            Name = i_Name;
            Email = i_Email;
            Id = null;
            Birthday = null;
            Cover = null;
            ProfileUrl = null;
            ProfilePicture = null;
            Hometown = null;
        }
    }
}
