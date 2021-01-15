using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Forged.Grid.Tests
{
    public class StringFilterTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Apply_StartsWith_NullOrEmptyValue_ReturnsNull(string? value)
        {
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            Assert.Null(new StringFilter { Method = "starts-with", Values = new[] { value, "1" } }.Apply(expression.Body));
        }

        [Fact]
        public void Apply_StartsWith_UsingOriginalCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "starts-with", Values = "tes" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "ttes" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TTESTE" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.StartsWith("tes"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_StartsWith_UsingUpperCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "starts-with", Values = "tt" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Upper;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "ttes" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TTESTE" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper().StartsWith("TT"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_StartsWith_UsingLowerCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "starts-with", Values = "TE" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Lower;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TTESTE" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToLower().StartsWith("te"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_StartsWith_MultiFilter()
        {
            StringFilter filter = new StringFilter { Method = "starts-with", Values = new[] { "te", "TT" } };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "ttes" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TTESTE" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && (model.Name.StartsWith("te") || model.Name.StartsWith("TT")));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Apply_EndsWith_NullOrEmptyValue_ReturnsNull(string? value)
        {
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            Assert.Null(new StringFilter { Method = "ends-with", Values = new[] { value, "1" } }.Apply(expression));
        }

        [Fact]
        public void Apply_EndsWith_UsingOriginalCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "ends-with", Values = "est" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TESTEr" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.EndsWith("est"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_EndsWith_UsingUpperCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "ends-with", Values = "est" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Upper;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TESTEr" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper().EndsWith("EST"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_EndsWith_UsingLowerCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "ends-with", Values = "EST" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Lower;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TESTEr" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToLower().EndsWith("est"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_EndsWith_MultiFilter()
        {
            StringFilter filter = new StringFilter { Method = "ends-with", Values = new[] { "t", "Er" } };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" },
                new GridModel { Name = "TESTE" },
                new GridModel { Name = "TESTEr" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && (model.Name.EndsWith("t") || model.Name.EndsWith("Er")));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Apply_Contains_NullOrEmptyValue_ReturnsNull(string? value)
        {
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            Assert.Null(new StringFilter { Method = "contains", Values = new[] { value, "1" } }.Apply(expression));
        }

        [Fact]
        public void Apply_Contains_UsingOriginalCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "contains", Values = "es" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.Contains("es"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Contains_UsingUpperCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "contains", Values = "es" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Upper;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper().Contains("ES"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Contains_UsingLowerCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "contains", Values = "ES" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Lower;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToLower().Contains("es"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Contains_MultiFilter()
        {
            StringFilter filter = new StringFilter { Method = "contains", Values = new[] { "Te", "es" } };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEST" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && (model.Name.Contains("Te") || model.Name.Contains("es")));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(GridFilterCase.Lower)]
        [InlineData(GridFilterCase.Upper)]
        [InlineData(GridFilterCase.Original)]
        public void Apply_NotEquals_FiltersNotEmptyAndNotNullValues(GridFilterCase filterCase)
        {
            StringFilter filter = new StringFilter { Method = "not-equals", Values = "" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = filterCase;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "" },
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => !string.IsNullOrEmpty(model.Name));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_NotEquals_UsingOriginalCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "not-equals", Values = "test" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != "test");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_NotEquals_UsingUpperCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "not-equals", Values = "test" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Upper;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name == null || model.Name.ToUpper() != "TEST");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_NotEquals_UsingLowerCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "not-equals", Values = "TEST" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Lower;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name == null || model.Name.ToLower() != "test");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_NotEquals_MultiFilter()
        {
            StringFilter filter = new StringFilter { Method = "not-equals", Values = new[] { "test", "Test2" } };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != "test" && model.Name != "Test2");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(GridFilterCase.Lower)]
        [InlineData(GridFilterCase.Upper)]
        [InlineData(GridFilterCase.Original)]
        public void Apply_Equals_FiltersEmptyOrNullValues(GridFilterCase filterCase)
        {
            StringFilter filter = new StringFilter { Method = "equals", Values = "" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = filterCase;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = "" },
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => string.IsNullOrEmpty(model.Name));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Equals_UsingOriginalCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "equals", Values = "test" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name == "test");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Equals_UsingUpperCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "equals", Values = "test" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Upper;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper() == "TEST");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Equals_UsingLowerCaseFilter()
        {
            StringFilter filter = new StringFilter { Method = "equals", Values = "TEST" };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Lower;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToLower() == "test");
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Apply_Equals_MultiFilter()
        {
            StringFilter filter = new StringFilter { Method = "equals", Values = new[] { "test", "Test2" } };
            Expression<Func<GridModel, string?>> expression = (model) => model.Name;
            filter.Case = GridFilterCase.Original;
            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();
            IQueryable expected = items.Where(model => model.Name != null && (model.Name == "test" || model.Name == "Test2"));
            IQueryable actual = items.Where(expression, filter);
            Assert.Equal(expected, actual);
        }
    }
}
