using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace Forged.Grid
{
    public class GridSort<T> : IGridSort<T>
    {
        public IGrid<T> Grid { get; set; }

        public GridProcessorType ProcessorType { get; set; }

        private OrderedDictionary Definitions
        {
            get
            {
                if (!DefinitionsIsSet && Grid.Query != null)
                {
                    string prefix = string.IsNullOrEmpty(Grid.Name) ? "" : Grid.Name + "-";
                    MatchCollection matches = Regex.Matches(Grid.Query[prefix + "sort"].ToString(),
                        "(^|,)(?<name>.*?) (?<order>asc|desc)(?=($|,))", RegexOptions.IgnoreCase);
                    foreach (Match match in matches)
                        foreach (IGridColumn<T> column in Grid.Columns)
                            if (match.Groups["name"].Value.Equals(column.Name, StringComparison.OrdinalIgnoreCase) && !DefinitionsValue.Contains(column))
                            {
                                if (match.Groups["order"].Value.Equals("desc", StringComparison.OrdinalIgnoreCase))
                                    DefinitionsValue.Add(column, (DefinitionsValue.Count, GridSortOrder.Desc));
                                else
                                    DefinitionsValue.Add(column, (DefinitionsValue.Count, GridSortOrder.Asc));

                                break;
                            }
                    DefinitionsIsSet = true;
                }
                return DefinitionsValue;
            }
        }
        private bool DefinitionsIsSet { get; set; }
        private OrderedDictionary DefinitionsValue { get; }

        public GridSort(IGrid<T> grid)
        {
            Grid = grid;
            ProcessorType = GridProcessorType.Pre;
            DefinitionsValue = new OrderedDictionary();
        }

        public (int Index, GridSortOrder Order)? this[IGridColumn<T> column]
        {
            get
            {
                if (column.Sort.IsEnabled == true && Definitions.Contains(column))
                    return ((int, GridSortOrder))Definitions[column];
                return null;
            }
        }

        public IQueryable<T> Process(IQueryable<T> items)
        {
            int index = 0;
            foreach (IGridColumn<T> column in Definitions.Keys.Cast<IGridColumn<T>>().Where(column => column.Sort.IsEnabled == true))
                items = index++ == 0 ? column.Sort.By(items) : column.Sort.ThenBy((IOrderedQueryable<T>)items);
            return items;
        }
    }
}
