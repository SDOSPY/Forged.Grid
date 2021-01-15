using System;
using System.Collections.Generic;

namespace Forged.Grid
{
    public interface IGridRows<out T> : IEnumerable<IGridRow<T>>
    {
    }

    public interface IGridRowsOf<T> : IGridRows<T>
    {
        IGrid<T> Grid { get; }

        Func<T, object>? Attributes { get; set; }
    }
}
