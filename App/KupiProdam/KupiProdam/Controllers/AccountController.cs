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
            get { return Constants.Cotrollers.Title_Account; }
        }

        // GET: Account
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = this.GetBreadcrumbs(string.Empty);

            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Breadcrumbs = this.GetBreadcrumbs("Registration");

            return View();
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            this.ViewBag.Titile = this.Title;
            this.ViewBag.Pagination = this.GetBreadcrumbs("UserProfile");

            return View();
        }

        public List<BreadcrumbsItem> GetBreadcrumbs(string pageName)
        {
            var result = new List<BreadcrumbsItem>();

            result.Add(new BreadcrumbsItem()
            {
                Controller = "Seller",
                Method = "Index",
                Title = this.Title
            });

            switch (pageName)
            {
                case "UserProfile":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = "Account",
                        Method = "Profile",
                        Title = "Профиль"
                    });
                    break;
                case "Registration":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = "Account",
                        Method = "Registration",
                        Title = "Регистрация"
                    });
                    break;
                default: break;
            }

            return result;
        }
    }
}