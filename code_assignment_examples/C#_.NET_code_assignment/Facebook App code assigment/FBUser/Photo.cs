using System.Collections.Generic;

namespace FBUser
{
    public class Photo
    {
        public string PictureNormalURL { get; }

        public long Width { get; }

        public long Height { get; }

        public string Name { get; }

        public int Like { get; }

        public int Comments { get; }

        public List<string> TaggedPeopleIds { get; }

        public Photo(string i_PictureNormalURL, long i_Width, long i_Height, string i_Name, int i_Like, int i_Comments, List<string> i_TaggedPeopleIds)
        {
            PictureNormalURL = i_PictureNormalURL;
            Width = i_Width;
            Height = i_Height;
            Name = i_Name;
            Like = i_Like;
            Comments = i_Comments;
            TaggedPeopleIds = i_TaggedPeopleIds;
        }
    }
}