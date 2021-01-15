using Microsoft.AspNetCore.Html;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Forged.Grid
{
    public class GridColumn<T, TValue> : IGridColumn<T, TValue> where T : class
    {
        public IGrid<T> Grid { get; set; }

        public string Name { get; set; }
        public object Title { get; set; }
        public string? Format { get; set; }
        public bool IsHidden { get; set; }
        public string CssClasses { get; set; }
        public bool IsEncoded { get; set; }
        public GridProcessorType ProcessorType { get; set; }

        public Func<T, TValue> ExpressionValue { get; set; }
        public Func<T, int, object?>? RenderValue { get; set; }
        public Expression<Func<T, TValue>> Expression { get; set; }

        IGridColumnSort IGridColumn.Sort => Sort;
        IGridColumnSort<T> IGridColumn<T>.Sort => Sort;
        public IGridColumnSort<T, TValue> Sort { get; set; }

        IGridColumnFilter IGridColumn.Filter => Filter;
        public IGridColumnFilter<T, TValue> Filter { get; set; }

        public GridColumn(IGrid<T> grid, Expression<Func<T, TValue>> expression)
        {
            Grid = grid;
            CssClasses = "";
            IsEncoded = true;
            Expression = expression;
            Name = NameFor(expression);
            Title = TitleFor(expression);
            ProcessorType = GridProcessorType.Pre;
            ExpressionValue = expression.Compile();
            Sort = new GridColumnSort<T, TValue>(this);
            Filter = new GridColumnFilter<T, TValue>(this);
        }

        public virtual IQueryable<T> Process(IQueryable<T> items)
        {
            return Filter.Apply(items);
        }
        public virtual IHtmlContent ValueFor(IGridRow<object> row)
        {
            object? value = ColumnValueFor(row);
            if (value == null)
                return HtmlString.Empty;
            if (value is IHtmlContent content)
                return content;
            if (Format != null)
                value = string.Format(Format, value);
            if (IsEncoded)
                return new GridHtmlString(value.ToString());
            return new HtmlString(value.ToString());
        }

        private string TitleFor(Expression<Func<T, TValue>> expression)
        {
            MemberExpression? body = expression.Body as MemberExpression;
            DisplayAttribute? display = body?.Member.GetCustomAttribute<DisplayAttribute>();
            return display?.GetShortName() ?? "";
        }
        private string NameFor(Expression<Func<T, TValue>> expression)
        {
            string text = expression.Body is MemberExpression member ? member.ToString() : "";
            return text.IndexOf('.') > 0 ? text.Substring(text.IndexOf('.') + 1) : text;
        }
        private object? ColumnValueFor(IGridRow<object> row)
        {
            try
            {
                if (RenderValue != null)
                    return RenderValue((T)row.Model, row.Index);
                Type type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
                if (type.IsEnum)
                    return EnumValue(type, ExpressionValue((T)row.Model)!.ToString()!);
                return ExpressionValue((T)row.Model);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        private string? EnumValue(Type type, string value)
        {
            return type
                .GetMember(value)
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? value;
        }
    }
}
