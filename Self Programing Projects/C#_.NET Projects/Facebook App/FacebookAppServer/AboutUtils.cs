using FacebookWrapper.ObjectModel;
using FBUser;

namespace FacebookAppServer
{
    internal class AboutUtils
    {
        internal static PersonalDetails BuildPersonalDetails(User i_User)
        {
            PersonalDetails ps = new PersonalDetails(i_User.Name, i_User.Email);
            ps.Birthday = i_User.Birthday;
            ps.Hometown = i_User.Hometown != null ? i_User.Hometown.Name : null;
            ps.ProfileUrl = i_User.PictureLargeURL;
            ps.Id = i_User.Id;
            ps.ProfilePicture = i_User.ImageSmall;
            ps.Gender = i_User.Gender.HasValue ? i_User.Gender.Value == User.eGender.female ? FBUser.eGender.female : eGender.male : eGender.male;
            return ps;
        }
    }
}
