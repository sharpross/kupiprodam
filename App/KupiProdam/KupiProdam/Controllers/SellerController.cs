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
    public class SellerController : Controller, IBaseController
    {
        private List<Seller> Sellers { get; set; }

        public SellerController()
            : base()
        {
            var generator = new KupiProdam.Utils.Generators.Sellers(); 
            this.Sellers = generator.Get();
        }

        public string Title
        {
            get { return Constants.Cotrollers.Title_Sallers; }
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Seller", "Index");

            return View(this.Sellers);
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
        public ActionResult Card(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("Index");
            }

            var seller = this.Sellers.Where(x => x.Id == id).FirstOrDefault();

            if (seller == null)
            {
                return Redirect("Index");
            }

            return View(seller);
        }
    }
}