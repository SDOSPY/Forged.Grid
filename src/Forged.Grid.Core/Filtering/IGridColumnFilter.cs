using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;

namespace Forged.Grid
{
    public interface IGridColumnFilter
    {
        string Name { get; set; }
        bool? IsEnabled { get; set; }
        string DefaultMethod { get; set; }
        GridFilterCase? Case { get; set; }
        GridFilterType? Type { get; set; }
        IEnumerable<SelectListItem> Options { get; set; }

        string? Operator { get; set; }
        IGridFilter? First { get; set; }
        IGridFilter? Second { get; set; }
    }
    public interface IGridColumnFilter<T, TValue> : IGridColumnFilter
    {
        IGridColumn<T, TValue> Column { get; set; }

        IQueryable<T> Apply(IQueryable<T> items);
    }
}
