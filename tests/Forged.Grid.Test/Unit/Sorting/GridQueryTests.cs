using System;
using System.Linq;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridQueryTests
    {
        [Fact]
        public void IsOrdered_False()
        {
            Assert.False(GridQuery.IsOrdered(Array.Empty<object>().AsQueryable().Where(_ => true)));
        }

        [Fact]
        public void IsOrdered_Ascending_True()
        {
            Assert.True(GridQuery.IsOrdered(Array.Empty<object>().AsQueryable().OrderBy(_ => 0)));
        }

        [Fact]
        public void IsOrdered_Descending_True()
        {
            Assert.True(GridQuery.IsOrdered(Array.Empty<object>().AsQueryable().OrderByDescending(_ => 0)));
        }
    }
}
