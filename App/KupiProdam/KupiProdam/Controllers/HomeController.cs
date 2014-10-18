namespace KupiProdam.Controllers
{
    using KupiProdam.Core;
    using KupiProdam.Utils;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;

    public class HomeController : Controller, IBaseController
    {
        public string Title 
        { 
            get 
            {
                return ConstantsKP.Cotrollers.Title_Home; 
            } 
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Home", "Index");
            this.ViewBag.TopContent = this.GetTopContent();
            
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Home", "About");

            return View();
        }

        [NonAction]
        private List<object> GetTopContent()
        {
            var result = new List<object>();

            return result;
        }
    }
}