using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridRowsTests
    {
        [Fact]
        public void GridRows_SetsGrid()
        {
            IGrid<GridModel> expected = new Grid<GridModel>(Array.Empty<GridModel>());
            IGrid<GridModel> actual = new GridRows<GridModel>(expected).Grid;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void GetEnumerator_ManuallyProcessesRows()
        {
            IQueryable<GridModel> items = new[] { new GridModel(), new GridModel() }.AsQueryable();
            IGridProcessor<GridModel> postProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IGridProcessor<GridModel> preProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IQueryable<GridModel> postProcessedItems = new[] { new GridModel() }.AsQueryable();
            IQueryable<GridModel> preProcessedItems = new[] { new GridModel() }.AsQueryable();
            postProcessor.ProcessorType = GridProcessorType.Post;
            preProcessor.ProcessorType = GridProcessorType.Pre;
            Grid<GridModel> grid = new Grid<GridModel>(items);
            grid.Mode = GridProcessingMode.Manual;
            postProcessor.Process(preProcessedItems).Returns(postProcessedItems);
            preProcessor.Process(items).Returns(preProcessedItems);
            grid.Processors.Add(postProcessor);
            grid.Processors.Add(preProcessor);
            IEnumerable<object> actual = new GridRows<GridModel>(grid).ToList().Select(row => row.Model);
            IEnumerable<object> expected = items;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumerator_AutomaticallyProcessesRows()
        {
            IQueryable<GridModel> items = new[] { new GridModel(), new GridModel() }.AsQueryable();
            IGridProcessor<GridModel> postProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IGridProcessor<GridModel> preProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IQueryable<GridModel> postProcessedItems = new[] { new GridModel() }.AsQueryable();
            IQueryable<GridModel> preProcessedItems = new[] { new GridModel() }.AsQueryable();
            postProcessor.ProcessorType = GridProcessorType.Post;
            preProcessor.ProcessorType = GridProcessorType.Pre;
            Grid<GridModel> grid = new Grid<GridModel>(items);
            grid.Mode = GridProcessingMode.Automatic;
            postProcessor.Process(preProcessedItems).Returns(postProcessedItems);
            preProcessor.Process(items).Returns(preProcessedItems);
            grid.Processors.Add(postProcessor);
            grid.Processors.Add(preProcessor);
            IEnumerable<object> actual = new GridRows<GridModel>(grid).ToList().Select(row => row.Model);
            IEnumerable<object> expected = postProcessedItems;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumerator_SetsRowIndexes()
        {
            IQueryable<GridModel> items = new[] { new GridModel(), new GridModel() }.AsQueryable();
            Grid<GridModel> grid = new Grid<GridModel>(items);
            int index = 0;
            GridRows<GridModel> rows = new GridRows<GridModel>(grid);
            Assert.All(rows, row => Assert.Equal(index++, row.Index));
        }

        [Fact]
        public void GetEnumerator_SetsRowAttributes()
        {
            (string key, object value) = new KeyValuePair<string, object>("data-id", "1");
            IQueryable<GridModel> items = new[] { new GridModel(), new GridModel() }.AsQueryable();
            Grid<GridModel> grid = new Grid<GridModel>(items);
            GridRows<GridModel> rows = new GridRows<GridModel>(grid) { Attributes = (_) => new { data_id = "1" } };
            Assert.True(rows.All(row =>
                row.Attributes.Single().Key == key &&
                row.Attributes.Single().Value == value));
        }

        [Fact]
        public void GetEnumerator_CachesRows()
        {
            IQueryable<GridModel> items = new[] { new GridModel(), new GridModel() }.AsQueryable();
            IGridProcessor<GridModel> preProcessor = Substitute.For<IGridProcessor<GridModel>>();
            preProcessor.Process(items).Returns(Array.Empty<GridModel>().AsQueryable());
            preProcessor.ProcessorType = GridProcessorType.Pre;
            Grid<GridModel> grid = new Grid<GridModel>(items);
            GridRows<GridModel> rows = new GridRows<GridModel>(grid);
            IGridRow<GridModel>[] originalRows = rows.ToArray();
            grid.Processors.Add(preProcessor);
            IEnumerable<object> actual = rows.ToList().Select(row => row.Model);
            IEnumerable<object> expected = originalRows.Select(row => row.Model);
            preProcessor.DidNotReceive().Process(Arg.Any<IQueryable<GridModel>>());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumerator_ReturnsSameEnumerable()
        {
            GridModel[] items = { new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(items);
            GridRows<GridModel> rows = new GridRows<GridModel>(grid);
            IEnumerator actual = ((IEnumerable)rows).GetEnumerator();
            IEnumerator expected = rows.GetEnumerator();
            while (expected.MoveNext() | actual.MoveNext())
                Assert.Same(((IGridRow<GridModel>?)expected.Current)?.Model, ((IGridRow<GridModel>?)actual.Current)?.Model);
        }
    }
}
