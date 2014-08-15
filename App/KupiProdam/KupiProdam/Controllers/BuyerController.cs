using KupiProdam.Core;
using KupiProdam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KupiProdam.Controllers
{
    public class BuyerController : Controller, IBaseController
    {
        public string Title
        {
            get { return Constants.Cotrollers.Title_Buyers; }
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Buyer", "Index");
            this.ViewBag.TopContent = this.GetTopContent();

            return View();
        }


        public ActionResult Description()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Buyer", "Description");
            this.ViewBag.TopContent = this.GetTopContent();

            return View();
        }

        private List<object> GetTopContent()
        {
            var result = new List<object>();

            return result;
        }
    }
}