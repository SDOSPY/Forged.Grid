using Microsoft.AspNetCore.Html;
using NSubstitute;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridColumnTests
    {
        private GridColumn<GridModel, object?> column;

        public GridColumnTests()
        {
            IGrid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());
            column = new GridColumn<GridModel, object?>(grid, model => model.Name);
        }

        [Fact]
        public void IGridColumn_ReturnsSort()
        {
            IGridColumn gridColumn = column;

            object actual = gridColumn.Sort;
            object expected = column.Sort;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void IGridColumnOfT_ReturnsSort()
        {
            IGridColumn<GridModel> gridColumn = column;

            object actual = gridColumn.Sort;
            object expected = column.Sort;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void IGridColumn_ReturnsFilter()
        {
            IGridColumn gridColumn = column;

            object actual = gridColumn.Filter;
            object expected = column.Filter;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsGrid()
        {
            object actual = new GridColumn<GridModel, int>(column.Grid, _ => 0).Grid;
            object expected = column.Grid;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsIsEncoded()
        {
            Assert.True(new GridColumn<GridModel, int>(column.Grid, _ => 1).IsEncoded);
        }

        [Fact]
        public void GridColumn_SetsExpression()
        {
            object actual = new GridColumn<GridModel, object?>(column.Grid, column.Expression).Expression;
            object expected = column.Expression;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumn_NotMemberExpression_SetsEmptyTitle()
        {
            Assert.Empty(new GridColumn<GridModel, int>(column.Grid, _ => 1).Title.ToString());
        }

        [Fact]
        public void GridColumn_NoDisplayAttribute_SetsEmptyTitle()
        {
            Assert.Empty(new GridColumn<GridModel, object?>(column.Grid, model => model.Name).Title.ToString());
        }

        [Fact]
        public void GridColumn_DisplayAttribute_SetsTitleFromDisplayName()
        {
            DisplayAttribute? display = typeof(GridModel).GetProperty(nameof(GridModel.Text))?.GetCustomAttribute<DisplayAttribute>();
            column = new GridColumn<GridModel, object?>(column.Grid, model => model.Text);

            string? actual = column.Title.ToString();
            string? expected = display?.GetName();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_DisplayAttribute_SetsTitleFromDisplayShortName()
        {
            DisplayAttribute? display = typeof(GridModel).GetProperty(nameof(GridModel.ShortText))?.GetCustomAttribute<DisplayAttribute>();
            column = new GridColumn<GridModel, object?>(column.Grid, model => model.ShortText);

            string? expected = display?.GetShortName();
            string? actual = column.Title.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsExpressionValue()
        {
            GridModel model = new GridModel { Name = "Testing name" };

            object? actual = column.ExpressionValue(model);
            object expected = "Testing name";

            Assert.Same(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsPreProcessorType()
        {
            GridProcessorType actual = new GridColumn<GridModel, object>(column.Grid, _ => 0).ProcessorType;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsDefaultSort()
        {
            column = new GridColumn<GridModel, object?>(column.Grid, model => model.Name);

            IGridColumnSort<GridModel, object?> actual = column.Sort;

            Assert.Equal(GridSortOrder.Asc, actual.FirstOrder);
            Assert.Same(column, actual.Column);
            Assert.Null(actual.IsEnabled);
            Assert.Null(actual.Order);
        }

        [Fact]
        public void GridColumn_SetsNameFromExpression()
        {
            Expression<Func<GridModel, bool?>> expression = (model) => model.NIsChecked;

            string actual = new GridColumn<GridModel, bool?>(column.Grid, expression).Name;
            string expected = "NIsChecked";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridColumn_SetsDefaultFilter()
        {
            column = new GridColumn<GridModel, object?>(column.Grid, model => model.Name);

            IGridColumnFilter<GridModel, object?> actual = column.Filter;

            Assert.Equal("default", actual.Name);
            Assert.Equal(column, actual.Column);
            Assert.Null(actual.IsEnabled);
            Assert.Null(actual.Operator);
            Assert.Null(actual.Second);
            Assert.Null(actual.First);
            Assert.Null(actual.Type);
        }

        [Fact]
        public void Process_FiltereItems()
        {
            column.Filter = Substitute.For<IGridColumnFilter<GridModel, object?>>();

            IQueryable<GridModel> filtered = new GridModel[2].AsQueryable();
            IQueryable<GridModel> items = new GridModel[2].AsQueryable();

            column.Filter.Apply(items).Returns(filtered);

            object actual = column.Process(items);
            object expected = filtered;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ValueFor_NullReferenceInExpressionValue_ReturnsEmpty()
        {
            column.ExpressionValue = (model) => model.Name;

            string? actual = column.ValueFor(new GridRow<object>(new GridModel(), 0)).ToString();

            Assert.Empty(actual);
        }

        [Fact]
        public void ValueFor_NullReferenceInRenderValue_ReturnsEmpty()
        {
            column.RenderValue = (model, _) => model.Name;

            string? actual = column.ValueFor(new GridRow<object>(new GridModel(), 0)).ToString();

            Assert.Empty(actual);
        }

        [Fact]
        public void ValueFor_NullRelationReference_ReturnsEmpty()
        {
            column.ExpressionValue = (model) => model.Child!.Name;

            string? actual = column.ValueFor(new GridRow<object>(new GridModel(), 0)).ToString();

            Assert.Empty(actual);
        }

        [Fact]
        public void ValueFor_ExpressionValue_ThrowsNotNullReferenceException()
        {
            column.ExpressionValue = (_) => int.Parse("Zero");

            Assert.Throws<FormatException>(() => column.ValueFor(new GridRow<object>(new GridModel(), 0)));
        }

        [Fact]
        public void ValueFor_RenderValue_ThrowsNotNullReferenceException()
        {
            column.RenderValue = (_, __) => int.Parse("Zero");

            Assert.Throws<FormatException>(() => column.ValueFor(new GridRow<object>(new GridModel(), 0)));
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, true, "<name>")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", "For <{0}>", true, "<name>")]
        [InlineData("<name>", "For <{0}>", false, "<name>")]
        public void ValueFor_RenderValue_Html(string value, string format, bool isEncoded, string renderedValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Content = value == null ? null : new HtmlString(value) }, 0);
            column.RenderValue = (model, _) => model.Content;
            column.ExpressionValue = (_) => "";
            column.IsEncoded = isEncoded;
            column.Format = format;

            string? actual = column.ValueFor(row).ToString();
            string? expected = renderedValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValueFor_RenderValue_Index()
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Name = "Test" }, 33);
            column.RenderValue = (model, i) => model.Name + " " + i;
            column.ExpressionValue = (_) => "";

            string? actual = column.ValueFor(row).ToString();
            string? expected = "Test 33";

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, true, "<name>")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", "For <{0}>", true, "<name>")]
        [InlineData("<name>", "For <{0}>", false, "<name>")]
        public void ValueFor_ExpressionValue_Html(string value, string format, bool isEncoded, string expressionValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Content = value == null ? null : new HtmlString(value) }, 0);
            column = new GridColumn<GridModel, object?>(column.Grid, model => model.Content);
            column.IsEncoded = isEncoded;
            column.Format = format;

            string? actual = column.ValueFor(row).ToString();
            string? expected = expressionValue;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", null, true, "&lt;name&gt;")]
        [InlineData("<name>", "For <{0}>", false, "For <<name>>")]
        [InlineData("<name>", "For <{0}>", true, "For &lt;&lt;name&gt;&gt;")]
        public void ValueFor_RenderValue(string value, string format, bool isEncoded, string renderedValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Name = value }, 33);
            TextWriter writer = new StringWriter();

            column.RenderValue = (model, _) => model.Name;
            column.ExpressionValue = (_) => "";
            column.IsEncoded = isEncoded;
            column.Format = format;

            column.ValueFor(row).WriteTo(writer, HtmlEncoder.Default);

            string? actual = writer.ToString();
            string? expected = renderedValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValueFor_BadValue_EnumExpressionValue()
        {
            GridColumn<GridModel, TestEnum> enumColumn = new GridColumn<GridModel, TestEnum>(column.Grid, model => model.Enum);
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Enum = (TestEnum)2 }, 0);

            string? actual = enumColumn.ValueFor(row).ToString();
            string? expected = "2";

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TestEnum.First, "1st")]
        [InlineData(TestEnum.Second, "2nd")]
        public void ValueFor_NullableEnumExpressionValue(TestEnum value, string expressionValue)
        {
            GridColumn<GridModel, TestEnum?> enumColumn = new GridColumn<GridModel, TestEnum?>(column.Grid, model => model.Enum);
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Enum = value }, 0);

            string? actual = enumColumn.ValueFor(row).ToString();
            string? expected = expressionValue;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(TestEnum.First, "1st")]
        [InlineData(TestEnum.Second, "2nd")]
        public void ValueFor_EnumExpressionValue(TestEnum value, string expressionValue)
        {
            GridColumn<GridModel, TestEnum> enumColumn = new GridColumn<GridModel, TestEnum>(column.Grid, model => model.Enum);
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Enum = value }, 0);

            string? actual = enumColumn.ValueFor(row).ToString();
            string? expected = expressionValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValueFor_NullEnum()
        {
            GridColumn<GridModel, TestEnum?> enumColumn = new GridColumn<GridModel, TestEnum?>(column.Grid, model => model.NEnum);
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel(), 0);

            Assert.Empty(enumColumn.ValueFor(row).ToString());
        }

        [Theory]
        [InlineData(null, "For {0}", true, "")]
        [InlineData(null, "For {0}", false, "")]
        [InlineData("<name>", null, false, "<name>")]
        [InlineData("<name>", null, true, "&lt;name&gt;")]
        [InlineData("<name>", "For <{0}>", false, "For <<name>>")]
        [InlineData("<name>", "For <{0}>", true, "For &lt;&lt;name&gt;&gt;")]
        public void ValueFor_ExpressionValue(string value, string format, bool isEncoded, string expressionValue)
        {
            IGridRow<GridModel> row = new GridRow<GridModel>(new GridModel { Name = value }, 0);
            TextWriter writer = new StringWriter();

            column.IsEncoded = isEncoded;
            column.Format = format;

            column.ValueFor(row).WriteTo(writer, HtmlEncoder.Default);

            string? expected = expressionValue;
            string? actual = writer.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
