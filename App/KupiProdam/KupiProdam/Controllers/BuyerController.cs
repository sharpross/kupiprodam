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
            get { return ConstantsKP.Cotrollers.Title_Buyers; }
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = this.Title;
            this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Buyer", "Index");

            return View();
        }

        /*public ActionResult Create()
        {
            var newTender = new Tender();

            return View(newTender);
        }*/

        public ActionResult Create(string theme, string subTheme)
        {
            if(theme == null)
            {
                theme = string.Empty;
            }

            if(subTheme == null)
            {
                subTheme = string.Empty;
            }

            var newTender = new Tender() {
                Theme = theme.ToString(),
                SubTheme = subTheme.ToString()
            };

            return View(newTender);
        }

        public ActionResult AddTender()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View(new Tender()
            {
                Messaget = "Хочу это",
                Statuse = 1,
                Theme = "Праздник",
                Title = "тамада",
                WriteMe = true
            });
        }

        public ActionResult List()
        {
            var list = new List<Tender>();

            list.Add(new Tender() {
                Messaget = "Хочу это",
                Statuse = 1,
                Theme = "Праздник",
                Title = "тамада",
                WriteMe = true
            });

            list.Add(new Tender()
            {
                Messaget = "Хочу это",
                Statuse = 1,
                Theme = "Праздник",
                Title = "тамада",
                WriteMe = true
            });

            return View(list);
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