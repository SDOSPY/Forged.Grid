using Microsoft.Extensions.Primitives;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public abstract class AGridFilter : IGridFilter
    {
        public string? Method { get; set; }
        public StringValues Values { get; set; }
        public GridFilterCase Case { get; set; }

        protected AGridFilter()
        {
            Case = GridFilterCase.Original;
        }

        public virtual Expression? Apply(Expression expression)
        {
            Expression? filter = null;
            foreach (string value in Values)
                if (Apply(expression, value) is Expression next)
                    filter = filter == null
                        ? next
                        : Method == "not-equals"
                            ? Expression.AndAlso(filter, next)
                            : Expression.OrElse(filter, next);
            return filter;
        }
        protected abstract Expression? Apply(Expression expression, string? value);
    }
}
