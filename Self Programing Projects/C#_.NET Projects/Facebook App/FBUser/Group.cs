using System.Collections.Generic;

namespace FBUser
{
    public class Group
    {
        public string m_GroupName;
        public List<Post> m_GroupPosts = new List<Post>();

        public Group(string i_GroupName, List<Post> i_GroupPosts )
        {
            this.m_GroupName = i_GroupName;
            this.m_GroupPosts = i_GroupPosts;
        }
    }
}
