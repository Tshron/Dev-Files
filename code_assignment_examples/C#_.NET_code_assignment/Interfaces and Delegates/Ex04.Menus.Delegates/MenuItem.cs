using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        private int m_Label;

        public int Label
        {
            get { return m_Label; }
        }

        private string m_DisplayName;

        public string DisplayName
        {
            get { return m_DisplayName; }
        }

        // $G$ CSS-999 (-3) This kind of field should be readonly.
        private List<MenuItem> m_ItemsUnderMe;

        public List<MenuItem> ItemsUnderThis
        {
            get { return m_ItemsUnderMe; }
        }

        public void AddTab(MenuItem i_MenuItem)
        {
            m_ItemsUnderMe.Add(i_MenuItem);
        }

        public virtual void Show()
        {
            for(int i = 1; i <= m_ItemsUnderMe.Count; i++)
            {
                Console.WriteLine("-> {0}.{1}", i, m_ItemsUnderMe[i - 1].m_DisplayName);
            }
        }

        public MenuItem(string i_DisplayName)
        {
            m_DisplayName = i_DisplayName;
            m_ItemsUnderMe = new List<MenuItem>();
        }

        public MenuItem(string i_DisplayName, List<MenuItem> i_ItemsUnderThis) : this(i_DisplayName)
        {   
            m_ItemsUnderMe = i_ItemsUnderThis;
        }
    }
}
