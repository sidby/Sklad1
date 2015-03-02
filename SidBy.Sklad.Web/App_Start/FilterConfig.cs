using SidBy.Sklad.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace SidBy.Sklad.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}