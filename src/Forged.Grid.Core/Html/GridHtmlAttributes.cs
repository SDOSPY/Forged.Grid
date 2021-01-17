using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;

namespace Forged.Grid
{
    public class GridHtmlAttributes : Dictionary<string, object?>, IHtmlContent
    {
        public GridHtmlAttributes()
        {
        }
        public GridHtmlAttributes(object? attributes)
            : base(HtmlHelper.AnonymousObjectToHtmlAttributes(attributes))
        {
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            foreach (KeyValuePair<string, object?> attribute in this)
            {
                if (attribute.Value == null)
                    continue;
                writer.Write(" ");
                writer.Write(attribute.Key);
                writer.Write("=\"");
                writer.Write(encoder.Encode(attribute.Value.ToString()));
                writer.Write("\"");
            }
        }
    }
}
