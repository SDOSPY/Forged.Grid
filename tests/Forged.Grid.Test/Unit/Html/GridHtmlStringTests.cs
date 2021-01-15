using System;
using System.IO;
using System.Text.Encodings.Web;
using Xunit;

namespace Forged.Grid.Tests
{
    public class GridHtmlStringTests
    {
        private readonly TextWriter writer;

        public GridHtmlStringTests()
        {
            writer = new StringWriter();
        }

        [Fact]
        public void WriteTo_RawString()
        {
            new GridHtmlString("<test>").WriteTo(writer, null);
            string? actual = writer.ToString();
            string? expected = "<test>";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteTo_EncodedString()
        {
            new GridHtmlString("<test>").WriteTo(writer, HtmlEncoder.Default);
            string? expected = HtmlEncoder.Default.Encode("<test>");
            string? actual = writer.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(null, "")]
        [InlineData("test", "test")]
        public void ToString_Value(string value, string representation)
        {
            string actual = new GridHtmlString(value).ToString();
            string expected = representation;
            Assert.Equal(expected, actual);
        }
    }
}
