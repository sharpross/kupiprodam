using KupiProdam.Core;
using KupiProdam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KupiProdam.Controllers
{
    public class SellerController : Controller, IBaseController
    {
        public string Title
        {
            get { return Constants.Cotrollers.Title_Sallers; }
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Seller", "Index");

            return View();
        }

        [HttpGet]
        public ActionResult Catalog(int? page)
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Seller", "Catalog");

            return View();
        }
    }
}