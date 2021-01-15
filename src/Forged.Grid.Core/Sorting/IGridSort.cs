namespace Forged.Grid
{
    public interface IGridSort<T> : IGridProcessor<T>
    {
        IGrid<T> Grid { get; set; }

        (int Index, GridSortOrder Order)? this[IGridColumn<T> column] { get; }
    }
}
