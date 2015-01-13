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
                    .ToList();
            }

            return View(listTenders);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                SellerTenderDetailsView tenderDetails = this.GetMessages((int)id);

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
                var user = this.GetUserByEmail(model.UserLogin);

                if (user != null)
                {
                    var newMessage = new TenderMessage()
                    {
                        CreationTime = DateTime.Now,
                        Coste = model.Cost,
                        Message = model.Message,
                        FromUserId = user.Id,
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
            }

            return RedirectToAction(string.Format("Details/{0}", model.TenderId));
        }


        public ActionResult AddMessage()
        {
            return View();
        }

        private UserSystem GetUserById(int id)
        {
            UserSystem user = null;

            using (var session = DBHelper.OpenSession())
            {
                user = session.CreateCriteria<UserSystem>()
                    .List<UserSystem>()
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            }

            return user;
        }

        private SellerTenderDetailsView GetMessages(int messageId)
        {
            SellerTenderDetailsView messageInfo = null;

            using (var session = DBHelper.OpenSession())
            {
                var tender = session.CreateCriteria<Tender>()
                    .List<Tender>()
                    .Where(x => x.Id == messageId)
                    .OrderByDescending(x => x.DateCreation)
                    .FirstOrDefault();

                if (tender != null)
                {
                    tender.Photos = this.GetImagesFirst(tender.Id, true);

                    messageInfo = new SellerTenderDetailsView()
                    {
                        TenderInfo = tender
                    };

                    List<TenderMessage> listMessages = null;

                    listMessages = session.CreateCriteria<TenderMessage>()
                        .List<TenderMessage>()
                        .Where(x => x.TenderId == tender.Id)
                        .OrderByDescending(x => x.CreationTime)
                        .ToList();

                    if (listMessages != null && listMessages.Count > 0)
                    {
                        foreach (var message in listMessages)
                        {
                            var user = this.GetUserById(message.FromUserId);

                            if (user != null)
                            {
                                messageInfo.Messages.Add(new MessageInfo()
                                {
                                    User = user,
                                    Messages = message
                                });
                            }
                        }
                        if (messageInfo != null)
                        {
                            messageInfo.HaveMyMessages = messageInfo.Messages
                                .Where(x => x.User.Id == this.Auth.CurrentUser.Id)
                                .FirstOrDefault() != null;
                        }
                    }
                }
            }

            return messageInfo;
        }

        private UserSystem GetUserByEmail(string email)
        {
            UserSystem user = null;

            using (var session = DBHelper.OpenSession())
            {
                user = session.CreateCriteria<UserSystem>()
                    .List<UserSystem>()
                    .Where(x => x.Email == email)
                    .FirstOrDefault();
            }

            return user;
        }

        private int[] GetImagesFirst(int parentId, bool all)
        {
            var list = new List<int>();

            using (var session = DBHelper.OpenSession())
            {


                if (all)
                {
                    var obj = new List<Image>();

                    obj = session.CreateCriteria<Image>().List<Image>()
                        .Where(x => x.ID == parentId).ToList();

                    if (obj != null)
                    {
                        list.AddRange(obj.Select(x => x.ID));
                    }
                }
                else
                {
                    var obj = new Image();

                    obj = session.CreateCriteria<Image>().List<Image>()
                        .Where(x => x.ID == parentId)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        list.Add(obj.ID);
                    }
                }
            }

            return list.ToArray();
        }

	}
}