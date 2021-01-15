using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Forged.Grid
{
    public static class GridExtensions
    {
        public static HtmlGrid<T> Grid<T>(this IHtmlHelper html, IEnumerable<T> source) where T : class
        {
            return new HtmlGrid<T>(html, new Grid<T>(source));
        }
        public static HtmlGrid<T> Grid<T>(this IHtmlHelper html, string partialViewName, IEnumerable<T> source) where T : class
        {
            return new HtmlGrid<T>(html, new Grid<T>(source)) { PartialViewName = partialViewName };
        }

        public static IHtmlContent AjaxGrid(this IHtmlHelper _, string url, object? htmlAttributes = null)
        {
            TagBuilder grid = new TagBuilder("div");
            grid.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            grid.Attributes["data-url"] = url;
            grid.AddCssClass("forged-grid");
            return grid;
        }

        public static IServiceCollection AddForgedGrid(this IServiceCollection services, Action<GridFilters>? configure = null)
        {
            GridFilters filters = new GridFilters();
            configure?.Invoke(filters);
            return services.AddSingleton<IGridFilters>(filters);
        }
    }
}
