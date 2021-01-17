using Xunit;

namespace Forged.Grid
{
    public class GridConfigTests
    {
        [Fact]
        public void GridConfig_Defaults()
        {
            GridConfig actual = new GridConfig();

            Assert.Empty(actual.Columns);
            Assert.Empty(actual.Name);
        }
    }
}
