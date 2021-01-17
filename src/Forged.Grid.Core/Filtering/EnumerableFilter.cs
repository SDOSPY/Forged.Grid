using Microsoft.Extensions.Primitives;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Forged.Grid
{
    public class EnumerableFilter<T> : IGridFilter where T : IGridFilter
    {
        private T Filter { get; }

        public string? Method
        {
            get
            {
                return Filter.Method;
            }
            set
            {
                Filter.Method = value;
            }
        }
        public StringValues Values
        {
            get
            {
                return Filter.Values;
            }
            set
            {
                Filter.Values = value;
            }
        }
        public GridFilterCase Case
        {
            get
            {
                return Filter.Case;
            }
            set
            {
                Filter.Case = value;
            }
        }

        public EnumerableFilter()
        {
            Filter = Activator.CreateInstance<T>();
        }
        public Expression? Apply(Expression expression)
        {
            if (GetFilterable(expression.Type) is Type type)
            {
                ParameterExpression parameter = Expression.Parameter(type, "x");
                Expression? filter = Filter.Apply(Expression.Lambda(parameter, parameter).Body);
                MethodInfo any = typeof(Enumerable).GetMethods()
                    .First(method =>
                        method.Name == nameof(Enumerable.Any) &&
                        method.GetParameters().Length == 2)
                    .MakeGenericMethod(type);
                return Expression.Call(any, expression, Expression.Lambda(filter, parameter));
            }
            return null;
        }

        private Type? GetFilterable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];
            foreach (Type interfaceType in type.GetInterfaces())
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return interfaceType.GetGenericArguments()[0];
            return null;
        }
    }
}
