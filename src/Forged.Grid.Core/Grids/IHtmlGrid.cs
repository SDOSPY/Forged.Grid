using Microsoft.AspNetCore.Html;

namespace Forged.Grid
{
    public interface IHtmlGrid<T> : IHtmlContent
    {
        IGrid<T> Grid { get; }

        string PartialViewName { get; set; }
    }
}
