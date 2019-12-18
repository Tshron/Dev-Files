using System.Drawing;
using System.Collections.Generic;
using System;
using FacebookWrapper.ObjectModel;
using FBUser;
using Post = FacebookWrapper.ObjectModel.Post;

namespace FacebookAppServer
{
    internal sealed class ServerUtils
    {
        private static Random s_Random = new Random();

        internal static void SetPersonalDetails(User i_User)
        {
            Server.User.m_About = buildPersonalDetails(i_User);
            Server.User.m_About.m_Cover = setCoverPhoto(Server.User.m_Album);
        }

        internal static void SetFriends(FacebookObjectCollection<User> i_Friends)
        {
            List<FBUser.Post> post;
            PersonalDetails personalDetails;

            foreach (User friend in i_Friends)
            {
                post = SetPosts(friend.Posts, friend.ImageNormal, friend.Name);
                personalDetails = buildPersonalDetails(friend);

                Server.User.m_FriendsList.Add(new Friend(personalDetails, post));
            }
        }

        internal static List<FBUser.Post> SetPosts(FacebookObjectCollection<Post> i_Posts, Image i_UserPhoto, string i_UserName)
        {
            List<FBUser.Post> posts = new List<FBUser.Post>();
            
            foreach (Post post in i_Posts)
            {
                posts.Add(new FBUser.Post(i_UserName, post.Caption, post.Description, post.CreatedTime, randomAmountof(), randomAmountof(), i_UserPhoto, post.PictureURL));
            }

            return posts;
        }

        internal static List<FBUser.Group> SetGroups(FacebookObjectCollection<FacebookWrapper.ObjectModel.Group> i_Groups)
        {
            List<FBUser.Group> groups = new List<FBUser.Group>();
            foreach (FacebookWrapper.ObjectModel.Group group in i_Groups)
            {   
                List<FBUser.Post> groupPosts = new List<FBUser.Post>();
                foreach (FacebookWrapper.ObjectModel.Post groupPost in group.WallPosts)
                {
                    groupPosts.Add(new FBUser.Post(groupPost.From.Name, groupPost.Caption, groupPost.Description, groupPost.CreatedTime, randomAmountof(), randomAmountof(), group.ImageSmall, groupPost.PictureURL));
                }

                groups.Add(new FBUser.Group(group.Name, groupPosts));
            }

            return groups;
        }

        private static int randomAmountof()
        {
            return s_Random.Next(1, 50);
        }

        internal static void SetAlbums(User i_User)
        {
            List<FBUser.Album> fbAlbums = new List<FBUser.Album>();
            string defaultPic = "default";
            fbAlbums.Add(new FBUser.Album(defaultPic, "PhotosTaggedIn", "Photo of You", i_User.PhotosTaggedIn));

            foreach (FacebookWrapper.ObjectModel.Album album in i_User.Albums)
            {
                fbAlbums.Add(new FBUser.Album(album.PictureAlbumURL, album.Description, album.Name, album.Photos));
            }
            
            Server.m_User.m_Album = fbAlbums;
            collectFriendsPhotos(defaultPic);
        }

        private static void collectFriendsPhotos(string i_AlbumPicture)
        {
            FBUser.Album friendsAlbum = new FBUser.Album(i_AlbumPicture, "My friends and I", "With my Friends!", null);

            foreach (FBUser.Album album in Server.User.m_Album)
            {
                foreach (FBUser.Photo photo in album.m_Photos)
                {
                    foreach (string friendId in photo.m_TaggedPeopleIds)
                    {
                        if (AppSettings.AppSetting.FriendsToFollow.Contains(friendId))
                        {
                            friendsAlbum.m_Photos.Add(photo);
                        }
                    }
                }
            }
            if(friendsAlbum.m_Photos.Count > 0)
            {
                Server.m_User.m_Album.Insert(0,friendsAlbum);
            }
            
        }
        private static string setCoverPhoto(List<FBUser.Album> m_Album)
        {
            string coverUrl = string.Empty;
            foreach (FBUser.Album album in m_Album)
            {
                if (album.m_Name == "Cover Photos")
                {
                    coverUrl = album.m_Photos[0].m_PictureNormalURL;
                }
            }

            return coverUrl;
        }

        private static PersonalDetails buildPersonalDetails(User i_User)
        {
            PersonalDetails ps = new PersonalDetails(i_User.Name, i_User.Email);
            ps.m_Birthday = i_User.Birthday;
            ps.m_Hometown = i_User.Hometown != null ? i_User.Hometown.Name : null;
            ps.m_ProfileUrl = i_User.PictureLargeURL;
            ps.m_Id = i_User.Id;
            ps.m_ProfilePicture = i_User.ImageSmall;
            return ps;
        }
    }
}
