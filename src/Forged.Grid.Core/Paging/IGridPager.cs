using System.Collections.Generic;

namespace Forged.Grid
{
    public interface IGridPager
    {
        int TotalPages { get; }
        int TotalRows { get; set; }
        int CurrentPage { get; set; }
        int RowsPerPage { get; set; }
        int FirstDisplayPage { get; }
        int PagesToDisplay { get; set; }
        bool ShowPageSizes { get; set; }
        Dictionary<int, string> PageSizes { get; set; }

        string CssClasses { get; set; }
        string PartialViewName { get; set; }
    }

    public interface IGridPager<T> : IGridProcessor<T>, IGridPager
    {
        IGrid<T> Grid { get; }
    }
}
