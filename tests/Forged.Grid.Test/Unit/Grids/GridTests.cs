using System;
using System.Linq;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridTests
    {
        [Fact]
        public void IGrid_ReturnsColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());

            object actual = ((IGrid)grid).Columns;
            object expected = grid.Columns;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void IGrid_ReturnsRows()
        {
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());

            object actual = ((IGrid)grid).Rows;
            object expected = grid.Rows;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void IGrid_ReturnsPager()
        {
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());

            object? actual = ((IGrid)grid).Pager;
            object? expected = grid.Pager;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Grid_SetsName()
        {
            Assert.Empty(new Grid<GridModel>(Array.Empty<GridModel>()).Name);
        }

        [Fact]
        public void Grid_SetsUrl()
        {
            Assert.Empty(new Grid<GridModel>(Array.Empty<GridModel>()).Url);
        }

        [Fact]
        public void Grid_SetsFooterPartialViewName()
        {
            Assert.Empty(new Grid<GridModel>(Array.Empty<GridModel>()).FooterPartialViewName);
        }

        [Fact]
        public void Grid_SetsProcessors()
        {
            Assert.Empty(new Grid<GridModel>(Array.Empty<GridModel>()).Processors);
        }

        [Fact]
        public void Grid_SetsSource()
        {
            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = new Grid<GridModel>(expected).Source;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Grid_SetsFilterMode()
        {
            GridFilterMode actual = new Grid<GridModel>(Array.Empty<GridModel>()).FilterMode;
            GridFilterMode expected = GridFilterMode.Excel;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Grid_SetsEmptyAttributes()
        {
            Assert.Empty(new Grid<GridModel>(Array.Empty<GridModel>()).Attributes);
        }

        [Fact]
        public void Grid_SetsColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());

            IGridColumnsOf<GridModel> expected = new GridColumns<GridModel>(grid);
            IGridColumnsOf<GridModel> actual = grid.Columns;

            Assert.Same(expected.Grid, actual.Grid);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Grid_SetsMode()
        {
            GridProcessingMode actual = new Grid<GridModel>(Array.Empty<GridModel>()).Mode;
            GridProcessingMode expected = GridProcessingMode.Automatic;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Grid_SetsRows()
        {
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());

            IGridRowsOf<GridModel> expected = new GridRows<GridModel>(grid);
            IGridRowsOf<GridModel> actual = grid.Rows;

            Assert.Same(expected.Grid, actual.Grid);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Grid_SetsSort()
        {
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());

            IGridSort<GridModel> expected = new GridSort<GridModel>(grid);
            IGridSort<GridModel> actual = grid.Sort;

            Assert.Same(expected.Grid, actual.Grid);
        }
    }
}
