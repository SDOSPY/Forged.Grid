using System.Linq;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public sealed class GridQuery : ExpressionVisitor
    {
        private bool Ordered { get; set; }

        private GridQuery()
        { }

        public static bool IsOrdered(IQueryable models)
        {
            GridQuery expression = new GridQuery();
            expression.Visit(models.Expression);
            return expression.Ordered;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable) &&
                (node.Method.Name == nameof(Queryable.OrderBy) || node.Method.Name == nameof(Queryable.OrderByDescending)))
            {
                Ordered = true;
                return node;
            }
            return base.VisitMethodCall(node);
        }
    }
}
