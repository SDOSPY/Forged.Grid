using Microsoft.Extensions.Primitives;

using System.Linq.Expressions;

namespace Forged.Grid
{
    public interface IGridFilter
    {
        string? Method { get; set; }
        StringValues Values { get; set; }
        GridFilterCase Case { get; set; }

        Expression? Apply(Expression expression);
    }
}
