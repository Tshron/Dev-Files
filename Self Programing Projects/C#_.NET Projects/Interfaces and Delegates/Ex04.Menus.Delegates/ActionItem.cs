namespace Ex04.Menus.Delegates
{
    public class ActionItem : MenuItem
    {
        // $G$ CSS-021 (-3) Delegate name should end with the extension of "EventHandler"
        public delegate void ActionItemDelegate();   

        public event ActionItemDelegate ActionItemClicked;

        public ActionItem(string i_ItemName) : base(i_ItemName)
        {
            ActionItemClicked = null;
        }

        public ActionItem(string i_ItemName, ActionItemDelegate i_MenuitemDelegate) : base(i_ItemName)
        {
            ActionItemClicked += i_MenuitemDelegate;
        }

        public override void Show()
        {
            OnClicked();
        }

        internal void OnClicked()
        {
            ActionItemClicked?.Invoke();
        }
    }
}
