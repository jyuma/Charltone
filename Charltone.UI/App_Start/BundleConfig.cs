using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace Charltone.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            
            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();

            var commonStylesBundle = new Bundle("~/bundles/CommonStyles");
            commonStylesBundle.Include(
                "~/Content/css/*.css",
                "~/Content/site.less");
            commonStylesBundle.Builder = nullBuilder;
            commonStylesBundle.Transforms.Add(styleTransformer);
            bundles.Add(commonStylesBundle);

            var modernizrBundle = new Bundle("~/bundles/Modernizr");
            modernizrBundle.Include("~/Scripts/modernizr-2.*");
            modernizrBundle.Builder = nullBuilder;
            modernizrBundle.Transforms.Add(scriptTransformer);
            bundles.Add(modernizrBundle);

            var jQueryBundle = new Bundle("~/bundles/Jquery");
            jQueryBundle.Include(
                "~/Scripts/js/jquery-{version}.js",
                "~/Scripts/js/jquery-ui.js",
                "~/Scripts/js/jquery.unobtrusive*", 
                "~/Scripts/js/jquery.validate*");
            jQueryBundle.Builder = nullBuilder;
            jQueryBundle.Transforms.Add(scriptTransformer);
            bundles.Add(jQueryBundle);

            var commonScriptsBundle = new Bundle("~/bundles/CommonScripts");
            commonScriptsBundle.Include(
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
                "~/Scripts/js/vendor/*.js");
            commonScriptsBundle.Builder = nullBuilder;
            commonScriptsBundle.Transforms.Add(scriptTransformer);
            bundles.Add(commonScriptsBundle);

            var customScriptsBundle = new Bundle("~/bundles/CustomScripts");
            customScriptsBundle.Include(
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
                "~/Scripts/Metrics/GoogleAnalytics.js");
            customScriptsBundle.Builder = nullBuilder;
            customScriptsBundle.Transforms.Add(scriptTransformer);
            bundles.Add(customScriptsBundle);

            //BundleTable.EnableOptimizations = false;
        }
    }
}
