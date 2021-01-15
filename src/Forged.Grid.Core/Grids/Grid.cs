using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Forged.Grid
{
    public class Grid<T> : IGrid<T> where T : class
    {
        public string Url { get; set; }
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? EmptyText { get; set; }

        public IGridSort<T> Sort { get; set; }
        public IQueryable<T> Source { get; set; }
        public IQueryCollection? Query { get; set; }
        public GridProcessingMode Mode { get; set; }
        public ViewContext? ViewContext { get; set; }
        public GridFilterMode FilterMode { get; set; }
        public string FooterPartialViewName { get; set; }
        public GridHtmlAttributes Attributes { get; set; }
        public HashSet<IGridProcessor<T>> Processors { get; set; }

        IGridColumns<IGridColumn> IGrid.Columns => Columns;
        public IGridColumnsOf<T> Columns { get; set; }

        IGridRows<object> IGrid.Rows => Rows;
        public IGridRowsOf<T> Rows { get; set; }

        IGridPager? IGrid.Pager => Pager;
        public IGridPager<T>? Pager { get; set; }

        public Grid(IEnumerable<T> source)
        {
            Url = "";
            Name = "";
            FooterPartialViewName = "";
            Source = source.AsQueryable();
            FilterMode = GridFilterMode.Excel;
            Mode = GridProcessingMode.Automatic;
            Attributes = new GridHtmlAttributes();
            Processors = new HashSet<IGridProcessor<T>>();
            Columns = new GridColumns<T>(this);
            Rows = new GridRows<T>(this);
            Sort = new GridSort<T>(this);
        }
    }
}
