using System.Collections.Generic;

namespace FBUser
{
    public class Photo
    {
        public string m_PictureNormalURL { get; }
        public long m_Width { get; }
        public long m_Height { get; }
        public string m_Name { get; }
        public int m_Like { get; }
        public int m_Comments { get; }
        public List<string> m_TaggedPeopleIds { get; }
        public Photo(string i_PictureNormalURL, long i_Width, long i_Height, string i_Name, int i_Like, int i_Comments, List<string> i_TaggedPeopleIds)
        {
            m_PictureNormalURL = i_PictureNormalURL;
            m_Width = i_Width;
            m_Height = i_Height;
            m_Name = i_Name;
            m_Like = i_Like;
            m_Comments = i_Comments;
            m_TaggedPeopleIds = i_TaggedPeopleIds;
        }
    }
}