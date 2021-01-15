using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridFiltersTests
    {
        private readonly GridFilters filters;
        private readonly IGridColumn<GridModel, string?> column;

        public GridFiltersTests()
        {
            filters = new GridFilters();
            Grid<GridModel> grid = new Grid<GridModel>(Array.Empty<GridModel>());
            column = new GridColumn<GridModel, string?>(grid, model => model.Name);
        }

        [Fact]
        public void GridFilters_SetEmptyBooleanText()
        {
            Assert.Empty(new GridFilters().BooleanEmptyOptionText());
        }

        [Fact]
        public void GridFilters_SetFalseBooleanText()
        {
            Assert.Equal("False", new GridFilters().BooleanFalseOptionText());
        }

        [Fact]
        public void GridFilters_SetTrueBooleanText()
        {
            Assert.Equal("True", new GridFilters().BooleanTrueOptionText());
        }

        [Theory]
        [InlineData(typeof(sbyte), "equals", typeof(NumberFilter<sbyte>))]
        [InlineData(typeof(sbyte), "not-equals", typeof(NumberFilter<sbyte>))]
        [InlineData(typeof(sbyte), "less-than", typeof(NumberFilter<sbyte>))]
        [InlineData(typeof(sbyte), "greater-than", typeof(NumberFilter<sbyte>))]
        [InlineData(typeof(sbyte), "less-than-or-equal", typeof(NumberFilter<sbyte>))]
        [InlineData(typeof(sbyte), "greater-than-or-equal", typeof(NumberFilter<sbyte>))]

        [InlineData(typeof(byte), "equals", typeof(NumberFilter<byte>))]
        [InlineData(typeof(byte), "not-equals", typeof(NumberFilter<byte>))]
        [InlineData(typeof(byte), "less-than", typeof(NumberFilter<byte>))]
        [InlineData(typeof(byte), "greater-than", typeof(NumberFilter<byte>))]
        [InlineData(typeof(byte), "less-than-or-equal", typeof(NumberFilter<byte>))]
        [InlineData(typeof(byte), "greater-than-or-equal", typeof(NumberFilter<byte>))]

        [InlineData(typeof(short), "equals", typeof(NumberFilter<short>))]
        [InlineData(typeof(short), "not-equals", typeof(NumberFilter<short>))]
        [InlineData(typeof(short), "less-than", typeof(NumberFilter<short>))]
        [InlineData(typeof(short), "greater-than", typeof(NumberFilter<short>))]
        [InlineData(typeof(short), "less-than-or-equal", typeof(NumberFilter<short>))]
        [InlineData(typeof(short), "greater-than-or-equal", typeof(NumberFilter<short>))]

        [InlineData(typeof(ushort), "equals", typeof(NumberFilter<ushort>))]
        [InlineData(typeof(ushort), "not-equals", typeof(NumberFilter<ushort>))]
        [InlineData(typeof(ushort), "less-than", typeof(NumberFilter<ushort>))]
        [InlineData(typeof(ushort), "greater-than", typeof(NumberFilter<ushort>))]
        [InlineData(typeof(ushort), "less-than-or-equal", typeof(NumberFilter<ushort>))]
        [InlineData(typeof(ushort), "greater-than-or-equal", typeof(NumberFilter<ushort>))]

        [InlineData(typeof(int), "equals", typeof(NumberFilter<int>))]
        [InlineData(typeof(int), "not-equals", typeof(NumberFilter<int>))]
        [InlineData(typeof(int), "less-than", typeof(NumberFilter<int>))]
        [InlineData(typeof(int), "greater-than", typeof(NumberFilter<int>))]
        [InlineData(typeof(int), "less-than-or-equal", typeof(NumberFilter<int>))]
        [InlineData(typeof(int), "greater-than-or-equal", typeof(NumberFilter<int>))]

        [InlineData(typeof(uint), "equals", typeof(NumberFilter<uint>))]
        [InlineData(typeof(uint), "not-equals", typeof(NumberFilter<uint>))]
        [InlineData(typeof(uint), "less-than", typeof(NumberFilter<uint>))]
        [InlineData(typeof(uint), "greater-than", typeof(NumberFilter<uint>))]
        [InlineData(typeof(uint), "less-than-or-equal", typeof(NumberFilter<uint>))]
        [InlineData(typeof(uint), "greater-than-or-equal", typeof(NumberFilter<uint>))]

        [InlineData(typeof(long), "equals", typeof(NumberFilter<long>))]
        [InlineData(typeof(long), "not-equals", typeof(NumberFilter<long>))]
        [InlineData(typeof(long), "less-than", typeof(NumberFilter<long>))]
        [InlineData(typeof(long), "greater-than", typeof(NumberFilter<long>))]
        [InlineData(typeof(long), "less-than-or-equal", typeof(NumberFilter<long>))]
        [InlineData(typeof(long), "greater-than-or-equal", typeof(NumberFilter<long>))]

        [InlineData(typeof(ulong), "equals", typeof(NumberFilter<ulong>))]
        [InlineData(typeof(ulong), "not-equals", typeof(NumberFilter<ulong>))]
        [InlineData(typeof(ulong), "less-than", typeof(NumberFilter<ulong>))]
        [InlineData(typeof(ulong), "greater-than", typeof(NumberFilter<ulong>))]
        [InlineData(typeof(ulong), "less-than-or-equal", typeof(NumberFilter<ulong>))]
        [InlineData(typeof(ulong), "greater-than-or-equal", typeof(NumberFilter<ulong>))]

        [InlineData(typeof(float), "equals", typeof(NumberFilter<float>))]
        [InlineData(typeof(float), "not-equals", typeof(NumberFilter<float>))]
        [InlineData(typeof(float), "less-than", typeof(NumberFilter<float>))]
        [InlineData(typeof(float), "greater-than", typeof(NumberFilter<float>))]
        [InlineData(typeof(float), "less-than-or-equal", typeof(NumberFilter<float>))]
        [InlineData(typeof(float), "greater-than-or-equal", typeof(NumberFilter<float>))]

        [InlineData(typeof(double), "equals", typeof(NumberFilter<double>))]
        [InlineData(typeof(double), "not-equals", typeof(NumberFilter<double>))]
        [InlineData(typeof(double), "less-than", typeof(NumberFilter<double>))]
        [InlineData(typeof(double), "greater-than", typeof(NumberFilter<double>))]
        [InlineData(typeof(double), "less-than-or-equal", typeof(NumberFilter<double>))]
        [InlineData(typeof(double), "greater-than-or-equal", typeof(NumberFilter<double>))]

        [InlineData(typeof(decimal), "equals", typeof(NumberFilter<decimal>))]
        [InlineData(typeof(decimal), "not-equals", typeof(NumberFilter<decimal>))]
        [InlineData(typeof(decimal), "less-than", typeof(NumberFilter<decimal>))]
        [InlineData(typeof(decimal), "greater-than", typeof(NumberFilter<decimal>))]
        [InlineData(typeof(decimal), "less-than-or-equal", typeof(NumberFilter<decimal>))]
        [InlineData(typeof(decimal), "greater-than-or-equal", typeof(NumberFilter<decimal>))]

        [InlineData(typeof(DateTime), "equals", typeof(DateTimeFilter))]
        [InlineData(typeof(DateTime), "not-equals", typeof(DateTimeFilter))]
        [InlineData(typeof(DateTime), "earlier-than", typeof(DateTimeFilter))]
        [InlineData(typeof(DateTime), "later-than", typeof(DateTimeFilter))]
        [InlineData(typeof(DateTime), "earlier-than-or-equal", typeof(DateTimeFilter))]
        [InlineData(typeof(DateTime), "later-than-or-equal", typeof(DateTimeFilter))]

        [InlineData(typeof(bool), "equals", typeof(BooleanFilter))]
        [InlineData(typeof(bool), "not-equals", typeof(BooleanFilter))]

        [InlineData(typeof(string), "equals", typeof(StringFilter))]
        [InlineData(typeof(string), "not-equals", typeof(StringFilter))]
        [InlineData(typeof(string), "contains", typeof(StringFilter))]
        [InlineData(typeof(string), "ends-with", typeof(StringFilter))]
        [InlineData(typeof(string), "starts-with", typeof(StringFilter))]

        [InlineData(typeof(Enum), "equals", typeof(EnumFilter))]
        [InlineData(typeof(Enum), "not-equals", typeof(EnumFilter))]

        [InlineData(typeof(Guid), "equals", typeof(GuidFilter))]
        [InlineData(typeof(Guid), "not-equals", typeof(GuidFilter))]
        public void GridFilters_RegistersDefaultFilters(Type type, string method, Type filter)
        {
            Assert.IsType(filter, new GridFilters().Create(type, method, ""));
        }

        [Theory]
        [InlineData(typeof(object))]
        [InlineData(typeof(object[]))]
        public void Create_NotFoundForType_ReturnsNull(Type type)
        {
            Assert.Null(filters.Create(type, "equals", ""));
        }

        [Fact]
        public void Create_NotFoundFilterType_ReturnsNull()
        {
            Assert.Null(filters.Create(typeof(string), "less-than", ""));
        }

        [Fact]
        public void Create_ForNullableType()
        {
            IGridFilter? actual = filters.Create(typeof(int?), "EQUALS", "");
            Assert.Equal("equals", Assert.IsType<NumberFilter<int>>(actual).Method);
        }

        [Theory]
        [InlineData(typeof(TestEnum))]
        [InlineData(typeof(TestEnum?))]
        public void Create_ForSpecificEnumType(Type type)
        {
            filters.Register(type, "equals", typeof(StringFilter));
            IGridFilter? actual = filters.Create(type, "EQUALS", "");
            Assert.Equal("equals", Assert.IsType<StringFilter>(actual).Method);
        }

        [Theory]
        [InlineData(typeof(TestEnum))]
        [InlineData(typeof(TestEnum?))]
        public void Create_ForEnumType(Type type)
        {
            IGridFilter? actual = filters.Create(type, "EQUALS", "");
            Assert.Equal("equals", Assert.IsType<EnumFilter>(actual).Method);
        }

        [Fact]
        public void Create_ForType()
        {
            IGridFilter? actual = filters.Create(typeof(string), "CONTAINS", "");
            Assert.Equal("contains", Assert.IsType<StringFilter>(actual).Method);
        }

        [Theory]
        [InlineData(typeof(string[]))]
        [InlineData(typeof(List<string>))]
        [InlineData(typeof(IEnumerable<string>))]
        public void Create_ForEnumerableType(Type type)
        {
            IGridFilter? actual = filters.Create(type, "CONTAINS", "");
            Assert.Equal("contains", Assert.IsType<EnumerableFilter<StringFilter>>(actual).Method);
        }

        [Fact]
        public void Create_ForEnumerableEnumType()
        {
            IGridFilter? actual = filters.Create(typeof(IEnumerable<TestEnum>), "Equals", "");
            Assert.Equal("equals", Assert.IsType<EnumerableFilter<EnumFilter>>(actual).Method);
        }

        [Fact]
        public void OptionsFor_ForBoolean()
        {
            SelectListItem[] actual = filters.OptionsFor(Substitute.For<IGridColumn<GridModel, bool>>()).ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Equal("", actual[0].Value);
            Assert.Equal("true", actual[1].Value);
            Assert.Equal("false", actual[2].Value);
            Assert.Equal(filters.BooleanTrueOptionText(), actual[1].Text);
            Assert.Equal(filters.BooleanEmptyOptionText(), actual[0].Text);
            Assert.Equal(filters.BooleanFalseOptionText(), actual[2].Text);
        }

        [Fact]
        public void OptionsFor_ForNullableBoolean()
        {
            SelectListItem[] actual = filters.OptionsFor(Substitute.For<IGridColumn<GridModel, bool?>>()).ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Equal("", actual[0].Value);
            Assert.Equal("true", actual[1].Value);
            Assert.Equal("false", actual[2].Value);
            Assert.Equal(filters.BooleanTrueOptionText(), actual[1].Text);
            Assert.Equal(filters.BooleanEmptyOptionText(), actual[0].Text);
            Assert.Equal(filters.BooleanFalseOptionText(), actual[2].Text);
        }

        [Fact]
        public void OptionsFor_ForEnumerableBoolean()
        {
            SelectListItem[] actual = filters.OptionsFor(Substitute.For<IGridColumn<GridModel, bool?[]>>()).ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Equal("", actual[0].Value);
            Assert.Equal("true", actual[1].Value);
            Assert.Equal("false", actual[2].Value);
            Assert.Equal(filters.BooleanTrueOptionText(), actual[1].Text);
            Assert.Equal(filters.BooleanEmptyOptionText(), actual[0].Text);
            Assert.Equal(filters.BooleanFalseOptionText(), actual[2].Text);
        }

        [Fact]
        public void OptionsFor_NullViewContext_ForEnum()
        {
            IGridColumn<GridModel, TestEnum> enumColumn = new GridColumn<GridModel, TestEnum>(column.Grid, _ => TestEnum.First);
            SelectListItem[] actual = filters.OptionsFor(enumColumn).ToArray();
            Assert.Single(actual);
            Assert.Null(actual[0].Text);
            Assert.Null(actual[0].Value);
        }

        [Fact]
        public void OptionsFor_ForEnum()
        {
            IHtmlHelper helper = Substitute.For<IHtmlHelper>();
            column.Grid.ViewContext = new ViewContext { HttpContext = Substitute.For<HttpContext>() };
            column.Grid.ViewContext.HttpContext.RequestServices.GetService(typeof(IHtmlHelper)).Returns(helper);
            IGridColumn<GridModel, TestEnum> enumColumn = new GridColumn<GridModel, TestEnum>(column.Grid, _ => TestEnum.First);
            helper.GetEnumSelectList(typeof(TestEnum)).Returns(new[] { new SelectListItem { Value = "0", Text = "1st" }, new SelectListItem { Value = "1", Text = "2nd" } });
            SelectListItem[] actual = filters.OptionsFor(enumColumn).ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Null(actual[0].Text);
            Assert.Null(actual[0].Value);
            Assert.Equal("0", actual[1].Value);
            Assert.Equal("1", actual[2].Value);
            Assert.Equal("1st", actual[1].Text);
            Assert.Equal("2nd", actual[2].Text);
        }

        [Fact]
        public void OptionsFor_ForNullableEnum()
        {
            IHtmlHelper helper = Substitute.For<IHtmlHelper>();
            column.Grid.ViewContext = new ViewContext { HttpContext = Substitute.For<HttpContext>() };
            column.Grid.ViewContext.HttpContext.RequestServices.GetService(typeof(IHtmlHelper)).Returns(helper);
            IGridColumn<GridModel, TestEnum?> enumColumn = new GridColumn<GridModel, TestEnum?>(column.Grid, _ => TestEnum.First);
            helper.GetEnumSelectList(typeof(TestEnum)).Returns(new[] { new SelectListItem { Value = "0", Text = "1st" }, new SelectListItem { Value = "1", Text = "2nd" } });
            SelectListItem[] actual = filters.OptionsFor(enumColumn).ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Null(actual[0].Text);
            Assert.Null(actual[0].Value);
            Assert.Equal("0", actual[1].Value);
            Assert.Equal("1", actual[2].Value);
            Assert.Equal("1st", actual[1].Text);
            Assert.Equal("2nd", actual[2].Text);
        }

        [Fact]
        public void OptionsFor_ForEnumerableEnum()
        {
            IHtmlHelper helper = Substitute.For<IHtmlHelper>();
            column.Grid.ViewContext = new ViewContext { HttpContext = Substitute.For<HttpContext>() };
            column.Grid.ViewContext.HttpContext.RequestServices.GetService(typeof(IHtmlHelper)).Returns(helper);
            IGridColumn<GridModel, IEnumerable<TestEnum?>> enumColumn = new GridColumn<GridModel, IEnumerable<TestEnum?>>(column.Grid, _ => new TestEnum?[] { TestEnum.First });
            helper.GetEnumSelectList(typeof(TestEnum)).Returns(new[] { new SelectListItem { Value = "0", Text = "1st" }, new SelectListItem { Value = "1", Text = "2nd" } });
            SelectListItem[] actual = filters.OptionsFor(enumColumn).ToArray();
            Assert.Equal(3, actual.Length);
            Assert.Null(actual[0].Text);
            Assert.Null(actual[0].Value);
            Assert.Equal("0", actual[1].Value);
            Assert.Equal("1", actual[2].Value);
            Assert.Equal("1st", actual[1].Text);
            Assert.Equal("2nd", actual[2].Text);
        }

        [Fact]
        public void OptionsFor_ForOtherTypes()
        {
            Assert.Empty(filters.OptionsFor(Substitute.For<IGridColumn<GridModel, string>>()));
        }

        [Fact]
        public void Register_FilterForExistingType()
        {
            filters.Register(typeof(int), "TEST", typeof(object));
            filters.Register(typeof(int), "TEST-FILTER", typeof(StringFilter));
            Assert.IsType<StringFilter>(filters.Create(typeof(int), "test-filter", ""));
        }

        [Fact]
        public void Register_NullableFilterTypeForExistingType()
        {
            filters.Register(typeof(int), "TEST", typeof(object));
            filters.Register(typeof(int?), "TEST-FILTER", typeof(StringFilter));
            Assert.IsType<StringFilter>(filters.Create(typeof(int), "test-filter", ""));
        }

        [Fact]
        public void Register_Overrides_NullableFilter()
        {
            filters.Register(typeof(int), "test-filter", typeof(object));
            filters.Register(typeof(int?), "TEST-filter", typeof(NumberFilter<int>));
            Assert.IsType<NumberFilter<int>>(filters.Create(typeof(int), "test-filter", ""));
        }

        [Fact]
        public void Register_Overrides_Filter()
        {
            filters.Register(typeof(int), "test-filter", typeof(object));
            filters.Register(typeof(int), "TEST-filter", typeof(NumberFilter<int>));
            Assert.IsType<NumberFilter<int>>(filters.Create(typeof(int), "test-filter", ""));
        }

        [Fact]
        public void Register_NullableTypeAsNotNullable()
        {
            filters.Register(typeof(int?), "TEST", typeof(NumberFilter<int>));
            Assert.IsType<NumberFilter<int>>(filters.Create(typeof(int), "test", ""));
        }

        [Fact]
        public void Register_FilterForNewType()
        {
            filters.Register(typeof(object), "test", typeof(NumberFilter<int>));
            Assert.IsType<NumberFilter<int>>(filters.Create(typeof(object), "test", ""));
        }

        [Fact]
        public void Unregister_ExistingFilter()
        {
            filters.Register(typeof(object), "test", typeof(StringFilter));
            filters.Unregister(typeof(object), "TEST");
            Assert.Null(filters.Create(typeof(object), "test", ""));
        }

        [Fact]
        public void Unregister_NotExistingFilter()
        {
            filters.Unregister(GetType(), "test");
            Assert.Null(filters.Create(GetType(), "test", ""));
        }

        [Fact]
        public void Unregister_NotExistingMethod()
        {
            filters.Register(typeof(object), "test", typeof(StringFilter));
            filters.Unregister(typeof(object), "method");
            Assert.Null(filters.Create(typeof(object), "method", Array.Empty<string>()));
        }
    }
}
