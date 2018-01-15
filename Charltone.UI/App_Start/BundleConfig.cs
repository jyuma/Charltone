using System.Web.Optimization;

namespace Charltone.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/CommonStyles").Include(
                "~/Content/css/*.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Modernizr").Include(
                "~/Scripts/modernizr-2.*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include(
                "~/Scripts/js/jquery-{version}.js",
                "~/Scripts/js/jquery-ui.js",
                "~/Scripts/js/jquery.unobtrusive*",
                "~/Scripts/js/jquery.validate*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/CommonScripts").Include(
                "~/Scripts/js/jquery.blockUI.js",
                "~/Scripts/js/load-image.all.min.js",
                "~/Scripts/js/jquery.fileupload.js",
                "~/Scripts/js/jquery.fileupload-ui.js",
                "~/Scripts/js/jquery.fileupload-process.js",
                "~/Scripts/js/jquery.fileupload-validate.js",
                "~/Scripts/js/jquery.fileupload-image.js",
                "~/Scripts/js/canvas-to-blob.js",
                "~/Scripts/js/jquery.fileupload-video.js",
                "~/Scripts/js/jquery.fileupload-audio.js",
                "~/Scripts/js/jquery.jquery.iframe-transport.js",
                "~/Scripts/js/bootstrap.js",
                "~/Scripts/js/cors/*.js",
                "~/Scripts/js/vendor/*.js"
                ));

            bundles.Add(new Bundle("~/bundles/CustomScripts").Include(
                "~/Scripts/Site/Site.js",
                "~/Scripts/File/Upload.js",
                "~/Scripts/Error/Error.js",
                "~/Scripts/Page/Position.js",
                "~/Scripts/Home/Index.js",
                "~/Scripts/Instrument/Detail.js",
                "~/Scripts/Instrument/Edit.js",
                "~/Scripts/Instrument/Zoom.js",
                "~/Scripts/Instrument/Carousel.js",
                "~/Scripts/Ordering/Edit.js",
                "~/Scripts/Admin/Login.js",
                "~/Scripts/Contact/Index.js",
                "~/Scripts/Metrics/GoogleAnalytics.js"
                ));

            //BundleTable.EnableOptimizations = false;
        }
    }
}
