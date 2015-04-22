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
                  "~/Scripts/Site/Site.js",
                  "~/Scripts/File/File.js",
                  "~/Scripts/Instrument/Index.js",
                  "~/Scripts/Instrument/Detail.js",
                  "~/Scripts/Instrument/Zoom.js",
                  "~/Scripts/Page/Position.js",
                  "~/Scripts/Metrics/GoogleAnalytics.js",
                  "~/Scripts/Admin/Login.js"));

            var lessBundle = new Bundle("~/bundles/less").Include(
                "~/Content/site.less", new CssRewriteUrlTransform());

            lessBundle.Transforms.Add(new LessTransform());
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);

            bundles.Add(new StyleBundle("~/bundles/jquery-ui/css")
                   .Include("~/Content/css/*.css"));

            //BundleTable.EnableOptimizations = false;
        }
    }
}
