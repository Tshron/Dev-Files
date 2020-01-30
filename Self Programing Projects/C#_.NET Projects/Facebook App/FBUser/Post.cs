using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using FacebookWrapper.ObjectModel;

namespace FBUser
{
    public class Post
    {
        private static int s_PostId = 0;

        public int PostId { get; set; }

        public FBUser Author { get; set; }

        public string PostContent { get; }

        public string PostCaption { get; set; }

        public DateTime PostCreateTime { get; }

        public int AmountOfLikes { get; set; }

        public int AmountOfComments { get; set; }

        public Image PostProfileImage { get; set; }

        public string PostContentImageUrl { get; set; }

        public bool IsLikedByUser { get; set; }

        public Post(FBUser i_Author, string i_PostCaption, string i_PostContent, DateTime? i_PostCreateTime, int i_AmountOfLikes, int i_AmountOfComments, Image i_PostProfileImage, string i_PostContentImageUrl)
        {
            this.PostId = s_PostId++;
            this.Author = i_Author;
            this.PostContent = i_PostContent;
            this.PostCaption = i_PostCaption;
            this.PostCreateTime = i_PostCreateTime.HasValue ? i_PostCreateTime.Value : new DateTime();
            this.AmountOfLikes = i_AmountOfLikes;
            this.AmountOfComments = i_AmountOfComments;
            this.PostProfileImage = i_PostProfileImage;
            this.PostContentImageUrl = i_PostContentImageUrl;
            this.IsLikedByUser = false;
        }

        public void ActOnLike()
        {
            if(IsLikedByUser)
            {
                AmountOfLikes--;
            }
            else
            {
                AmountOfLikes++;
            }

            IsLikedByUser = !IsLikedByUser;
        }

        public void NotifyAboutComment()
        {
            AmountOfComments++;
        }
    }
}
