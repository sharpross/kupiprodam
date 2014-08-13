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
            this.ViewBag.Breadcrumbs = this.GetBreadcrumbs(string.Empty);
            this.ViewBag.TopContent = this.GetTopContent();

            return View();
        }

        [HttpGet]
        public ActionResult Catalog(int? page)
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = this.GetBreadcrumbs("Catalog");

            return View();
        }

        private List<object> GetTopContent()
        {
            var result = new List<object>();

            return result;
        }

        public List<BreadcrumbsItem> GetBreadcrumbs(string pageName)
        {
            var result = new List<BreadcrumbsItem>();

            result.Add(new BreadcrumbsItem()
            {
                Controller = "Buyer",
                Method = "Index",
                Title = this.Title
            });

            switch (pageName)
            {
                case "Catalog":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = "Buyer",
                        Method = "Catalog",
                        Title = Constants.Cotrollers.Title_Catalog
                    });
                    break;
                default: break;
            }

            return result;
        }
    }
}