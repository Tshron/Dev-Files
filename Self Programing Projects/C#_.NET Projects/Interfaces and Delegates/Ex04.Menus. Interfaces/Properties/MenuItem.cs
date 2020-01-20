using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly List<MenuItem> r_ItemsUnderThis;

        public List<MenuItem> ItemsUnderThis
        {
            get { return r_ItemsUnderThis; }
        }

        private readonly string r_DisplayName;

        public string DisplayName
        {
            get { return r_DisplayName; }
        }

        private int m_Label;

        public int Label
        {
            get { return m_Label; }
        }

        public void AddTab(MenuItem i_MenuItem)
        {
            r_ItemsUnderThis.Add(i_MenuItem);
        }

        public virtual void Show()
        {
            for(int i = 1; i <= r_ItemsUnderThis.Count; i++)
            {
                Console.WriteLine("-> {0}.{1}", i, r_ItemsUnderThis[i - 1].r_DisplayName);
            }
        }

        public MenuItem(string i_DisplayName)
        {
            r_DisplayName = i_DisplayName;
            r_ItemsUnderThis = new List<MenuItem>();
        }

        public MenuItem(string i_DisplayName, List<MenuItem> i_ItemsUnderThis) : this(i_DisplayName)
        {   
            r_ItemsUnderThis = i_ItemsUnderThis;
        }
    }
}
