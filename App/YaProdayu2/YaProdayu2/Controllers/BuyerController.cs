using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views;
using YaProdayu2.Y2System;
using YaProdayu2.Y2System.Utils;
using YaProdayu2.Y2System.System;

namespace YaProdayu2.Controllers
{
    public class BuyerController : BaseController
    {
        //
        // GET: /Buyer/
        public ActionResult Index()
        {
            using (var session = DBHelper.OpenSession())
            {
                var listTenders = session.CreateCriteria<Tender>().List<Tender>()
                    .Where(x => x.UserId == 1);

                return View(listTenders);
            }
        }

        [HttpGet]
        public ActionResult Newtender()
        {
            var list = new DictionaryThemes().List;

            return View(list);
        }

        [HttpGet]
        public ActionResult CreateTender(string theme)
        {
            var themes = new DictionaryThemes();

            var model = new NewTenderView();
            model.Theme = theme;
            model.IconTheme = themes.List.Where(x => x.Key == theme).FirstOrDefault().Image;
            model.IconWidth = themes.List.Where(x => x.Key == theme).FirstOrDefault().Width;
            model.IconHeight = themes.List.Where(x => x.Key == theme).FirstOrDefault().Heigth;
            model.ListSubThemes = themes.List.Where(x => x.Key == theme).FirstOrDefault().SubThemes;
            model.Citys = new DictionaryCitys().List;
            model.AllowWriteMe = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTender(NewTenderView model)
        {
            var tender = new Tender()
            {
                City = model.City,
                UserId = 1,
                Message = model.Message,
                Title = model.Title,
                DateCreation = DateTime.Now,
                ActiviteTime = model.ActiveTime == null ? "3 дня" : model.ActiveTime,
                Theme = model.Theme,
                SubTheme = model.SubTheme,
                AllowWriteMe = model.AllowWriteMe,
                Cost = model.Coste,
                TypeTender = model.TenderType
            };

            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(tender);
                    transaction.Commit();
                }
            }

            return RedirectToAction("Index", "Buyer");
        }

        public ActionResult EditTender(int? id)
        {
            if (id.HasValue)
            {
                using (var session = DBHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var tender = session.Get<Tender>((int)id);

                        return View();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveTender(int id)
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var tender = new Tender();

                using (var session = DBHelper.OpenSession())
                {
                    tender = session.CreateCriteria<Tender>()
                        .List<Tender>()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
                }
                if (tender != null)
                {
                    return View(tender);
                }
            }

            return RedirectToAction("Newtender");
        }
	}
}