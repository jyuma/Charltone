using System.Web.Optimization;

namespace Charltone.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/CommonStyles").Include(
                "~/Content/css/*.css",
                "~/Content/site.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Modernizr").Include(
                "~/Scripts/modernizr-{version}"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jquery.fileupload.js",
                "~/Scripts/jquery.fileupload-ui.js",
                "~/Scripts/jquery.fileupload-process.js",
                "~/Scripts/jquery.fileupload-validate.js",
                "~/Scripts/jquery.fileupload-image.js",
                "~/Scripts/canvas-to-blob.js",
                "~/Scripts/jquery.fileupload-video.js",
                "~/Scripts/jquery.fileupload-audio.js",
                "~/Scripts/jquery.jquery.iframe-transport.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/CommonScripts").Include(
                "~/Scripts/load-image.all.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/cors/*.js",
                "~/Scripts/vendor/*.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/CustomScripts").Include(
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
