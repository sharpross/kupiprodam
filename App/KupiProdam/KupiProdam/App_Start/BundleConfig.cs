using System.Web;
using System.Web.Optimization;

namespace KupiProdam
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/static/css").Include(
                        "~/Content/static/theme/css/global-style.css",
                        "~/Content/static/theme/assets/fancybox/jquery.fancybox.css"));

            bundles.Add(new ScriptBundle("~/Content/static/js").Include(
                        "~/Content/static/jquery/jquery.js",
                        "~/Content/static/theme/assets/bootstrap/js/bootstrap.min.js",
                        "~/Content/static/theme/js/html5shiv.js",
                        "~/Content/static/theme/js/jquery.cookie.js",
                        "~/Content/static/theme/js/jquery.easing.js",
                        "~/Content/static/theme/js/jquery.mousewheel-3.0.6.pack.js",
                        "~/Content/static/theme/js/jquery.nod.validation.js",
                        "~/Content/static/theme/js/jquery.wp.custom.js",
                        "~/Content/static/theme/js/jquery.wp.switcher.js",
                        "~/Content/static/theme/js/less.js",
                        "~/Content/static/theme/js/modernizr.custom.js",
                        "~/Content/static/theme/js/respond.min.js",

                        "~/Content/static/theme/assets/fancybox/jquery.fancybox.js",
                        "~/Content/static/theme/assets/hover-dropdown/bootstrap-hover-dropdown.min.js",
                        "~/Content/static/theme/assets/masonry/masonry.js",
                        "~/Content/static/theme/assets/page-scroller/jquery.ui.totop.min.js",
                        "~/Content/static/theme/assets/mixitup/jquery.mixitup.js",
                        "~/Content/static/theme/assets/mixitup/jquery.mixitup.init.js",
                        "~/Content/static/theme/assets/fancybox/jquery.fancybox.pack.js?v=2.1.5",
                        "~/Content/static/theme/assets/easy-pie-chart/jquery.easypiechart.js",
                        "~/Content/static/theme/assets/easy-pie-chart/jquery.easypiechart.js",
                        "~/Content/static/theme/assets/waypoints/waypoints.min.js",
                        "~/Content/static/theme/assets/sticky/jquery.sticky.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/app/css").Include(
                        "~/Content/app/styles/styles.css"));

            bundles.Add(new ScriptBundle("~/Content/app/js").Include(
                        "~/Content/app/scripts/common.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
