using Microsoft.AspNetCore.Html;
using System;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public interface IGridColumn
    {
        string Name { get; set; }
        object Title { get; set; }
        string? Format { get; set; }
        bool IsHidden { get; set; }
        string CssClasses { get; set; }
        bool IsEncoded { get; set; }

        IGridColumnSort Sort { get; }
        IGridColumnFilter Filter { get; }

        IHtmlContent ValueFor(IGridRow<object> row);
    }
    public interface IGridColumn<T> : IGridColumn
    {
        IGrid<T> Grid { get; }

        new IGridColumnSort<T> Sort { get; }
    }
    public interface IGridColumn<T, TValue> : IGridProcessor<T>, IGridColumn<T>
    {
        Func<T, int, object?>? RenderValue { get; set; }
        Expression<Func<T, TValue>> Expression { get; set; }

        new IGridColumnSort<T, TValue> Sort { get; set; }
        new IGridColumnFilter<T, TValue> Filter { get; set; }
    }
}
