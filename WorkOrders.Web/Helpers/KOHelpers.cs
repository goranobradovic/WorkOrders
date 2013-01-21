using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkOrders.Web.Helpers
{
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    public static class KOHelpers
    {
        public static IHtmlString DataBindWith<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.Raw(string.Format("data-bind='with: {0}'", ModelMetadata.FromLambdaExpression(expression, html.ViewData).PropertyName));
        }
    }
}