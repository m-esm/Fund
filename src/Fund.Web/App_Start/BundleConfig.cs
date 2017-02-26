using System.Web;
using System.Web.Optimization;

namespace Fund.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Css/Bundle").Include(
               "~/Css/materialize.css",
               "~/Css/materialize_RTL.css",
               "~/Css/slicebox.css",
              "~/Css/PagedList.css",
              "~/Css/style.css"));

            //            <link href="~/Css/materialize.css" rel="stylesheet" />
            //<link href="~/Css/materialize_RTL.css" rel="stylesheet" />
            //<link href="~/Css/slicebox.css" rel="stylesheet" />
            //<link href="~/Css/PagedList.css" rel="stylesheet" />
            //<link href="~/Css/style.css" rel="stylesheet" />


            bundles.Add(new ScriptBundle("~/Scripts/Bundle").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.maskMoney.js",
                        "~/Scripts/materialize/materialize.js",
                        "~/Scripts/jquery.slicebox.js",
                        "~/Scripts/myjq.js"));


            bundles.Add(new ScriptBundle("~/Scripts/ChartJs/Bundle").Include(
                    "~/Scripts/chartjs/Chart.Core.js",
                    "~/Scripts/chartjs/Chart.Bar.js",
                    "~/Scripts/chartjs/Chart.Line.js"));


            //          <script src="~/Scripts/modernizr-2.8.3.js"></script>
            //<script src="~/Scripts/jquery-2.1.4.js"></script>
            //<script src="~/Scripts/materialize/materialize.js"></script>
            //<script src="~/Scripts/jquery.slicebox.js"></script>
            //<script src="~/Scripts/myjq.js"></script>


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



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

            BundleTable.EnableOptimizations = false;
        }
    }
}