using System.Linq;
using FBUser;

namespace PanelBar
{
    public class LikeCommand : ISortable<Feed>
    {
        public Feed Sort(Feed i_Sort)
        {
            return new Feed(i_Sort.OrderByDescending(o => o.AmountOfLikes).ToList());
        }

        public Feed ReverseSort(Feed i_Sort)
        {
            return new Feed(i_Sort.OrderBy(o => o.AmountOfLikes).ToList());
        }
    }
}