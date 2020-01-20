using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FacebookAppServer
{
    internal class GroupUtils
    {
        internal static List<FBUser.Group> SetGroups(FacebookObjectCollection<Group> i_Groups)
        {
            List<FBUser.Group> groups = new List<FBUser.Group>();
            foreach (Group group in i_Groups)
            {
                List<FBUser.Post> groupPosts = new List<FBUser.Post>();
                foreach (Post groupPost in group.WallPosts)
                {
                    groupPosts.Add(new FBUser.Post(groupPost.From.Name, groupPost.Caption, groupPost.Description, groupPost.CreatedTime, ServerUtils.RandomAmountOf(), ServerUtils.RandomAmountOf(), group.ImageSmall, groupPost.PictureURL));
                }

                groups.Add(new FBUser.Group(group.Name, groupPosts));
            }

            return groups;
        }
    }
}
