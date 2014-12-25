using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views;
using YaProdayu2.Y2System;
using YaProdayu2.Y2System.System;

namespace YaProdayu2.Controllers
{
    public class SellerController : BaseController
    {
        //
        // GET: /Seller/
        public ActionResult Index()
        {
            return RedirectToAction("List");
            //return View();
        }

        public ActionResult List()
        {
            var listTenders = new List<Tender>();

            using (var session = DBHelper.OpenSession())
            {
                listTenders = session.CreateCriteria<Tender>().List<Tender>()
                    .Where(x => x.UserId == 1)
                    .ToList();
            }

            return View(listTenders);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                SellerTenderDetailsView tenderDetails = null;

                using (var session = DBHelper.OpenSession())
                {
                    var tender = session.CreateCriteria<Tender>()
                        .List<Tender>()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();

                    TenderMessage message = null;

                    if (tender != null)
                    {
                        message = session.CreateCriteria<TenderMessage>()
                            .List<TenderMessage>()
                            .Where(x => x.TenderId == tender.Id)
                            .FirstOrDefault(); 
                    }

                    UserSystem user = null;

                    if (message != null)
                    {
                        user = session.CreateCriteria<UserSystem>()
                            .List<UserSystem>()
                            .Where(x => x.Id == message.FromUserId)
                            .FirstOrDefault();
                    }

                    if (tender != null)
                    {
                        tenderDetails = new SellerTenderDetailsView()
                        {
                            Message = message,
                            TenderInfo = tender,
                            User = user
                        };
                    }
                }
                if (tenderDetails != null)
                {
                    return View(tenderDetails);
                }
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Details(TenderAddMessageView model)
        {
            if (model != null)
            {
                var newMessage = new TenderMessage() 
                {
                    CreationTime = DateTime.Now,
                    Coste = model.Cost,
                    Message = model.Message,
                    FromUserId = model.UserID,
                    TenderId = model.TenderId
                };

                using (var session = DBHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(newMessage);
                        transaction.Commit();
                    }
                }
            }

            return RedirectToAction(string.Format("Details/{0}", model.TenderId));
        }


        public ActionResult AddMessage()
        {
            return View();
        }
	}
}