using System;
using System.Collections.Generic;
using System.Linq;

namespace Forged.Grid
{
    public class GridPager<T> : IGridPager<T>
    {
        public IGrid<T> Grid { get; set; }

        public virtual int TotalRows
        {
            get;
            set;
        }
        public virtual int TotalPages
        {
            get
            {
                if (TotalRows == 0)
                    return 0;
                if (RowsPerPage == 0)
                    return 1;
                return (int)Math.Ceiling(TotalRows / (double)RowsPerPage);
            }
        }
        public virtual int CurrentPage
        {
            get
            {
                string prefix = string.IsNullOrEmpty(Grid.Name) ? "" : Grid.Name + "-";
                CurrentPageValue = int.TryParse(Grid.Query?[prefix + "page"], out int page) ? page : CurrentPageValue;
                CurrentPageValue = Math.Min(TotalPages, CurrentPageValue);
                CurrentPageValue = Math.Max(1, CurrentPageValue);
                return CurrentPageValue;
            }
            set
            {
                CurrentPageValue = value;
            }
        }
        public virtual int RowsPerPage
        {
            get
            {
                if (ShowPageSizes)
                {
                    string prefix = string.IsNullOrEmpty(Grid.Name) ? "" : Grid.Name + "-";
                    RowsPerPageValue = int.TryParse(Grid.Query?[prefix + "rows"], out int rows) ? rows : RowsPerPageValue;
                    RowsPerPageValue = RowsPerPageValue < 0 ? 0 : RowsPerPageValue;
                    if (PageSizes.Count > 0 && !PageSizes.Keys.Contains(RowsPerPageValue))
                        RowsPerPageValue = PageSizes.Keys.First();
                }
                return RowsPerPageValue;
            }
            set
            {
                RowsPerPageValue = value;
            }
        }
        public virtual int PagesToDisplay
        {
            get;
            set;
        }
        public virtual int FirstDisplayPage
        {
            get
            {
                int middlePage = PagesToDisplay / 2 + PagesToDisplay % 2;
                if (CurrentPage < middlePage)
                    return 1;
                if (TotalPages < CurrentPage + PagesToDisplay - middlePage)
                    return Math.Max(TotalPages - PagesToDisplay + 1, 1);
                return CurrentPage - middlePage + 1;
            }
        }
        public virtual bool ShowPageSizes { get; set; }
        public virtual Dictionary<int, string> PageSizes { get; set; }

        public string CssClasses { get; set; }
        public string PartialViewName { get; set; }
        public GridProcessorType ProcessorType { get; set; }

        private int CurrentPageValue { get; set; }
        private int RowsPerPageValue { get; set; }

        public GridPager(IGrid<T> grid)
        {
            Grid = grid;
            CssClasses = "";
            CurrentPage = 1;
            RowsPerPage = 20;
            PagesToDisplay = 5;
            PartialViewName = "MvcGrid/_Pager";
            ProcessorType = GridProcessorType.Post;
            PageSizes = new Dictionary<int, string> { [10] = "10", [20] = "20", [50] = "50", [100] = "100" };
        }

        public virtual IQueryable<T> Process(IQueryable<T> items)
        {
            TotalRows = items.Count();
            if (RowsPerPage == 0)
                return items;
            if (!GridQuery.IsOrdered(items))
                items = items.OrderBy(_ => 0);
            return items.Skip((CurrentPage - 1) * RowsPerPage).Take(RowsPerPage);
        }
    }
}
