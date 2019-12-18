using System.Collections.Generic;

namespace FBUser
{
    public class Friend
    {
        public PersonalDetails m_About { get; set; }
        public List<Post> FriendPosts = new List<Post>();
        public bool m_Follow { get; set; }

        public Friend(PersonalDetails i_About, List<Post> i_FriendPosts)
        {
            this.m_About = i_About;
            FriendPosts = i_FriendPosts;
            m_Follow = false;
        }
    }
}