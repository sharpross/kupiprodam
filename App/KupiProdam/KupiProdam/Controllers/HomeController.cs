namespace KupiProdam.Controllers
{
    using KupiProdam.Core;
    using KupiProdam.Utils;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;

    public class HomeController : Controller, IBaseController
    {
        private Breadcrumbs Breadcrumbs { get; set; }

        public string Title 
        { 
            get 
            {
                return Constants.Cotrollers.Title_Home; 
            } 
        }

        public HomeController()
            : base()
        {
            this.Breadcrumbs = new Breadcrumbs();
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = this.Breadcrumbs.GetBreadcrumbs("Home", "Index");
            this.ViewBag.TopContent = this.GetTopContent();

            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = this.Breadcrumbs.GetBreadcrumbs("Home", "About");

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
                Controller = "Home",
                Method = "Index",
                Title = this.Title
            });

            switch (pageName)
            {
                case "About":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = "Home",
                        Method = "About",
                        Title = Constants.Cotrollers.Title_About
                    });
                    break;
                default: break;
            }

            result.Last().IsActive = true;

            return result;
        }
    }
}