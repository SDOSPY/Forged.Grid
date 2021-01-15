using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public class GridColumns<T> : List<IGridColumn<T>>, IGridColumnsOf<T> where T : class
    {
        public IGrid<T> Grid { get; set; }

        public GridColumns(IGrid<T> grid)
        {
            Grid = grid;
        }

        public virtual IGridColumn<T, object> Add()
        {
            return Add<object>(_ => "");
        }
        public virtual IGridColumn<T, TValue> Add<TValue>(Expression<Func<T, TValue>> expression)
        {
            IGridColumn<T, TValue> column = new GridColumn<T, TValue>(Grid, expression);
            Grid.Processors.Add(column);
            Add(column);
            return column;
        }

        public virtual IGridColumn<T, object> Insert(int index)
        {
            return Insert<object>(index, _ => "");
        }
        public virtual IGridColumn<T, TValue> Insert<TValue>(int index, Expression<Func<T, TValue>> expression)
        {
            IGridColumn<T, TValue> column = new GridColumn<T, TValue>(Grid, expression);
            Grid.Processors.Add(column);
            Insert(index, column);
            return column;
        }
    }
}
