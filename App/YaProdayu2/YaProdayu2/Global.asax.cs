using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace YaProdayu2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /*void Application_EndRequest(object sender, EventArgs e)
        {
            // if login failed then display user friendly error page.
            if (Response.StatusCode == 401)
            {
                Response.ClearContent();
                Server.Transfer("~/Error/Unauthorised");
            }

            if (Response.StatusCode == 404)
            {
                Response.ClearContent();
                Server.Transfer("~/Error/NotFound");
            }

            if (Response.StatusCode == 500)
            {
                Response.ClearContent();
                Server.Transfer("~/Error/Error");
            }
        }*/
    }
}
