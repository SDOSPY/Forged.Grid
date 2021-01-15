using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public interface IGridColumns<out T> : IEnumerable<T> where T : IGridColumn
    {
    }

    public interface IGridColumnsOf<T> : IList<IGridColumn<T>>, IGridColumns<IGridColumn<T>>
    {
        IGrid<T> Grid { get; set; }

        IGridColumn<T, object> Add();
        IGridColumn<T, TValue> Add<TValue>(Expression<Func<T, TValue>> expression);

        IGridColumn<T, object> Insert(int index);
        IGridColumn<T, TValue> Insert<TValue>(int index, Expression<Func<T, TValue>> expression);
    }
}
