using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Forged.Grid
{
    public class StringFilter : AGridFilter
    {
        protected static Expression Null { get; }
        protected static Expression Empty { get; }
        protected static MethodInfo ToLower { get; }
        protected static MethodInfo ToUpper { get; }
        protected static MethodInfo Contains { get; }
        protected static MethodInfo EndsWith { get; }
        protected static MethodInfo StartsWith { get; }

        static StringFilter()
        {
            Empty = Expression.Constant("");
            Null = Expression.Constant(null, typeof(string));
            ToLower = typeof(string).GetMethod(nameof(String.ToLower), Array.Empty<Type>())!;
            ToUpper = typeof(string).GetMethod(nameof(String.ToUpper), Array.Empty<Type>())!;
            Contains = typeof(string).GetMethod(nameof(String.Contains), new[] { typeof(string) })!;
            EndsWith = typeof(string).GetMethod(nameof(String.EndsWith), new[] { typeof(string) })!;
            StartsWith = typeof(string).GetMethod(nameof(String.StartsWith), new[] { typeof(string) })!;
        }

        public override Expression? Apply(Expression expression)
        {
            if (Values.Count == 0)
                return null;
            switch (Method)
            {
                case "starts-with":
                case "ends-with":
                case "contains":
                    if (Values.Any(string.IsNullOrEmpty))
                        return null;
                    return Expression.AndAlso(Expression.NotEqual(expression, Null), base.Apply(expression));
                case "not-equals":
                    if (Case == GridFilterCase.Original)
                        return base.Apply(expression);
                    if (Values.Any(string.IsNullOrEmpty))
                        return Expression.AndAlso(Apply(expression, null), base.Apply(expression));
                    return Expression.OrElse(Expression.Equal(expression, Null), base.Apply(expression));
                case "equals":
                    if (Case == GridFilterCase.Original)
                        return base.Apply(expression);
                    if (Values.Any(string.IsNullOrEmpty))
                        return Expression.OrElse(Apply(expression, null), base.Apply(expression));
                    return Expression.AndAlso(Expression.NotEqual(expression, Null), base.Apply(expression));
            }
            return base.Apply(expression);
        }

        protected override Expression? Apply(Expression expression, string? value)
        {
            return Method switch
            {
                "not-equals" => string.IsNullOrEmpty(value)
                    ? Expression.AndAlso(Expression.NotEqual(expression, Null), Expression.NotEqual(expression, Empty))
                    : Expression.NotEqual(ConvertCase(expression), ConvertCase(value)),
                "equals" => String.IsNullOrEmpty(value)
                    ? Expression.OrElse(Expression.Equal(expression, Null), Expression.Equal(expression, Empty))
                    : Expression.Equal(ConvertCase(expression), ConvertCase(value)),
                "starts-with" => Expression.Call(ConvertCase(expression), StartsWith, ConvertCase(value)),
                "ends-with" => Expression.Call(ConvertCase(expression), EndsWith, ConvertCase(value)),
                "contains" => Expression.Call(ConvertCase(expression), Contains, ConvertCase(value)),
                _ => null
            };
        }
        protected Expression ConvertCase(Expression expression)
        {
            return Case switch
            {
                GridFilterCase.Upper => Expression.Call(expression, ToUpper),
                GridFilterCase.Lower => Expression.Call(expression, ToLower),
                _ => expression
            };
        }
        protected Expression ConvertCase(string? value)
        {
            return Expression.Constant(Case switch
            {
                GridFilterCase.Upper => value?.ToUpper(),
                GridFilterCase.Lower => value?.ToLower(),
                _ => value
            });
        }
    }
}
