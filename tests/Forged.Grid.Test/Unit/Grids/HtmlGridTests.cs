using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xunit;

namespace Forged.Grid.Tests
{
    public class HtmlGridTests
    {
        private HtmlGrid<GridModel> htmlGrid;

        public HtmlGridTests()
        {
            IHtmlHelper html = Substitute.For<IHtmlHelper>();
            IGrid<GridModel> grid = new Grid<GridModel>(new GridModel[8]);
            html.ViewContext.Returns(new ViewContext { HttpContext = new DefaultHttpContext() });

            htmlGrid = new HtmlGrid<GridModel>(html, grid);

            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);
        }

        [Fact]
        public void HtmlGrid_DoesNotChangeQuery()
        {
            object? expected = htmlGrid.Grid.Query = new QueryCollection();
            object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.Query;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsRequestGridQuery()
        {
            htmlGrid.Grid.Query = null;
            htmlGrid.Html.ViewContext.Returns(new ViewContext());
            htmlGrid.Html.ViewContext.HttpContext = new DefaultHttpContext();

            object? expected = htmlGrid.Html.ViewContext.HttpContext.Request.Query;
            object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.Query;

            Assert.Same(expected, actual);
            Assert.NotNull(actual);
        }

        [Fact]
        public void HtmlGrid_SetsEmptyGridQuery()
        {
            htmlGrid.Grid.Query = null;
            htmlGrid.Html.ViewContext.ReturnsNull();

            Assert.Empty(new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.Query);
        }

        [Fact]
        public void HtmlGrid_DoesNotChangeViewContext()
        {
            htmlGrid.Grid.ViewContext = new ViewContext { HttpContext = new DefaultHttpContext() };
            htmlGrid.Html.ViewContext.Returns(new ViewContext { HttpContext = new DefaultHttpContext() });

            object? expected = htmlGrid.Grid.ViewContext;
            object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.ViewContext;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsViewContext()
        {
            htmlGrid.Grid.ViewContext = null;
            htmlGrid.Html.ViewContext.Returns(new ViewContext());
            htmlGrid.Html.ViewContext.HttpContext = new DefaultHttpContext();

            object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.ViewContext;
            object? expected = htmlGrid.Html.ViewContext;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsPartialViewName()
        {
            string actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).PartialViewName;
            string expected = "ForgedGrid/_Grid";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsHtml()
        {
            object actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Html;
            object expected = htmlGrid.Html;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsGrid()
        {
            object actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid;
            object expected = htmlGrid.Grid;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void WriteTo_WritesPartialView()
        {
            StringWriter writer = new StringWriter();
            Task<IHtmlContent> view = Task.FromResult<IHtmlContent>(new HtmlString("Test"));
            htmlGrid.Html.PartialAsync(htmlGrid.PartialViewName, htmlGrid.Grid, null).Returns(view);

            htmlGrid.WriteTo(writer, HtmlEncoder.Default);

            string actual = writer.GetStringBuilder().ToString();
            string expected = "Test";

            Assert.Equal(expected, actual);
        }
    }
}
