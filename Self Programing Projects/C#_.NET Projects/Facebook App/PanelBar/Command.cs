namespace PanelBar
{
    public abstract class Command<T> : ICommand<T>
    {
        public string Title { get; set; }

        public T Execute(T i_ExecuteOnMe)
        {
            return InternalExecute(i_ExecuteOnMe);
        }

        protected abstract T InternalExecute(T i_Feed);
    }
}
