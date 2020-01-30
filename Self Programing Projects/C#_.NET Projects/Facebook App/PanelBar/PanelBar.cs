using System.Collections.Generic;
using FBUser;

namespace PanelBar
{
    public class PanelBar
    {
        private static Stack<Feed> s_FeedState = new Stack<Feed>();
        private Feed currentFeed;

        public Feed CurrentFeed
        {
            get
            {
                return currentFeed;
            }
        }

        public ControlState ControlButtons = new ControlState();

        public PanelBar(Feed i_Feed)
        {
            currentFeed = i_Feed;
            AddState();

            ControlButtons.Add(new SortButton<Feed> { Command = new SortCommand<Feed> { Title = "LikeCommand", Sortable = new LikeCommand() } });
            ControlButtons.Add(new SortButton<Feed> { Command = new SortCommand<Feed> { Title = "CommentCommand", Sortable = new CommentCommand() } });
            ControlButtons.Add(new FilterButton<Feed> { Command = new FilterCommand<Feed> { Title = "FriendCommand", Filterable = new FriendCommand() } });
            ControlButtons.Add(new FilterButton<Feed> { Command = new FilterCommand<Feed> { Title = "LastDaysCommand", Filterable = new LastDaysCommand() } });
            ControlButtons.Add(new FilterButton<Feed> { Command = new FilterCommand<Feed> { Title = "FemaleCommand", Filterable = new FemaleCommand() } });
            ControlButtons.Add(new SortButton<Feed> { Command = new SortCommand<Feed> { Title = "PostLengthCommand", Sortable = new PostLengthCommand() } });
        }

        private void AddState()
        {
            s_FeedState.Push(currentFeed);
        }

        private Feed RemoveState()
        {
            return s_FeedState.Pop();
        }

        public IControlButton<Feed> ForwardStep(string i_Button)
        {
            IControlButton<Feed> changedButton = null;
            foreach(IControlButton<Feed> button in ControlButtons)
            {
                if (button.Command.Title == i_Button)
                {
                    if (button.Status == ePanelBarStatus.Off)
                    {
                        AddState();
                    }

                    button.ForwardStatus();

                    if (button.Status != ePanelBarStatus.Off)
                    {
                        currentFeed = button.Command.Execute(CurrentFeed);
                    }
                    else
                    {
                        currentFeed = RemoveState();
                    }

                    changedButton = button;
                }
            }

            return changedButton;
        }
    }
}
