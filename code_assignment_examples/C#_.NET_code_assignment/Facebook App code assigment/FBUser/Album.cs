using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FBUser
{
    public class Album
    {
        private static Random random = new Random();

        public string m_PictureAlbumURL { get; }

        public List<Photo> m_Photos = new List<Photo>();

        public string m_Description { get; }

        public string m_Name { get; set; }

        public int m_Count { get; }
        
        public Album(string i_PictureAlbumURL, string i_Description, string i_Name, FacebookObjectCollection<FacebookWrapper.ObjectModel.Photo> i_Photos)
        {
            AddPhotos(i_Photos);
            m_PictureAlbumURL = i_PictureAlbumURL;
            m_Description = i_Description;
            m_Name = i_Name;
            m_Count = 0;
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
                    m_Photos.Add(new Photo(photo.PictureNormalURL, photo.Width, photo.Height, photo.Name, random.Next(0, 50), random.Next(0, 50), tags));
                }
            }
        }
    }
}
