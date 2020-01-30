namespace PanelBar
{
    public interface ISortable<T>
    {
        T Sort(T i_Sort);

        T ReverseSort(T i_Sort);
    }
}
