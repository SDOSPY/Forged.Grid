using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;

namespace Forged.Grid
{
    public interface IGrid
    {
        string Url { get; set; }
        string? Id { get; set; }
        string Name { get; set; }
        string? EmptyText { get; set; }

        IQueryCollection? Query { get; set; }
        GridProcessingMode Mode { get; set; }
        ViewContext? ViewContext { get; set; }
        GridFilterMode FilterMode { get; set; }
        string FooterPartialViewName { get; set; }
        GridHtmlAttributes Attributes { get; set; }

        IGridColumns<IGridColumn> Columns { get; }
        IGridRows<object> Rows { get; }
        IGridPager? Pager { get; }
    }

    public interface IGrid<T> : IGrid
    {
        IGridSort<T> Sort { get; set; }
        IQueryable<T> Source { get; set; }
        HashSet<IGridProcessor<T>> Processors { get; set; }

        new IGridColumnsOf<T> Columns { get; }
        new IGridRowsOf<T> Rows { get; }
        new IGridPager<T>? Pager { get; set; }
    }
}
