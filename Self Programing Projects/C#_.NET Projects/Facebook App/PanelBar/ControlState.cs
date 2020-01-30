using System.Collections;
using System.Collections.Generic;
using FBUser;

namespace PanelBar
{
    public class ControlState : IEnumerable<IControlButton<Feed>>
    {
        private List<IControlButton<Feed>> m_ControlButtons = new List<IControlButton<Feed>>();

        public void Add(IControlButton<Feed> i_Button)
        {
            m_ControlButtons.Add(i_Button);
        }

        public void Remove(IControlButton<Feed> i_Button)
        {
            m_ControlButtons.Remove(i_Button);
        }

        public IEnumerator<IControlButton<Feed>> GetEnumerator()
        {
            foreach (IControlButton<Feed> feed in m_ControlButtons)
            {
                yield return feed;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
