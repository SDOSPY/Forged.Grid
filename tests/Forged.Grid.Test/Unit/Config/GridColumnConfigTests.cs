using Xunit;

namespace Forged.Grid
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
