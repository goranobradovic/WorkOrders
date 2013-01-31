using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using WorkOrders.Web.Models;

namespace WorkOrders.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SetUserCultureAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                using (var db = new UsersContext())
                {
                    UserProfile user =
                        db.UserProfiles.Find(WebSecurity.GetUserId(filterContext.HttpContext.User.Identity.Name));
                    if (user != null && user.CultureId.HasValue)
                    {
                        var culture = CultureInfo.GetCultureInfo(user.CultureId.Value);
                        Thread.CurrentThread.CurrentUICulture = culture;
                        Thread.CurrentThread.CurrentCulture = culture;
                    }
                }
            }
        }
    }
}