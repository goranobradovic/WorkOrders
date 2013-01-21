using System;
using System.Linq.Expressions;

namespace WorkOrders.Web.Helpers
{
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Resource = Resources.Localize;

    public static class LocalizationHelpers
    {
        public static string DateFormat(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(Resource.DateFormat))
            {
                return CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            }
            return Resource.DateFormat;
        }

        public static IHtmlString DataForAttr(this HtmlHelper helper, string key = null, string attribute = "text")
        {
            return helper.Raw(string.Format("data-for-{0}='{1}'", attribute, key ?? helper.IdForModel().ToString()));
        }

        public static IHtmlString DataForAttr<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string attribute = "text")
        {
            return helper.Raw(string.Format("data-for-{0}='{1}'", attribute, helper.IdFor(expression)));
        }

        public static IHtmlString DataForTitle(this HtmlHelper helper, string key = null)
        {
            return helper.DataForAttr(key, "title");
        }

        public static string Localize(this HtmlHelper helper, string value = null)
        {
            return Resource.ResourceManager.GetString(value ?? helper.IdForModel().ToString()) ?? value ?? helper.NameForModel().ToString();
        }

        public static string Localize<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return helper.Localize(helper.IdFor(expression).ToString());
        }
    }
}