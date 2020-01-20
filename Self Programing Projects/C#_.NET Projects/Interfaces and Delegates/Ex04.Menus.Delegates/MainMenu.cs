using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MainMenu : MenuItem
    {
        public MainMenu(string i_DisplayName) : base(i_DisplayName)
        {
        }

        public MainMenu(string i_DisplayName, List<MenuItem> i_ItemsUnderThis) : base(i_DisplayName, i_ItemsUnderThis)
        {
        }
    }
}