using System.Linq;
using FBUser;

namespace PanelBar
{
     public class PostLengthCommand : ISortable<Feed>
    {
        public Feed Sort(Feed i_Sort)
        {
            return new Feed(i_Sort.OrderByDescending(o => string.IsNullOrEmpty(o.PostContent) ? 0 : o.PostContent.Length).ToList());
        }

        public Feed ReverseSort(Feed i_Sort)
        {
            return new Feed(i_Sort.OrderBy(o => string.IsNullOrEmpty(o.PostContent) ? 0 : o.PostContent.Length).ToList());
        }
    }
}
