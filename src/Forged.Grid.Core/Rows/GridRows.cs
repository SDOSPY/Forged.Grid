using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Forged.Grid
{
    public class GridRows<T> : IGridRowsOf<T>
    {
        public IGrid<T> Grid { get; set; }

        public Func<T, object>? Attributes { get; set; }

        private IEnumerable<IGridRow<T>>? Cache { get; set; }

        public GridRows(IGrid<T> grid)
        {
            Grid = grid;
        }

        public virtual IEnumerator<IGridRow<T>> GetEnumerator()
        {
            if (Cache == null)
            {
                IQueryable<T> items = Grid.Source;
                if (Grid.Mode == GridProcessingMode.Automatic)
                {
                    foreach (IGridProcessor<T> processor in Grid.Processors.Where(proc => proc.ProcessorType == GridProcessorType.Pre))
                        items = processor.Process(items);
                    foreach (IGridProcessor<T> processor in Grid.Processors.Where(proc => proc.ProcessorType == GridProcessorType.Post))
                        items = processor.Process(items);
                }
                Cache = items
                    .ToList()
                    .Select((model, index) => new GridRow<T>(model, index)
                    {
                        Attributes = new GridHtmlAttributes(Attributes?.Invoke(model))
                    });
            }
            return Cache.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
