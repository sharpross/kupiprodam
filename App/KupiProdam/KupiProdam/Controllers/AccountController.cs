using KupiProdam.Core;
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
            get { return "Учётная запись"; }
        }

        // GET: Account
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var listPagination = this.GetBreadcrumbs();

            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = listPagination;

            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            var listPagination = this.GetBreadcrumbs();
            listPagination.Add(new BreadcrumbsItem()
            {
                Controller = "Account",
                Method = "Registration",
                Titile = "Регистрация"
            });

            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = listPagination;

            return View();
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            var listPagination = this.GetBreadcrumbs();
            listPagination.Add(new BreadcrumbsItem()
            {
                Controller = "Account",
                Method = "Profile",
                Titile = "Профиль"
            });

            this.ViewBag.Titile = this.Title;
            this.ViewBag.Pagination = listPagination;

            return View();
        }

        public ActionResult SellerProfile()
        {
            return View();
        }

        public ActionResult BuyerProfile()
        {
            return View();
        }

        public List<BreadcrumbsItem> GetBreadcrumbs()
        {
            var result = new List<BreadcrumbsItem>();

            result.Add(new BreadcrumbsItem() 
            {
                Controller = "Account",
                Method = "Index",
                Titile = this.Title
            });

            return result;
        }
    }
}