using KupiProdam.Core;
using KupiProdam.Entities.Entites;
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

            return View();
        }

        /// <summary>
        /// Каталог покупателя
        /// </summary>
        /// <returns></returns>
        public ActionResult Catalog()
        {
            return View();
        }

        /// <summary>
        /// Визитка
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public ActionResult Card(int id)
        {
            return View();
        }

        private List<object> GetTopContent()
        {
            var result = new List<object>();

            return result;
        }
    }
}