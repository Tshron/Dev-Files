using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FacebookAppServer
{
    internal class AlbumUtils
    {
        internal static string SetCoverPhoto(List<FBUser.Album> m_Album)
        {
            string coverUrl = string.Empty;
            foreach (FBUser.Album album in m_Album)
            {
                if (album.Name == "Cover Photos")
                {
                    coverUrl = album.Photos[0].PictureNormalURL;
                    break;
                }
            }

            return coverUrl;
        }

        internal static List<FBUser.Album> SetAlbums(User i_User)
        {
            List<FBUser.Album> fbAlbums = new List<FBUser.Album>();
            try
            {
                string defaultPic = "default";
                fbAlbums.Add(new FBUser.Album(defaultPic, "PhotosTaggedIn", "Photo of You", i_User.PhotosTaggedIn));

                foreach (FacebookWrapper.ObjectModel.Album album in i_User.Albums)
                {
                    fbAlbums.Add(new FBUser.Album(album.PictureAlbumURL, album.Description, album.Name, album.Photos));
                }

                FBUser.Album withMyFriend = collectFriendsPhotos(fbAlbums, defaultPic);
                if (withMyFriend.Photos.Count > 0)
                {
                    fbAlbums.Insert(0, withMyFriend);
                }
            }
            catch (Exception e)
            {
            }

            return fbAlbums;
        }

        private static FBUser.Album collectFriendsPhotos(List<FBUser.Album> i_Albums, string i_AlbumPicture)
        {
            FBUser.Album friendsAlbum = new FBUser.Album(i_AlbumPicture, "My friends and I", "With my Friends!", null);

            foreach (FBUser.Album album in i_Albums)
            {
                foreach (FBUser.Photo photo in album.Photos)
                {
                    foreach (string friendId in photo.TaggedPeopleIds)
                    {
                        if (Server.m_AppSettings.FriendsToFollow.Contains(friendId))
                        {
                            friendsAlbum.Photos.Add(photo);
                        }
                    }
                }
            }

            return friendsAlbum;
        }

        private static void updateAlbumTitle(int i_Id, string i_Title)
        {
            Server.m_User.m_Album[i_Id].Name = i_Title;
        }

        private static void updateAlbumDescription(int i_Id, string i_Discription)
        {
            Server.m_User.m_Album[i_Id].Description = i_Discription;
        }

        internal static List<Action<int, string>> UpdateFields()
        {
            List<Action<int, string>> functions = new List<Action<int, string>>();

            functions.Add(new Action<int, string>(updateAlbumTitle));
            functions.Add(new Action<int, string>(updateAlbumDescription));

            return functions;
        }
    }
}
