using System.Linq;
using FBUser;

namespace PanelBar
{
     public class CommentCommand : ISortable<Feed>
    {
        public Feed Sort(Feed i_Sort)
        {
            return new Feed(i_Sort.OrderByDescending(o => o.AmountOfComments).ToList());
        }

        public Feed ReverseSort(Feed i_Sort)
        {
            return new Feed(i_Sort.OrderBy(x => x.AmountOfComments).ToList());
        }
    }
}