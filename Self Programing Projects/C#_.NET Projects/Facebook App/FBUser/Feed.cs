using System.Collections;
using System.Collections.Generic;

namespace FBUser
{
    public class Feed : IEnumerable<Post>
    {
        private List<Post> Posts { get; set; }

        public Dictionary<string, bool> FriendsId { get; set; }

        public Feed(List<Post> i_Posts)
        {
            FriendsId = new Dictionary<string, bool>();
            Posts = i_Posts;
            foreach(Post post in Posts) 
            {
                if(!FriendsId.ContainsKey(post.Author.UserId))
                {
                    FriendsId.Add(post.Author.UserId, false);
                    post.Author.m_FollowingFriendDelegates += updateFollowing;
                }
            }
        }

        public IEnumerator<Post> GetEnumerator()
        {
            foreach(Post post in Posts)
            {
                if(!string.IsNullOrEmpty(post.PostContent) || !string.IsNullOrEmpty(post.PostContentImageUrl))
                {
                    yield return post;
                }
            }
        }

        public IEnumerator<IEnumerator<Post>> GetFeedDisplayModeEnumerator()
        {
            IEnumerator<Post> innerEnumerator = GetEnumerator();
            List<IEnumerator<Post>> iteratorList = new List<IEnumerator<Post>>();
            while(innerEnumerator.MoveNext())
            {
                List<Post> subList = new List<Post>() { innerEnumerator.Current };
                for(int i = 0; i < 2; i++)
                {
                    if(innerEnumerator.MoveNext())
                    {
                        subList.Add(innerEnumerator.Current);
                    }
                }

                iteratorList.Add(subList.GetEnumerator());
            }

            innerEnumerator.Dispose();

            return iteratorList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return this.GetEnumerator();
        }

        private void updateFollowing(string i_FriendId)
        {
            FriendsId[i_FriendId] = !FriendsId[i_FriendId];
        }
    }
}
