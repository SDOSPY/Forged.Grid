using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Forged.Grid.Tests
{
    public class NumberFilterTests
    {
        private readonly Expression<Func<GridModel, int?>> nSumExpression;
        private readonly Expression<Func<GridModel, int>> sumExpression;
        private readonly IQueryable<GridModel> items;

        public NumberFilterTests()
        {
            items = new[]
            {
                new GridModel(),
                new GridModel { NSum = 1, Sum = 2 },
                new GridModel { NSum = 2, Sum = 1 }
            }.AsQueryable();
            nSumExpression = (model) => model.NSum;
            sumExpression = (model) => model.Sum;
        }

        [Theory]
        [InlineData("test")]
        [InlineData("79228162514264337593543950336")]
        [InlineData("-79228162514264337593543950336")]
        public void Apply_BadDecimalValue_ReturnsNull(string value)
        {
            NumberFilter<decimal> filter = new NumberFilter<decimal> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("test")]
        [InlineData("1.8076931348623157E+308")]
        [InlineData("-1.8076931348623157E+308")]
        public void Apply_BadDoubleValue_ReturnsNull(string value)
        {
            NumberFilter<double> filter = new NumberFilter<double> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("test")]
        [InlineData("3.50282347E+38")]
        [InlineData("-3.50282347E+38")]
        public void Apply_BadSingleValue_ReturnsNull(string value)
        {
            NumberFilter<float> filter = new NumberFilter<float> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("test")]
        [InlineData("9223372036854775808")]
        [InlineData("-9223372036854775809")]
        public void Apply_BadInt64Value_ReturnsNull(string value)
        {
            NumberFilter<long> filter = new NumberFilter<long> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        [InlineData("18446744073709551616")]
        public void Apply_BadUInt64Value_ReturnsNull(string value)
        {
            NumberFilter<ulong> filter = new NumberFilter<ulong> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("test")]
        [InlineData("2147483648")]
        [InlineData("-2147483649")]
        public void Apply_BadInt32Value_ReturnsNull(string value)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        [InlineData("4294967296")]
        public void Apply_BadUInt32Value_ReturnsNull(string value)
        {
            NumberFilter<uint> filter = new NumberFilter<uint> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("test")]
        [InlineData("32768")]
        [InlineData("-32769")]
        public void Apply_BadInt16Value_ReturnsNull(string value)
        {
            NumberFilter<short> filter = new NumberFilter<short> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("test")]
        [InlineData("65536")]
        public void Apply_BadUInt16Value_ReturnsNull(string value)
        {
            NumberFilter<ushort> filter = new NumberFilter<ushort> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("128")]
        [InlineData("-129")]
        [InlineData("test")]
        public void Apply_BadSByteValue_ReturnsNull(string value)
        {
            NumberFilter<sbyte> filter = new NumberFilter<sbyte> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("256")]
        [InlineData("test")]
        public void Apply_BadByteValue_ReturnsNull(string value)
        {
            NumberFilter<byte> filter = new NumberFilter<byte> { Method = "equals", Values = value };
            Assert.Null(filter.Apply(sumExpression.Body));
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NullableEqualsFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = value };
            IEnumerable expected = items.Where(model => model.NSum == number);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableEqualsFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = new[] { "", "1" } };
            IEnumerable expected = items.Where(model => model.NSum == null || model.NSum == 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_EqualsFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = value };
            IEnumerable expected = items.Where(model => model.Sum == number);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleEqualsFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = new[] { "1", "2" } };
            IEnumerable expected = items.Where(model => model.Sum == 1 || model.Sum == 2);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NullableNotEqualsFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "not-equals", Values = value };
            IEnumerable expected = items.Where(model => model.NSum != number);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableNotEqualsFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "not-equals", Values = new[] { "", "1" } };
            IEnumerable expected = items.Where(model => model.NSum != null && model.NSum != 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NotEqualsFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "not-equals", Values = value };
            IEnumerable expected = items.Where(model => model.Sum != number);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNotEqualsFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "not-equals", Values = new[] { "1", "2" } };
            IEnumerable expected = items.Where(model => model.Sum != 1 && model.Sum != 2);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NullableLessThanFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than", Values = value };
            IEnumerable expected = items.Where(model => model.NSum < number);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableLessThanFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than", Values = new[] { "", "1" } };
            IEnumerable expected = items.Where(model => model.NSum < 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_LessThanFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than", Values = value };
            IEnumerable expected = items.Where(model => model.Sum < number);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleLessThanFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than", Values = new[] { "1", "2" } };
            IEnumerable expected = items.Where(model => model.Sum < 2);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NullableGreaterThanFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than", Values = value };
            IEnumerable expected = items.Where(model => model.NSum > number);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableGreaterThanFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than", Values = new[] { "", "1" } };
            IEnumerable expected = items.Where(model => model.NSum > 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_GreaterThanFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than", Values = value };
            IEnumerable expected = items.Where(model => model.Sum > number);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleGreaterThanFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than", Values = new[] { "1", "2" } };
            IEnumerable expected = items.Where(model => model.Sum > 1);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NullableLessThanOrEqualFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than-or-equal", Values = value };
            IEnumerable expected = items.Where(model => model.NSum <= number);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableLessThanOrEqualFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than-or-equal", Values = new[] { "", "1" } };
            IEnumerable expected = items.Where(model => model.NSum <= 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_LessThanOrEqualFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than-or-equal", Values = value };
            IEnumerable expected = items.Where(model => model.Sum <= number);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleLessThanOrEqualFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "less-than-or-equal", Values = new[] { "0", "1" } };
            IEnumerable expected = items.Where(model => model.Sum <= 1);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_NullableGreaterThanOrEqualFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than-or-equal", Values = value };
            IEnumerable actual = items.Where(nSumExpression, filter);
            IEnumerable expected = items.Where(model => model.NSum >= number);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleNullableGreaterThanOrEqualFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than-or-equal", Values = new[] { "", "1" } };
            IEnumerable expected = items.Where(model => model.NSum >= 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("", null)]
        public void Apply_GreaterThanOrEqualFilter(string value, int? number)
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than-or-equal", Values = value };
            IEnumerable actual = items.Where(sumExpression, filter);
            IEnumerable expected = items.Where(model => model.Sum >= number);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleGreaterThanOrEqualFilter()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "greater-than-or-equal", Values = new[] { "1", "2" } };
            IEnumerable expected = items.Where(model => model.Sum >= 1);
            IEnumerable actual = items.Where(sumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_MultipleWithBadValues()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = new[] { "", "test", "1" } };
            IEnumerable expected = items.Where(model => model.NSum == null ||  model.NSum == 1);
            IEnumerable actual = items.Where(nSumExpression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_EmptyValue_ReturnsNull()
        {
            NumberFilter<int> filter = new NumberFilter<int> { Method = "equals", Values = StringValues.Empty };
            Assert.Null(filter.Apply(nSumExpression.Body));
        }

        [Fact]
        public void Apply_BadMethod_ReturnsNull()
        {
            Assert.Null(new NumberFilter<int> { Method = "test", Values = "1" }.Apply(sumExpression.Body));
        }
    }
}
