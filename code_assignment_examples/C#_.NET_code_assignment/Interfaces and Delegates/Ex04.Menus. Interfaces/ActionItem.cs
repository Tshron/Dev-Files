namespace Ex04.Menus.Interfaces
{
    public class ActionItem : MenuItem
    {
        private IShowDate m_ShowDate;
        private IShowTime m_ShowTime;
        private ICountDigits m_CountDigits;
        private IShowVersion m_ShowVersion;

        public ActionItem(string i_DisplayName) : base(i_DisplayName)
        {
        }

        public ActionItem(string i_DisplayName, IShowDate i_ShowDate) : this(i_DisplayName)
        {
            m_ShowDate = i_ShowDate;
        }

        public ActionItem(string i_DisplayName, IShowTime i_ShowTime) : this(i_DisplayName)
        {
            m_ShowTime = i_ShowTime;
        }

        public ActionItem(string i_DisplayName, ICountDigits i_CountDigits) : this(i_DisplayName)
        {
            m_CountDigits = i_CountDigits;
        }

        public ActionItem(string i_DisplayName, IShowVersion i_ShowVersion) : this(i_DisplayName)
        {
            m_ShowVersion = i_ShowVersion;
        }

        public override void Show()
        {
            m_ShowDate?.ShowDate();
            m_ShowTime?.ShowTime();
            m_CountDigits?.CountDigits();
            m_ShowVersion?.ShowVersion();
        }
    }
}
