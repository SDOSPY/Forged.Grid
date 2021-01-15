using System;
using System.Linq;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridSortTests
    {
        private readonly GridSort<GridModel> sort;
        private readonly IGridColumn<GridModel, int> sumColumn;
        private readonly IGridColumn<GridModel, string?> nameColumn;
        private readonly IGridColumn<GridModel, string?> textColumn;

        public GridSortTests()
        {
            IGrid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());
            textColumn = grid.Columns.Add(model => model.Text);
            nameColumn = grid.Columns.Add(model => model.Name);
            sumColumn = grid.Columns.Add(model => model.Sum);
            sort = new GridSort<GridModel>(grid);
        }

        [Fact]
        public void GridSort_SetsGrid()
        {
            object expected = sort.Grid;
            object actual = new GridSort<GridModel>(sort.Grid).Grid;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridSort_SetsProcessorType()
        {
            GridProcessorType expected = new GridSort<GridModel>(sort.Grid).ProcessorType;
            GridProcessorType actual = GridProcessorType.Pre;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Indexer_DisabledSort_ReturnsNull()
        {
            sumColumn.Name = "sum";
            sumColumn.Sort.IsEnabled = false;
            sumColumn.Grid.Query = HttpUtility.ParseQueryString("sort=sum asc");
            Assert.Null(sort[sumColumn]);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "sort=")]
        [InlineData("", "sort=asc")]
        [InlineData("", "sort=sum as")]
        [InlineData("", "sort=sumasc")]
        [InlineData("", "sort=sum-asc")]
        [InlineData("", "sort=name asc")]
        [InlineData("", "sort=grid-sum-asc")]
        [InlineData(null, "")]
        [InlineData(null, "sort=")]
        [InlineData(null, "sort=asc")]
        [InlineData(null, "sort=sum as")]
        [InlineData(null, "sort=sumasc")]
        [InlineData(null, "sort=sum-asc")]
        [InlineData(null, "sort=name asc")]
        [InlineData(null, "sort=grid-sum-asc")]
        [InlineData("grid", "")]
        [InlineData("grid", "grid-sort=")]
        [InlineData("grid", "grid-sort=asc")]
        [InlineData("grid", "grid-sort=sum as")]
        [InlineData("grid", "grid-sort=sumasc")]
        [InlineData("grid", "grid-sort=sum-asc")]
        [InlineData("grid", "grid-sort=name asc")]
        [InlineData("grid", "grid-sort=grid-sum-asc")]
        public void Indexer_NotSorted(string gridName, string query)
        {
            sumColumn.Name = "sum";
            sumColumn.Grid.Name = gridName;
            sumColumn.Sort.IsEnabled = true;
            sumColumn.Grid.Query = HttpUtility.ParseQueryString(query);
            Assert.Null(sort[sumColumn]);
        }

        [Theory]
        [InlineData("", "sort=sum asc", 0)]
        [InlineData("", "SORT=SUM ASC", 0)]
        [InlineData("", "sort=sum asc,sum desc", 0)]
        [InlineData("", "SORT=SURNAME ASC,SUM ASC", 0)]
        [InlineData("", "sort=name asc,sum asc", 1)]
        [InlineData("", "SORT=NAME ASC,SUM ASC", 1)]
        [InlineData("", "SORT=NAME ASC,SURNAME ASC,SUM ASC", 1)]
        [InlineData(null, "sort=sum asc", 0)]
        [InlineData(null, "SORT=SUM ASC", 0)]
        [InlineData(null, "sort=sum asc,sum desc", 0)]
        [InlineData(null, "SORT=SURNAME ASC,SUM ASC", 0)]
        [InlineData(null, "sort=name asc,sum asc", 1)]
        [InlineData(null, "SORT=NAME ASC,SUM ASC", 1)]
        [InlineData(null, "SORT=SURNAME ASC,NAME ASC,SUM ASC", 1)]
        [InlineData("grid", "grid-sort=sum asc", 0)]
        [InlineData("grid", "GRID-SORT=SUM ASC", 0)]
        [InlineData("grid", "grid-sort=sum asc,sum desc", 0)]
        [InlineData("grid", "GRID-SORT=SURNAME ASC,SUM ASC", 0)]
        [InlineData("grid", "grid-sort=name asc,sum asc", 1)]
        [InlineData("grid", "GRID-SORT=NAME ASC,SUM ASC", 1)]
        [InlineData("grid", "GRID-SORT=SURNAME ASC,NAME ASC,SUM ASC", 1)]
        public void Indexer_SortIndex(string gridName, string query, int index)
        {
            sumColumn.Name = "sum";
            sumColumn.Grid.Name = gridName;
            sumColumn.Sort.IsEnabled = true;
            sumColumn.Grid.Query = HttpUtility.ParseQueryString(query);
            int? expected = index;
            int? actual = sort[sumColumn]?.Index;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "sort=sum asc", GridSortOrder.Asc)]
        [InlineData("", "SORT=SUM ASC", GridSortOrder.Asc)]
        [InlineData("", "sort=sum desc", GridSortOrder.Desc)]
        [InlineData("", "SORT=SUM DESC", GridSortOrder.Desc)]
        [InlineData("", "SORT=SUM DESC,SUM ASC", GridSortOrder.Desc)]
        [InlineData(null, "sort=sum asc", GridSortOrder.Asc)]
        [InlineData(null, "SORT=SUM ASC", GridSortOrder.Asc)]
        [InlineData(null, "sort=sum desc", GridSortOrder.Desc)]
        [InlineData(null, "SORT=SUM DESC", GridSortOrder.Desc)]
        [InlineData(null, "SORT=SUM DESC,SUM ASC", GridSortOrder.Desc)]
        [InlineData("grid", "grid-sort=sum asc", GridSortOrder.Asc)]
        [InlineData("grid", "GRID-SORT=SUM ASC", GridSortOrder.Asc)]
        [InlineData("grid", "grid-sort=sum desc", GridSortOrder.Desc)]
        [InlineData("grid", "GRID-SORT=SUM DESC", GridSortOrder.Desc)]
        [InlineData("grid", "GRID-SORT=SUM DESC, SUM ASC", GridSortOrder.Desc)]
        public void Indexer_SortOrder(string gridName, string query, GridSortOrder order)
        {
            sumColumn.Name = "sum";
            sumColumn.Grid.Name = gridName;
            sumColumn.Sort.IsEnabled = true;
            sumColumn.Grid.Query = HttpUtility.ParseQueryString(query);
            GridSortOrder? actual = sort[sumColumn]?.Order;
            GridSortOrder? expected = order;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Indexer_SortAvailableAfterSettingQuery()
        {
            sumColumn.Name = "sum";
            sumColumn.Grid.Query = null;
            sumColumn.Sort.IsEnabled = true;
            Assert.Null(sort[sumColumn]);
            sumColumn.Grid.Query = HttpUtility.ParseQueryString("sort=sum asc");
            GridSortOrder? expected = GridSortOrder.Asc;
            GridSortOrder? actual = sort[sumColumn]?.Order;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Process_SortsEnabledColumns()
        {
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "a", Text = "a", Sum = 20 },
                new GridModel { Name = "b", Text = "b", Sum = 10 },
                new GridModel { Name = "a", Text = "c", Sum = 10 }
            }.AsQueryable();
            sumColumn.Sort.IsEnabled = true;
            nameColumn.Sort.IsEnabled = true;
            textColumn.Sort.IsEnabled = false;
            sort.Grid.Query = HttpUtility.ParseQueryString("sort=text asc,name asc,sum asc");
            IQueryable<GridModel> expected = items.OrderBy(item => item.Name).ThenBy(item => item.Sum);
            IQueryable<GridModel> actual = sort.Process(items);
            Assert.Equal(expected, actual);
        }
    }
}
