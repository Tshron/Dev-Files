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
        internal static List<FBUser.Post> SetPosts(ref FBUser.FBUser i_UserAsRef, FacebookObjectCollection<Post> i_Posts, Image i_UserPhoto, string i_UserName)
        {
            List<FBUser.Post> posts = new List<FBUser.Post>();

            foreach (Post post in i_Posts)
            {
                posts.Add(new FBUser.Post(i_UserAsRef, post.Caption, post.Description, post.CreatedTime, ServerUtils.RandomAmountOf(), ServerUtils.RandomAmountOf(), i_UserPhoto, post.PictureURL));
            }

            return posts;
        }

        internal static void ActOnLike(FBUser.Post i_Post)
        {
            i_Post.ActOnLike();
        }

        internal static void NotifyAboutComment(FBUser.Post i_Post)
        {
            i_Post.NotifyAboutComment();
        }

        internal static FBUser.Post GetPost(int i_PostId)
        {
            return Server.User.m_Feed.First(post => post.PostId == i_PostId);
        }
    }
}
