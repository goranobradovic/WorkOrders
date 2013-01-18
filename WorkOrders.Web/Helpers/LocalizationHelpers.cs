using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace WorkOrders.Web.Helpers
{
    public static class LocalizationHelpers
    {
        public static string DateFormat(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(Localize.DateFormat))
            {
                return CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            }
            return Localize.DateFormat;
        }
    }
}