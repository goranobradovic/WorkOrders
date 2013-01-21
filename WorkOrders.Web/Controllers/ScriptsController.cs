namespace WorkOrders.Web.Controllers
{
    
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Web.Mvc;
    using Resource = Resources.Localize;

    public class ScriptsController : Controller
    {
        private static readonly string[] JsonAcceptTypes = { "application/json", "text/javascript" };

        //
        // GET: /Scripts/
        [OutputCache(CacheProfile = "ScriptResources")]
        public ActionResult Resources(string id)
        {
            // get culture based on passed argument, or default if none
            var culture = GetCultureInfoForLanguage(id);

            // dictionary with resource key/value pairs
            var result = CreateDictionaryFromResourceSet(culture, Resource.ResourceManager);

            if (Request.AcceptTypes != null && Request.AcceptTypes.Intersect(JsonAcceptTypes).Any())
            {
                // json request, return json result
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            // standard request, return view
            return View(result);
        }

        /// <summary>
        /// Creates dictionary with key value pairs from resources in resourceManager.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <returns>Dictionary with all resource keys and values</returns>
        private static Dictionary<string, string> CreateDictionaryFromResourceSet(CultureInfo culture, ResourceManager resourceManager)
        {
            var result = new Dictionary<string, string>();
            var resources = resourceManager.GetResourceSet(culture, true, true).GetEnumerator();
            while (resources.MoveNext())
            {
                if (resources.Key is string && resources.Value is string)
                {
                    result.Add((string)resources.Key, (string)resources.Value);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the culture info for language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns>CultureInfo for language, or current if language is not valid</returns>
        private static CultureInfo GetCultureInfoForLanguage(string language = null)
        {
            if (!string.IsNullOrEmpty(language))
            {
                try
                {
                    return CultureInfo.GetCultureInfo(language);
                }
                catch
                {
                }
            }
            return CultureInfo.CurrentUICulture;
        }
    }
}
