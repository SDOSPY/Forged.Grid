using System;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridRowTests
    {
        [Fact]
        public void GridRow_SetsIndex()
        {
            Assert.Equal(3, new GridRow<object>(new object(), 3).Index);
        }

        [Fact]
        public void GridRow_SetsModel()
        {
            object expected = new object();
            object actual = new GridRow<object>(expected, 0).Model;
            Assert.Same(expected, actual);
        }
    }
}
