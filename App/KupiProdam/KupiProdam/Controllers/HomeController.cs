namespace KupiProdam.Controllers
{
    using KupiProdam.Core;
    using KupiProdam.Utils;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : Controller, IBaseController
    {
        public string Title 
        { 
            get 
            { 
                return "Главная"; 
            } 
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = this.GetBreadcrumbs();

            return View();
        }

        [HttpGet]
        public ActionResult News()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        public List<BreadcrumbsItem> GetBreadcrumbs()
        {
            var result = new List<BreadcrumbsItem>();

            result.Add(new BreadcrumbsItem() 
            { 
                Controller = "Home", 
                Method = "Index",
                Titile = this.Title
            });

            return result;
        }
    }
}