namespace SidBy.Sklad.Web
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> nameSelector, Func<T, string> valueSelector, Func<T, bool> selected)
        {
            return (from item in items
                orderby nameSelector(item)
                select new SelectListItem { Selected = selected(item), Text = nameSelector(item), Value = valueSelector(item) });
        }
    }
}

