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
            get { return "Продавцы"; }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? page)
        {
            return View();
        }

        public List<BreadcrumbsItem> GetBreadcrumbs()
        {
            throw new NotImplementedException();
        }
    }
}