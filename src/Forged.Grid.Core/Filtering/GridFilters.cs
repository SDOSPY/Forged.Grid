using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forged.Grid
{
    public class GridFilters : IGridFilters
    {
        public Func<string> BooleanTrueOptionText { get; set; }
        public Func<string> BooleanFalseOptionText { get; set; }
        public Func<string> BooleanEmptyOptionText { get; set; }

        private Dictionary<Type, IDictionary<string, Type>> Filters
        {
            get;
        }

        public GridFilters()
        {
            BooleanEmptyOptionText = () => "";
            BooleanTrueOptionText = () => "True";
            BooleanFalseOptionText = () => "False";
            Filters = new Dictionary<Type, IDictionary<string, Type>>();
            Register(typeof(sbyte), "equals", typeof(NumberFilter<sbyte>));
            Register(typeof(sbyte), "not-equals", typeof(NumberFilter<sbyte>));
            Register(typeof(sbyte), "less-than", typeof(NumberFilter<sbyte>));
            Register(typeof(sbyte), "greater-than", typeof(NumberFilter<sbyte>));
            Register(typeof(sbyte), "less-than-or-equal", typeof(NumberFilter<sbyte>));
            Register(typeof(sbyte), "greater-than-or-equal", typeof(NumberFilter<sbyte>));
            Register(typeof(byte), "equals", typeof(NumberFilter<byte>));
            Register(typeof(byte), "not-equals", typeof(NumberFilter<byte>));
            Register(typeof(byte), "less-than", typeof(NumberFilter<byte>));
            Register(typeof(byte), "greater-than", typeof(NumberFilter<byte>));
            Register(typeof(byte), "less-than-or-equal", typeof(NumberFilter<byte>));
            Register(typeof(byte), "greater-than-or-equal", typeof(NumberFilter<byte>));
            Register(typeof(short), "equals", typeof(NumberFilter<short>));
            Register(typeof(short), "not-equals", typeof(NumberFilter<short>));
            Register(typeof(short), "less-than", typeof(NumberFilter<short>));
            Register(typeof(short), "greater-than", typeof(NumberFilter<short>));
            Register(typeof(short), "less-than-or-equal", typeof(NumberFilter<short>));
            Register(typeof(short), "greater-than-or-equal", typeof(NumberFilter<short>));
            Register(typeof(ushort), "equals", typeof(NumberFilter<ushort>));
            Register(typeof(ushort), "not-equals", typeof(NumberFilter<ushort>));
            Register(typeof(ushort), "less-than", typeof(NumberFilter<ushort>));
            Register(typeof(ushort), "greater-than", typeof(NumberFilter<ushort>));
            Register(typeof(ushort), "less-than-or-equal", typeof(NumberFilter<ushort>));
            Register(typeof(ushort), "greater-than-or-equal", typeof(NumberFilter<ushort>));
            Register(typeof(int), "equals", typeof(NumberFilter<int>));
            Register(typeof(int), "not-equals", typeof(NumberFilter<int>));
            Register(typeof(int), "less-than", typeof(NumberFilter<int>));
            Register(typeof(int), "greater-than", typeof(NumberFilter<int>));
            Register(typeof(int), "less-than-or-equal", typeof(NumberFilter<int>));
            Register(typeof(int), "greater-than-or-equal", typeof(NumberFilter<int>));
            Register(typeof(uint), "equals", typeof(NumberFilter<uint>));
            Register(typeof(uint), "not-equals", typeof(NumberFilter<uint>));
            Register(typeof(uint), "less-than", typeof(NumberFilter<uint>));
            Register(typeof(uint), "greater-than", typeof(NumberFilter<uint>));
            Register(typeof(uint), "less-than-or-equal", typeof(NumberFilter<uint>));
            Register(typeof(uint), "greater-than-or-equal", typeof(NumberFilter<uint>));
            Register(typeof(long), "equals", typeof(NumberFilter<long>));
            Register(typeof(long), "not-equals", typeof(NumberFilter<long>));
            Register(typeof(long), "less-than", typeof(NumberFilter<long>));
            Register(typeof(long), "greater-than", typeof(NumberFilter<long>));
            Register(typeof(long), "less-than-or-equal", typeof(NumberFilter<long>));
            Register(typeof(long), "greater-than-or-equal", typeof(NumberFilter<long>));
            Register(typeof(ulong), "equals", typeof(NumberFilter<ulong>));
            Register(typeof(ulong), "not-equals", typeof(NumberFilter<ulong>));
            Register(typeof(ulong), "less-than", typeof(NumberFilter<ulong>));
            Register(typeof(ulong), "greater-than", typeof(NumberFilter<ulong>));
            Register(typeof(ulong), "less-than-or-equal", typeof(NumberFilter<ulong>));
            Register(typeof(ulong), "greater-than-or-equal", typeof(NumberFilter<ulong>));
            Register(typeof(float), "equals", typeof(NumberFilter<float>));
            Register(typeof(float), "not-equals", typeof(NumberFilter<float>));
            Register(typeof(float), "less-than", typeof(NumberFilter<float>));
            Register(typeof(float), "greater-than", typeof(NumberFilter<float>));
            Register(typeof(float), "less-than-or-equal", typeof(NumberFilter<float>));
            Register(typeof(float), "greater-than-or-equal", typeof(NumberFilter<float>));
            Register(typeof(double), "equals", typeof(NumberFilter<double>));
            Register(typeof(double), "not-equals", typeof(NumberFilter<double>));
            Register(typeof(double), "less-than", typeof(NumberFilter<double>));
            Register(typeof(double), "greater-than", typeof(NumberFilter<double>));
            Register(typeof(double), "less-than-or-equal", typeof(NumberFilter<double>));
            Register(typeof(double), "greater-than-or-equal", typeof(NumberFilter<double>));
            Register(typeof(decimal), "equals", typeof(NumberFilter<decimal>));
            Register(typeof(decimal), "not-equals", typeof(NumberFilter<decimal>));
            Register(typeof(decimal), "less-than", typeof(NumberFilter<decimal>));
            Register(typeof(decimal), "greater-than", typeof(NumberFilter<decimal>));
            Register(typeof(decimal), "less-than-or-equal", typeof(NumberFilter<decimal>));
            Register(typeof(decimal), "greater-than-or-equal", typeof(NumberFilter<decimal>));
            Register(typeof(DateTime), "equals", typeof(DateTimeFilter));
            Register(typeof(DateTime), "not-equals", typeof(DateTimeFilter));
            Register(typeof(DateTime), "earlier-than", typeof(DateTimeFilter));
            Register(typeof(DateTime), "later-than", typeof(DateTimeFilter));
            Register(typeof(DateTime), "earlier-than-or-equal", typeof(DateTimeFilter));
            Register(typeof(DateTime), "later-than-or-equal", typeof(DateTimeFilter));
            Register(typeof(bool), "equals", typeof(BooleanFilter));
            Register(typeof(bool), "not-equals", typeof(BooleanFilter));
            Register(typeof(string), "equals", typeof(StringFilter));
            Register(typeof(string), "not-equals", typeof(StringFilter));
            Register(typeof(string), "contains", typeof(StringFilter));
            Register(typeof(string), "ends-with", typeof(StringFilter));
            Register(typeof(string), "starts-with", typeof(StringFilter));
            Register(typeof(Enum), "equals", typeof(EnumFilter));
            Register(typeof(Enum), "not-equals", typeof(EnumFilter));
            Register(typeof(Guid), "equals", typeof(GuidFilter));
            Register(typeof(Guid), "not-equals", typeof(GuidFilter));
        }

        public virtual IGridFilter? Create(Type type, string method, StringValues values)
        {
            if (Get(Nullable.GetUnderlyingType(type) ?? type, method) is Type filterType)
            {
                IGridFilter filter = (IGridFilter)Activator.CreateInstance(filterType);
                filter.Method = method.ToLower();
                filter.Values = values;
                return filter;
            }
            return null;
        }
        public virtual IEnumerable<SelectListItem> OptionsFor<T, TValue>(IGridColumn<T, TValue> column)
        {
            Type? type = GetElementType(typeof(TValue)) ?? typeof(TValue);
            List<SelectListItem> options = new List<SelectListItem>();
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type == typeof(bool))
            {
                options.Add(new SelectListItem { Value = "", Text = BooleanEmptyOptionText?.Invoke() });
                options.Add(new SelectListItem { Value = "true", Text = BooleanTrueOptionText?.Invoke() });
                options.Add(new SelectListItem { Value = "false", Text = BooleanFalseOptionText?.Invoke() });
            }
            else if (type.IsEnum)
            {
                options.Add(new SelectListItem());
                IEnumerable<SelectListItem>? items = column.Grid
                    .ViewContext?
                    .HttpContext
                    .RequestServices
                    .GetRequiredService<IHtmlHelper>()
                    .GetEnumSelectList(type)
                    .OrderBy(item => item.Text);
                if (items != null)
                    options.AddRange(items);
            }
            return options;
        }

        public void Register(Type type, string method, Type filter)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (!Filters.ContainsKey(type))
                Filters[type] = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            Filters[type][method] = filter;
        }
        public void Unregister(Type type, string method)
        {
            if (Filters.ContainsKey(type))
                Filters[type].Remove(method);
        }

        private bool TryGet(Type type, string method, out Type? filter)
        {
            if (Filters.ContainsKey(type) && Filters[type].ContainsKey(method))
                filter = Filters[type][method];
            else
                filter = null;
            return filter != null;
        }
        private Type? Get(Type type, string method)
        {
            if (TryGet(type, method, out Type? filter))
                return filter;
            if (GetElementType(type) is Type elementType)
            {
                if (TryGet(elementType, method, out filter))
                    return typeof(EnumerableFilter<>).MakeGenericType(filter);
                if (elementType.IsEnum && TryGet(typeof(Enum), method, out filter))
                    return typeof(EnumerableFilter<>).MakeGenericType(filter);
            }
            else if (type.IsEnum && TryGet(typeof(Enum), method, out filter))
            {
                return filter;
            }
            return null;
        }
        private Type? GetElementType(Type type)
        {
            if (type == typeof(string))
                return null;
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];
            foreach (Type interfaceType in type.GetInterfaces())
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return interfaceType.GetGenericArguments()[0];
            return null;
        }
    }
}
