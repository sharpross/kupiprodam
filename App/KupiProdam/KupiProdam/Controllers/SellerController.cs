using KupiProdam.Core;
using KupiProdam.Entities;
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

        //[Authorize]
        [HttpGet]
        public ActionResult Index(int? page)
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Seller", "Index");

            var repo = new RepoSeller();

            return View(repo.GetAll());
        }

        /// <summary>
        /// Каталог покупателя
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        public ActionResult Catalog()
        {
            return View(this.Sellers);
        }

        /// <summary>
        /// Визитка
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        ///[Authorize]
        [HttpGet]
        public ActionResult Card(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("Index");
            }

            var seller = new RepoSeller().GetById(id.Value);

            if (seller == null)
            {
                return Redirect("Index");
            }

            return View(seller);
        }
    }
}