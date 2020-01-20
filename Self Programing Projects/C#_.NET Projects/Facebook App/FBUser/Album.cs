using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FBUser
{
    public class Album
    {
        private static Random random = new Random();

        public string PictureAlbumURL { get; }

        public List<Photo> Photos = new List<Photo>();

        public string Description { get; set; }

        public string Name { get; set; }

        public int Count { get; }

        public Album(string i_PictureAlbumURL, string i_Description, string i_Name, FacebookObjectCollection<FacebookWrapper.ObjectModel.Photo> i_Photos)
        {
            AddPhotos(i_Photos);
            PictureAlbumURL = i_PictureAlbumURL;
            Description = i_Description;
            Name = i_Name;
            Count = 0;
        }

        public void AddPhotos(FacebookObjectCollection<FacebookWrapper.ObjectModel.Photo> i_Photos)
        {
            if(i_Photos != null)
            {
                List<string> tags;
                foreach (FacebookWrapper.ObjectModel.Photo photo in i_Photos)
                {
                    tags = new List<string>();
                    if (photo.Tags != null)
                    {
                        foreach (PhotoTag tag in photo.Tags)
                        {
                            tags.Add(tag.User.Id);
                        }
                    }

                    Photos.Add(new Photo(photo.PictureNormalURL, photo.Width, photo.Height, photo.Name, random.Next(0, 50), random.Next(0, 50), tags));
                }
            }
        }
    }
}
