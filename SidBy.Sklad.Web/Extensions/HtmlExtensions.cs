using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SidBy.Sklad.Web
{
    public static class HtmlExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(
            this IEnumerable<T> items,
            Func<T, string> nameSelector,
            Func<T, string> valueSelector,
            Func<T, bool> selected)
        {
            return items.OrderBy(item => nameSelector(item))
                .Select(item => new SelectListItem {
                    Selected = selected(item),
                    Text = nameSelector(item),
                    Value = valueSelector(item)
                });
        }

        //public static IEnumerable<SelectListItem> ToSelectListItems<T>(
        //    this IEnumerable<T> items, int? selectedId)
        //{
        //    return
        //        items.OrderBy(item => item.Name)
        //        .Select(item =>
        //                new SelectListItem
        //                {
        //                    Selected = (item.Id == selectedId),
        //                    Text = item.Name,
        //                    Value = item.Id.ToString()
        //                });
        //}
    }
}