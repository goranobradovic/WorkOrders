using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace WorkOrders.Web
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-1.*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js"
                ));

            var styleBundle = new StyleBundle("~/content/css").Include(
                "~/Content/less/bootstrap.less");
            styleBundle.Transforms.Add(new BundleTransformer.Core.Transformers.CssTransformer());
            bundles.Add(styleBundle);
            
            var responsiveBundle = new StyleBundle("~/content/css-responsive").Include(
                "~/Content/less/responsive.less"
                //"~/Content/less/customizations.less"
                );
            responsiveBundle.Transforms.Add(new BundleTransformer.Core.Transformers.CssTransformer());
            bundles.Add(responsiveBundle);
        }
    }
}