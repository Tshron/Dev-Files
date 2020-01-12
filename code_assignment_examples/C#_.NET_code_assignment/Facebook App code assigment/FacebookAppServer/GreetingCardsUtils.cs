using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FBUser;
using GreetingCard;

namespace FacebookAppServer
{
     internal class GreetingCardsUtils
     {
         internal static string GetFriendshipLength(FBUser.FBUser i_FriendToSend)
        {
            Random randomLength = new Random();

            return randomLength.Next(1, 15).ToString();
        }

        internal static List<Image> GetFriendTaggedPhotosOfUser(FBUser.FBUser i_Friend)
        {
            List<Image> photosWithFriendTag = new List<Image>();
            
            foreach(Album album in Server.m_User.m_Album)
            {
                foreach(Photo photo in album.Photos)
                {
                    if(photo.TaggedPeopleIds.Contains(i_Friend.UserId))
                    {
                        photosWithFriendTag.Add(Image.FromFile(photo.PictureNormalURL));
                    }
                }
            }

            return photosWithFriendTag;
        }

        internal static List<string> GetUserAndFriendsNames(FBUser.FBUser i_FriendToSend)
        {
            return new List<string>(){i_FriendToSend.m_About.Name, Server.m_User.m_About.Name};
        }

        internal static List<Image> GetUserAndFriendPhotos(FBUser.FBUser i_FriendToSend)
        {
            return new List<Image>() {i_FriendToSend.m_About.ProfilePicture, Server.m_User.m_About.ProfilePicture };
        }

        internal static string GetFriendAge(FBUser.FBUser i_Friend)
        {
            string age = string.Empty;
            try
            {
               age = ((int)(DateTime.Now - DateTime.Parse(i_Friend.m_About.Birthday)).TotalDays / 365).ToString();
            }
            catch(Exception e)
            {
                Random randomAge = new Random();
                age = randomAge.Next(10, 70).ToString();
            }

            return age;
        }

        internal static void SendCardByEmail(Form i_CardToSend, string i_FriendEmailAddress)
        {
            string path = "../../tempImage.bmp";
            using(Bitmap bmp = new Bitmap(i_CardToSend.Width, i_CardToSend.Height))
            {
                i_CardToSend.DrawToBitmap(bmp, new Rectangle(0,0, i_CardToSend.Width, i_CardToSend.Height));
                bmp.Save(path);
                EmailSender.EmailSender sender = new EmailSender.EmailSender(Server.m_User.m_About.Email, i_FriendEmailAddress);
                sender.SendEmail("I maid this for you", path);
            }
        }

        internal static Form GetGreetingCard(string i_Name, FBUser.FBUser i_Friend)
        {
            return GreetingCardHandler.GetGreetingCard(i_Name, i_Friend).Form;
        }
    }
}
