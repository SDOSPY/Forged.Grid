using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Forged.Grid.Tests
{
    public class EnumFilterTests
    {
        private readonly Expression<Func<GridModel, TestEnum?>> nEnumExpression;
        private readonly Expression<Func<GridModel, TestEnum>> enumExpression;
        private readonly IQueryable<GridModel> items;
        private readonly EnumFilter filter;

        public EnumFilterTests()
        {
            items = new[]
            {
                new GridModel(),
                new GridModel { Enum = TestEnum.First },
                new GridModel { Enum = TestEnum.Second },
                new GridModel { Enum = TestEnum.First, NEnum = TestEnum.Second },
                new GridModel { Enum = TestEnum.Second, NEnum = TestEnum.First }
            }.AsQueryable();
            nEnumExpression = (model) => model.NEnum;
            enumExpression = (model) => model.Enum;
            filter = new EnumFilter();
        }

        [Fact]
        public void Apply_BadValue_ReturnsNull()
        {
            filter.Values = "test";
            filter.Method = "equals";
            Assert.Null(filter.Apply(enumExpression.Body));
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("1", TestEnum.Second)]
        public void Apply_NullableEqualsFilter(string value, TestEnum? test)
        {
            filter.Values = value;
            filter.Method = "equals";
            IEnumerable actual = items.Where(nEnumExpression, filter);
            IEnumerable expected = items.Where(model => model.NEnum == test);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("1", TestEnum.Second)]
        public void Apply_EqualsFilter(string value, TestEnum? test)
        {
            filter.Values = value;
            filter.Method = "equals";
            IEnumerable actual = items.Where(enumExpression, filter);
            IEnumerable expected = items.Where(model => model.Enum == test);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("1", TestEnum.Second)]
        public void Apply_NullableNotEqualsFilter(string value, TestEnum? test)
        {
            filter.Values = value;
            filter.Method = "not-equals";
            IEnumerable actual = items.Where(nEnumExpression, filter);
            IEnumerable expected = items.Where(model => model.NEnum != test);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("1", TestEnum.Second)]
        public void Apply_NotEqualsFilter(string value, TestEnum? test)
        {
            filter.Values = value;
            filter.Method = "not-equals";
            IEnumerable actual = items.Where(enumExpression, filter);
            IEnumerable expected = items.Where(model => model.Enum != test);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_EmptyValue_ReturnsNull()
        {
            filter.Method = "equals";
            filter.Values = StringValues.Empty;
            Assert.Null(filter.Apply(enumExpression.Body));
        }

        [Fact]
        public void Apply_BadMethod_ReturnsNull()
        {
            filter.Values = "0";
            filter.Method = "test";
            Assert.Null(filter.Apply(enumExpression.Body));
        }
    }
}
