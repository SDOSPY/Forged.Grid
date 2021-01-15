using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Forged.Grid
{
    public class BooleanFilter : AGridFilter
    {
        protected override Expression? Apply(Expression expression, string? value)
        {
            if (string.IsNullOrEmpty(value) && Nullable.GetUnderlyingType(expression.Type) == null)
                expression = Expression.Convert(expression, typeof(Nullable<>).MakeGenericType(expression.Type));
            try
            {
                object boolValue = TypeDescriptor.GetConverter(expression.Type).ConvertFrom(value);
                return Method switch
                {
                    "not-equals" => Expression.NotEqual(expression, Expression.Constant(boolValue, expression.Type)),
                    "equals" => Expression.Equal(expression, Expression.Constant(boolValue, expression.Type)),
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
