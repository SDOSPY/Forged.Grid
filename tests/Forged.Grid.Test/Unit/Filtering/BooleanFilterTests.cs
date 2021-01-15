using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Forged.Grid.Tests
{
    public class BooleanFilterTests
    {
        private readonly BooleanFilter filter;
        private readonly IQueryable<GridModel> items;
        private readonly Expression<Func<GridModel, bool>> booleanExpression;
        private readonly Expression<Func<GridModel, bool?>> nBooleanExpression;

        public BooleanFilterTests()
        {
            items = new[]
            {
                new GridModel(),
                new GridModel { IsChecked = true, NIsChecked = false },
                new GridModel { IsChecked = false, NIsChecked = true }
            }.AsQueryable();
            filter = new BooleanFilter();
            booleanExpression = (model) => model.IsChecked;
            nBooleanExpression = (model) => model.NIsChecked;
        }

        [Fact]
        public void Apply_BadValue_ReturnsNull()
        {
            filter.Values = "Test";
            filter.Method = "equals";
            Assert.Null(filter.Apply(booleanExpression.Body));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        [InlineData("false", false)]
        [InlineData("FALSE", false)]
        public void Apply_NullableEqualsFilter(string value, bool? isChecked)
        {
            filter.Values = value;
            filter.Method = "equals";
            IEnumerable actual = items.Where(nBooleanExpression, filter);
            IEnumerable expected = items.Where(model => model.NIsChecked == isChecked);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableEqualsFilter()
        {
            filter.Method = "equals";
            filter.Values = new[] { "", "false" };
            IEnumerable actual = items.Where(nBooleanExpression, filter);
            IEnumerable expected = items.Where(model => model.NIsChecked != true);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        [InlineData("false", false)]
        [InlineData("FALSE", false)]
        public void Apply_EqualsFilter(string value, bool? isChecked)
        {
            filter.Values = value;
            filter.Method = "equals";
            IEnumerable actual = items.Where(booleanExpression, filter);
            IEnumerable expected = items.Where(model => model.IsChecked == isChecked);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleEqualsFilter()
        {
            filter.Method = "equals";
            filter.Values = new[] { "true", "false" };
            IEnumerable expected = items;
            IEnumerable actual = items.Where(booleanExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        [InlineData("false", false)]
        [InlineData("FALSE", false)]
        public void Apply_NullableNotEqualsFilter(string value, bool? isChecked)
        {
            filter.Values = value;
            filter.Method = "not-equals";
            IEnumerable actual = items.Where(nBooleanExpression, filter);
            IEnumerable expected = items.Where(model => model.NIsChecked != isChecked);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableNotEqualsFilter()
        {
            filter.Method = "not-equals";
            filter.Values = new[] { "", "false" };
            IEnumerable actual = items.Where(nBooleanExpression, filter);
            IEnumerable expected = items.Where(model => model.NIsChecked == true);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("true", true)]
        [InlineData("TRUE", true)]
        [InlineData("false", false)]
        [InlineData("FALSE", false)]
        public void Apply_NotEqualsFilter(string value, bool? isChecked)
        {
            filter.Values = value;
            filter.Method = "not-equals";
            IEnumerable actual = items.Where(booleanExpression, filter);
            IEnumerable expected = items.Where(model => model.IsChecked != isChecked);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNotEqualsFilter()
        {
            filter.Method = "not-equals";
            filter.Values = new[] { "true", "false" };
            Assert.Empty(items.Where(booleanExpression, filter));
        }

        [Fact]
        public void Apply_MultipleWithBadValues()
        {
            filter.Method = "equals";
            filter.Values = new[] { "", "test", "false" };
            IEnumerable actual = items.Where(nBooleanExpression, filter);
            IEnumerable expected = items.Where(model => model.NIsChecked != true);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_EmptyValue_ReturnsNull()
        {
            filter.Method = "equals";
            filter.Values = StringValues.Empty;
            Assert.Null(filter.Apply(booleanExpression.Body));
        }

        [Fact]
        public void Apply_BadMethod_ReturnsNull()
        {
            filter.Method = "test";
            filter.Values = "false";
            Assert.Null(filter.Apply(booleanExpression.Body));
        }
    }
}
