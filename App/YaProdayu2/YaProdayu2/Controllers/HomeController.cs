using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Y2System.Utils;
using YaProdayu2.Y2System;
using YaProdayu2.Models.Support;

namespace YaProdayu2.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var list = new DictionaryThemes().List;

            return View(list);
        }

        public ActionResult How()
        {
            return View();
        }

        public ActionResult Reginfo()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Instruction()
        {
            return View();
        }

        public ActionResult Termins()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Support()
        {
            var model = new SupportModel();

            var service = new MailService();
            service.Send(new MailServiceParams()
            {
                Body = model.Message,
                From = "site",
                Title = model.Title,
                To = "",
                
            });

            return View(model);
        }

        public ActionResult Docs()
        {
            return View();
        }

        public ActionResult Sendmessge()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Support(SupportModel model)
        {
            return RedirectToAction("Sendmessge");
        }
    }
}