using System.Web.Optimization;

namespace Charltone.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
             bundles.Add(new ScriptBundle("~/bundles/jquery").Include( 
                  "~/Scripts/js/jquery-{version}.js",
                  "~/Scripts/js/jquery-ui.js",
                  "~/Scripts/js/jquery.blockUI.js")); 

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include( 
                  "~/Scripts/js/jquery.unobtrusive*", 
                  "~/Scripts/js/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/charltone").Include(
                  "~/Scripts/Instrument/Index.js",
                  "~/Scripts/Instrument/Detail.js",
                  "~/Scripts/Page/Position.js",
                  "~/Scripts/Metrics/GoogleAnalytics.js",
                  "~/Scripts/Admin/Login.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                   "~/Content/favicon.png",
                   "~/Content/favicon.ico",
                   "~/Content/css/jquery-ui.css",
                   "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
