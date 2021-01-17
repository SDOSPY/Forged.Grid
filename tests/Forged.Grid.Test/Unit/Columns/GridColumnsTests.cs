using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridColumnsTests
    {
        private GridColumns<GridModel> columns;

        public GridColumnsTests()
        {
            columns = new GridColumns<GridModel>(new Grid<GridModel>(Array.Empty<GridModel>()));
        }

        [Fact]
        public void GridColumns_SetsGrid()
        {
            object actual = new GridColumns<GridModel>(columns.Grid).Grid;
            object expected = columns.Grid;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Add_GridColumn()
        {
            IGridColumn<GridModel, object> expected = new GridColumn<GridModel, object>(columns.Grid, _ => "");
            IGridColumn<GridModel, object> actual = columns.Add();

            Assert.Equal("", actual.Expression.Compile().Invoke(new GridModel()));
            Assert.Equal(expected.Filter.IsEnabled, actual.Filter.IsEnabled);
            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.Sort.IsEnabled, actual.Sort.IsEnabled);
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.Filter.Type, actual.Filter.Type);
            Assert.Equal(expected.Filter.Name, actual.Filter.Name);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.Sort.Order, actual.Sort.Order);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Add_Expression_GridColumn()
        {
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;

            IGridColumn<GridModel, string?> expected = new GridColumn<GridModel, string?>(columns.Grid, expression);
            IGridColumn<GridModel, string?> actual = columns.Add(expression);

            Assert.Equal(expected.Filter.IsEnabled, actual.Filter.IsEnabled);
            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.Sort.IsEnabled, actual.Sort.IsEnabled);
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.Filter.Type, actual.Filter.Type);
            Assert.Equal(expected.Filter.Name, actual.Filter.Name);
            Assert.Equal(expected.Expression, actual.Expression);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.Sort.Order, actual.Sort.Order);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Add_GridColumnProcessor()
        {
            columns.Add(model => model.Name);

            object expected = columns.Single();
            object actual = columns.Grid.Processors.Single();

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Add_ReturnsAddedColumn()
        {
            object actual = columns.Add(model => model.Name);
            object expected = columns.Single();

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Insert_GridColumn()
        {
            columns.Add(_ => 0);

            IGridColumn<GridModel, int> expected = new GridColumn<GridModel, int>(columns.Grid, _ => 1);
            IGridColumn<GridModel, object> actual = columns.Insert(0);

            Assert.Equal("", actual.Expression.Compile().Invoke(new GridModel()));
            Assert.Equal(expected.Filter.IsEnabled, actual.Filter.IsEnabled);
            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.Sort.IsEnabled, actual.Sort.IsEnabled);
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.Filter.Type, actual.Filter.Type);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.Sort.Order, actual.Sort.Order);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal("default", actual.Filter.Name);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Insert_Expression_GridColumn()
        {
            Expression<Func<GridModel, int>> expression = (model) => model.Sum;
            columns.Add(model => model.Name);

            IGridColumn<GridModel, int> expected = new GridColumn<GridModel, int>(columns.Grid, expression);
            IGridColumn<GridModel, int> actual = columns.Insert(0, expression);

            Assert.Equal(expected.Filter.IsEnabled, actual.Filter.IsEnabled);
            Assert.Equal(expected.Title.ToString(), actual.Title.ToString());
            Assert.Equal(expected.Sort.IsEnabled, actual.Sort.IsEnabled);
            Assert.Equal(expected.ProcessorType, actual.ProcessorType);
            Assert.Equal(expected.Filter.Type, actual.Filter.Type);
            Assert.Equal(expected.Filter.Name, actual.Filter.Name);
            Assert.Equal(expected.Expression, actual.Expression);
            Assert.Equal(expected.CssClasses, actual.CssClasses);
            Assert.Equal(expected.Sort.Order, actual.Sort.Order);
            Assert.Equal(expected.IsEncoded, actual.IsEncoded);
            Assert.Equal(expected.Format, actual.Format);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Grid, actual.Grid);
        }

        [Fact]
        public void Insert_GridColumnProcessor()
        {
            columns.Insert(0, model => model.Name);

            object actual = columns.Grid.Processors.Single();
            object expected = columns.Single();

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Insert_ReturnsInsertedColumn()
        {
            object actual = columns.Insert(0, model => model.Name);
            object expected = columns.Single();

            Assert.Same(expected, actual);
        }
    }
}
