namespace PanelBar
{
    public interface ICommand<T>
    {
        T Execute(T i_ExecuteOnMe);

        string Title { get; set; }
    }
}
