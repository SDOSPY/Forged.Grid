using Xunit;

namespace Forged.Grid.Tests
{
    public class GridColumnConfigTests
    {
        [Fact]
        public void GridColumnConfig_Defaults()
        {
            GridColumnConfig actual = new GridColumnConfig();
            Assert.False(actual.Hidden);
            Assert.Empty(actual.Name);
        }
    }
}
