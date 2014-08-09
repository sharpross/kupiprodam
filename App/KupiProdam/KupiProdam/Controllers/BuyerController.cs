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
            get { return "Покупатели"; }
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = this.GetBreadcrumbs();

            return View();
        }

        [HttpGet]
        public ActionResult List(int? page)
        {
            var listPagination = this.GetBreadcrumbs();
            listPagination.Add(new BreadcrumbsItem()
            {
                Controller = "Buyer",
                Method = "List",
                Titile = "Список покупателей"
            });

            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = listPagination;

            return View();
        }

        public List<BreadcrumbsItem> GetBreadcrumbs()
        {
            var result = new List<BreadcrumbsItem>();

            result.Add(new BreadcrumbsItem()
            {
                Controller = "Buyer",
                Method = "Index",
                Titile = this.Title
            });

            return result;
        }
    }
}