using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Forged.Grid.Web.Filters
{
    public class NumberContainsFilter : AGridFilter
    {
        public override Expression? Apply(Expression expression)
        {
            if (Values.Count == 0 || Values.Any(string.IsNullOrEmpty))
                return null;
            return base.Apply(expression);
        }

        protected override Expression? Apply(Expression expression, string? value)
        {
            Expression valueExpression = Expression.Constant(value?.ToUpper());
            MethodInfo toStringMethod = typeof(int).GetMethod(nameof(Int32.ToString), new Type[0])!;
            MethodInfo containsMethod = typeof(string).GetMethod(nameof(String.Contains), new[] { typeof(string) })!;
            Expression toString = Expression.Call(expression, toStringMethod);
            return Expression.Call(toString, containsMethod, valueExpression);
        }
    }
}
