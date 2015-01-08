using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YaProdayu2.Y2System.Secutity;

namespace YaProdayu2.Y2System
{
    public class BaseController : Controller
    {
        public Y2Authentication Auth { get; set; }

        public BaseController() : base()
        {
            this.Auth = new Y2Authentication();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Auth.HttpContext = filterContext.HttpContext;

            this.ViewBag.IsAuth = false;

            if (this.Auth.CurrentUser != null)
            {
                this.ViewBag.IsAuth = true;
                this.ViewBag.UserLogin = this.Auth.CurrentUser.Email;
                this.ViewBag.UserName = this.Auth.CurrentUser.Name;
                this.ViewBag.IsSeller = this.Auth.CurrentUser.IsSeller;
            }

 	        base.OnActionExecuting(filterContext);
        }

    }
}