using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public class DateTimeFilter : AGridFilter
    {
        protected override Expression? Apply(Expression expression, string? value)
        {
            if (string.IsNullOrEmpty(value) && Nullable.GetUnderlyingType(expression.Type) == null)
                expression = Expression.Convert(expression, typeof(Nullable<>).MakeGenericType(expression.Type));
            try
            {
                object dateValue = TypeDescriptor.GetConverter(expression.Type).ConvertFrom(value);
                return Method switch
                {
                    "later-than-or-equal" => Expression.GreaterThanOrEqual(expression, Expression.Constant(dateValue, expression.Type)),
                    "earlier-than-or-equal" => Expression.LessThanOrEqual(expression, Expression.Constant(dateValue, expression.Type)),
                    "later-than" => Expression.GreaterThan(expression, Expression.Constant(dateValue, expression.Type)),
                    "earlier-than" => Expression.LessThan(expression, Expression.Constant(dateValue, expression.Type)),
                    "not-equals" => Expression.NotEqual(expression, Expression.Constant(dateValue, expression.Type)),
                    "equals" => Expression.Equal(expression, Expression.Constant(dateValue, expression.Type)),
                    _ => null
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
