namespace PanelBar
{
    public class SortCommand<T> : Command<T>
    {
        public ISortable<T> Sortable { get; set; }

        private bool m_state;

        public SortCommand()
        {
            m_state = false;
        }

        protected override T InternalExecute(T i_Feed)
        {
            m_state = !m_state;
            return m_state ? Sortable.Sort(i_Feed) : Sortable.ReverseSort(i_Feed);
        }
    }
}
