using System.Web;
using System.Web.Mvc;
using WorkOrders.Web.Filters;

namespace WorkOrders.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SetUserCultureAttribute());
        }
    }
}