namespace PanelBar
{
    public class FilterCommand<T> : Command<T>
    {
        public IFilterable<T> Filterable { get; set; }

        protected override T InternalExecute(T i_Feed)
        {
            return Filterable.Filter(i_Feed);
        }
    }
}
