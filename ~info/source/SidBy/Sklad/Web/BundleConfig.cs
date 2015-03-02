namespace SidBy.Sklad.Web
{
    using System;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(new string[] { "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.cookie.js" }));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(new string[] { "~/Scripts/jquery-ui-{version}.js", "~/Scripts/i18n/jquery.ui.datepicker-ru.js" }));
            bundles.Add(new ScriptBundle("~/bundles/skladui").Include("~/Scripts/sklad-ui-{version}.js", new IItemTransform[0]));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(new string[] { "~/Scripts/jquery.unobtrusive*", "~/Scripts/jquery.validate*" }));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*", new IItemTransform[0]));
            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(new string[] { "~/Scripts/trirand/i18n/grid.locale-ru.js", "~/Scripts/trirand/jquery.jqGrid*" }));
            bundles.Add(new ScriptBundle("~/bundles/plupload").Include(new string[] { "~/Scripts/plupload/moxie*", "~/Scripts/plupload/plupload*", "~/Scripts/plupload/i18n/ru.js" }));
            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(new string[] { "~/Scripts/fileupload/vendor/jquery.ui.widget.js", "~/Scripts/fileupload/jquery.iframe-transport.js", "~/Scripts/fileupload/jquery.fileupload.js" }));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css", new IItemTransform[0]));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(new string[] { "~/Content/themes/base/jquery.ui.core.css", "~/Content/themes/base/jquery.ui.resizable.css", "~/Content/themes/base/jquery.ui.selectable.css", "~/Content/themes/base/jquery.ui.accordion.css", "~/Content/themes/base/jquery.ui.autocomplete.css", "~/Content/themes/base/jquery.ui.button.css", "~/Content/themes/base/jquery.ui.dialog.css", "~/Content/themes/base/jquery.ui.slider.css", "~/Content/themes/base/jquery.ui.tabs.css", "~/Content/themes/base/jquery.ui.datepicker.css", "~/Content/themes/base/jquery.ui.progressbar.css", "~/Content/themes/base/jquery.ui.theme.css" }));
            bundles.Add(new StyleBundle("~/Content/themes/redmond/css").Include("~/Content/themes/redmond/jquery.ui.all.css", new IItemTransform[0]));
            bundles.Add(new StyleBundle("~/Content/themes/jqgrid/css").Include("~/Content/themes/ui.jqgrid.css", new IItemTransform[0]));
        }
    }
}

