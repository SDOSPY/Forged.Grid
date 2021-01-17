using System;
using System.Linq;
using NSubstitute;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridColumnSortTests
    {
        private GridColumnSort<GridModel, object?> sort;

        public GridColumnSortTests()
        {
            IGrid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());
            GridColumn<GridModel, object?> column = new GridColumn<GridModel, object?>(grid, model => model.Name);

            sort = new GridColumnSort<GridModel, object?>(column) { IsEnabled = true };
        }

        [Fact]
        public void Index_NotSorted_Null()
        {
            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns(((int, GridSortOrder)?)null);

            Assert.Null(sort.Index);
        }

        [Fact]
        public void Index_ReturnsFromGridSort()
        {
            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns((2, GridSortOrder.Desc));

            int? actual = sort.Index;
            int? expected = 2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Order_ReturnsFromGridSort()
        {
            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns((0, GridSortOrder.Desc));

            GridSortOrder? expected = GridSortOrder.Desc;
            GridSortOrder? actual = sort.Order;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumnSort_SetsColumn()
        {
            IGridColumn<GridModel, string?> expected = new GridColumn<GridModel, string?>(sort.Column.Grid, model => model.Name);
            IGridColumn<GridModel, string?> actual = new GridColumnSort<GridModel, string?>(expected).Column;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumnSort_NotMemberExpression_IsNotEnabled()
        {
            IGridColumn<GridModel, string?> column = new GridColumn<GridModel, string?>(sort.Column.Grid, model => model.ToString());

            Assert.False(new GridColumnSort<GridModel, string?>(column).IsEnabled);
        }

        [Fact]
        public void GridColumnSort_MemberExpression_IsEnabledNull()
        {
            IGridColumn<GridModel, string?> column = new GridColumn<GridModel, string?>(sort.Column.Grid, model => model.Name);

            Assert.Null(new GridColumnSort<GridModel, string?>(column).IsEnabled);
        }

        [Fact]
        public void GridColumnSort_SetsFirstOrder()
        {
            GridSortOrder actual = new GridColumnSort<GridModel, object?>(sort.Column).FirstOrder;
            GridSortOrder expected = GridSortOrder.Asc;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void By_ReturnsSameItems()
        {
            IQueryable<GridModel> items = new GridModel[2].AsQueryable();
            sort.IsEnabled = false;

            object expected = items;
            object actual = sort.By(items);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void By_AscendingOrder()
        {
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "b" },
                new GridModel { Name = "a" }
            }.AsQueryable();

            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns((0, GridSortOrder.Asc));

            IQueryable<GridModel> expected = items.OrderBy(sort.Column.Expression);
            IQueryable<GridModel> actual = sort.By(items);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void By_DescendingOrder()
        {
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "a" },
                new GridModel { Name = "b" }
            }.AsQueryable();

            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns((0, GridSortOrder.Desc));

            IQueryable<GridModel> expected = items.OrderByDescending(sort.Column.Expression);
            IQueryable<GridModel> actual = sort.By(items);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenBy_ReturnsSameItems()
        {
            IOrderedQueryable<GridModel> items = new GridModel[2].AsQueryable().OrderBy(item => item.ShortText);
            sort.IsEnabled = false;

            object expected = items;
            object actual = sort.ThenBy(items);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ThenBy_AscendingOrder()
        {
            IOrderedQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "b", ShortText = "c" },
                new GridModel { Name = "a", ShortText = "c" }
            }.AsQueryable().OrderBy(item => item.ShortText);

            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns((1, GridSortOrder.Asc));

            IQueryable<GridModel> expected = items.ThenBy(item => item.Name);
            IQueryable<GridModel> actual = sort.ThenBy(items);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenBy_DescendingOrder()
        {
            IOrderedQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "a", ShortText = "c" },
                new GridModel { Name = "b", ShortText = "c" }
            }.AsQueryable().OrderBy(item => item.ShortText);

            sort.Column.Grid.Sort = Substitute.For<IGridSort<GridModel>>();
            sort.Column.Grid.Sort[sort.Column].Returns((1, GridSortOrder.Desc));

            IQueryable<GridModel> expected = items.ThenByDescending(item => item.Name);
            IQueryable<GridModel> actual = sort.ThenBy(items);

            Assert.Equal(expected, actual);
        }
    }
}
