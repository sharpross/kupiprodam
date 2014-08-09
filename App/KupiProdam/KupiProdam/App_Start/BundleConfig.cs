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
                        "~/Content/static/bootstrap/css/bootstrap.css",
                        "~/Content/static/bootstrap/css/bootstrap-theme.css"));


            bundles.Add(new ScriptBundle("~/Content/static/js").Include(
                        "~/Content/static/jquery/jquery.js",
                        "~/Content/static/bootstrap/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/app/css").Include(
                        "~/Content/app/styles/styles.css"));


            bundles.Add(new ScriptBundle("~/Content/app/js").Include(
                        "~/Content/app/scripts/common.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
