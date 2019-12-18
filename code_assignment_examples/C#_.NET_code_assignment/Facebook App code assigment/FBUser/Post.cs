using System;
using System.Drawing;

namespace FBUser
{
    public class Post
    {
        public string AuthorName { get; set; }

        public string PostContent { get; }

        public string PostCaption { get; set; }

        public DateTime? PostCreateTime { get; }

        public int AmountOfLikes { get; set; }

        public int AmountOfComments { get; set; }

        public Image postProfileImage { get; set; }

        public string PostContentImageUrl { get; set; }

        public bool IsLikedByUser { get; set; }

        public Post(string i_AuthorName, string i_PostCaption, string i_PostContent, DateTime? i_PostCreateTime, int i_AmountOfLikes, int i_AmountOfComments, Image i_PostProfileImage, string i_PostContentImageUrl)
        {
            this.AuthorName = i_AuthorName;
            this.PostContent = i_PostContent;
            this.PostCaption = i_PostCaption;
            this.PostCreateTime = i_PostCreateTime;
            this.AmountOfLikes = i_AmountOfLikes;
            this.AmountOfComments = i_AmountOfComments;
            this.postProfileImage = i_PostProfileImage;
            this.PostContentImageUrl = i_PostContentImageUrl;
            this.IsLikedByUser = false;
        }
    }
}
