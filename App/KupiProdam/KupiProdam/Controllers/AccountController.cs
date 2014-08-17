﻿using KupiProdam.Core;
using KupiProdam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KupiProdam.Controllers
{
    public class AccountController : Controller, IBaseController
    {
        public  string Title
        {
            get { return Constants.Cotrollers.Title_Account; }
        }

        // GET: Account
        [HttpGet]
        [Authorize]
        [OutputCache(Duration=10)]
        public ActionResult Index()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Account", "Index");

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult Registration()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Account", "Registration");
            
            this.ViewBag.HideBreadcrumbs = true;
            this.ViewBag.HideRigthSide = true;
            this.ViewBag.HideLeftSide = true;

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult BuyerRegistration()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Account", "BuyerRegistration");

            this.ViewBag.HideBreadcrumbs = true;
            this.ViewBag.HideRigthSide = true;
            this.ViewBag.HideLeftSide = true;

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult SellerRegistration()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Account", "SellerRegistration");
            
            this.ViewBag.HideBreadcrumbs = true;
            this.ViewBag.HideRigthSide = true;
            this.ViewBag.HideLeftSide = true;

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult Profiles()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Pagination = Breadcrumbs.Get("Account", "Profiles");

            return View();
        }
    }
}