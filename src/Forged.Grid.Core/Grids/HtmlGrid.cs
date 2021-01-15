using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace Forged.Grid
{
    public class HtmlGrid<T> : IHtmlGrid<T>
    {
        public IGrid<T> Grid { get; set; }
        public IHtmlHelper Html { get; set; }
        public string PartialViewName { get; set; }

        public HtmlGrid(IHtmlHelper html, IGrid<T> grid)
        {
            Html = html;
            Grid = grid;
            PartialViewName = "MvcGrid/_Grid";
            grid.ViewContext ??= html.ViewContext;
            grid.Query ??= grid.ViewContext.HttpContext.Request.Query;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            Html.Partial(PartialViewName, Grid).WriteTo(writer, encoder);
        }
    }
}
