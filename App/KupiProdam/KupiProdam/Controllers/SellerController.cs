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
        private List<User> Sellers { get; set; }

        public SellerController()
            : base()
        {
            
            var generator = new KupiProdam.Utils.Generators.UserGenerstor(); 
            this.Sellers = generator.Get();
        }

        public ActionResult ShowOpenTenders()
        {
            var generator = new Utils.Generators.TenderGenerator();

            return View(generator.Get());
        }

        public ActionResult ShowOpenTender(int? id)
        {
            var generator = new Utils.Generators.TenderGenerator();

            return View(generator.Get().Where(x => x.Id == id).FirstOrDefault());
        }

        public string Title
        {
            get { return ConstantsKP.Cotrollers.Title_Sallers; }
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            this.ViewBag.Title = this.Title;

            var generator = new Utils.Generators.UserGenerstor();

            return View(generator.Get());
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
        //[Authorize]
        [HttpGet]
        public ActionResult Card(int? id)
        {
            var generator = new Utils.Generators.UserGenerstor();

            return View(generator.Get().Where(x => x.Id == id ).FirstOrDefault());
        }

        public List<User> GetListUsers()
        {
            var list = new List<User>();

            return list;
        }
    }
}