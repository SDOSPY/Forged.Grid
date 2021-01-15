using System.Linq;

namespace Forged.Grid
{
    public interface IGridColumnSort
    {
        int? Index { get; }
        GridSortOrder? Order { get; }
        bool? IsEnabled { get; set; }
        GridSortOrder FirstOrder { get; set; }
    }
    public interface IGridColumnSort<T> : IGridColumnSort
    {
        IQueryable<T> By(IQueryable<T> items);
        IQueryable<T> ThenBy(IOrderedQueryable<T> items);
    }
    public interface IGridColumnSort<T, TValue> : IGridColumnSort<T>
    {
        IGridColumn<T, TValue> Column { get; set; }
    }
}
