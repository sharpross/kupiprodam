﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YaProdayu2.Models.Entities;
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
                this.ViewBag.UserLogin = this.Auth.CurrentUser.Login;
                this.ViewBag.UserEmail = this.Auth.CurrentUser.Email;
                this.ViewBag.UserName = this.Auth.CurrentUser.Name;
                this.ViewBag.IsSeller = this.Auth.CurrentUser.IsSeller;
            }

 	        base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (this.Auth.CurrentUser != null && this.Auth.CurrentUser.IsSeller)
            {
                ViewBag.NeedSubs = false;

                if (this.NeedSubs()) 
                {
                    ViewBag.NeedSubs = true;

                    if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Account" &&
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Image")
                    {
                         filterContext.Result = this.RedirectToAction("Profile", "Account");
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }

        private bool NeedSubs()
        {
            if (this.Auth.CurrentUser != null)
            {
                var subsciptions = 0;

                using (var session = DBHelper.OpenSession())
                {
                    subsciptions = session.CreateCriteria<Subsciptions>().List<Subsciptions>()
                        .Where(rec => rec.UserId == this.Auth.CurrentUser.Id)
                        .ToList()
                        .Count;
                }

                return subsciptions == 0;
            }

            return false;
        }
    }
}