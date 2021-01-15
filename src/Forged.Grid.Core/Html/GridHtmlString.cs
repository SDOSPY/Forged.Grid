using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;

namespace Forged.Grid
{
    public class GridHtmlString : IHtmlContent
    {
        private string Value { get; }

        public GridHtmlString(string? value)
        {
            Value = value ?? "";
        }

        public void WriteTo(TextWriter writer, HtmlEncoder? encoder)
        {
            writer.Write(encoder?.Encode(Value) ?? Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
