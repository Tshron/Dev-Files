using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FacebookWrapper.ObjectModel;
using Post = FacebookWrapper.ObjectModel.Post;

namespace FacebookAppServer
{
    internal class PostUtils
    {
        internal static List<FBUser.Post> SetPosts(FacebookObjectCollection<Post> i_Posts, Image i_UserPhoto, string i_UserName)
        {
            List<FBUser.Post> posts = new List<FBUser.Post>();

            foreach (Post post in i_Posts)
            {
                posts.Add(new FBUser.Post(i_UserName, post.Caption, post.Description, post.CreatedTime, ServerUtils.RandomAmountOf(), ServerUtils.RandomAmountOf(), i_UserPhoto, post.PictureURL));
            }

            return posts;
        }

        internal static void NotifyAboutLike(FBUser.Post i_Post)
        {
            i_Post.NotifyAboutLike();
        }

        internal static void NotifyAboutComment(FBUser.Post i_Post)
        {
            i_Post.NotifyAboutComment();
        }

        internal static Tuple<FBUser.Post, string, Image> GetPost(int i_PostId)
        {
            return Server.User.m_Feed.First(post => post.Item1.PostId == i_PostId);
        }
    }
}
