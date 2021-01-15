using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Forged.Grid
{
    public interface IGridFilters
    {
        Func<string> BooleanTrueOptionText { get; set; }
        Func<string> BooleanFalseOptionText { get; set; }
        Func<string> BooleanEmptyOptionText { get; set; }

        IGridFilter? Create(Type type, string method, StringValues values);
        IEnumerable<SelectListItem> OptionsFor<T, TValue>(IGridColumn<T, TValue> column);

        void Register(Type type, string method, Type filter);
        void Unregister(Type type, string method);
    }
}
