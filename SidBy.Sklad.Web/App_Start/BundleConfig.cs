using System.Web;
using System.Web.Optimization;

namespace SidBy.Sklad.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.cookie.js"
                        , "~/Scripts/jsrender*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js", "~/Scripts/i18n/jquery.ui.datepicker-ru.js"));

            bundles.Add(new ScriptBundle("~/bundles/skladui").Include(
            "~/Scripts/sklad-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // the jqGrid javascript runtime
            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                // language pack - MUST be included before the jqGrid javascript        
                "~/Scripts/trirand/i18n/grid.locale-ru.js",
                // the jqGrid javascript runtime
                "~/Scripts/trirand/jquery.jqGrid*"));

            bundles.Add(new ScriptBundle("~/bundles/plupload").Include(
                  "~/Scripts/plupload/moxie*"
                    , "~/Scripts/plupload/plupload*"
                    , "~/Scripts/plupload/i18n/ru.js"
                  ));
       
            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                "~/Scripts/fileupload/vendor/jquery.ui.widget.js"
                  , "~/Scripts/fileupload/jquery.iframe-transport.js"
                  , "~/Scripts/fileupload/jquery.fileupload.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/redmond/css").Include(
                        "~/Content/themes/redmond/jquery.ui.all.css"
               ));

            bundles.Add(new StyleBundle("~/Content/themes/jqgrid/css").Include(
                        "~/Content/themes/ui.jqgrid.css"));
        }
    }
}