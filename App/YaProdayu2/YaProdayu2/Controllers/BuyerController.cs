using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views;
using YaProdayu2.Y2System;
using YaProdayu2.Y2System.Utils;
using YaProdayu2.Models;
using System.IO;

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
                    .Where(x => x.UserId != this.Auth.CurrentUser.Id);

                return View(listTenders);
            }
        }

        public ActionResult MyTenders()
        {
            var tenders = new List<Tender>();

            using (var session = DBHelper.OpenSession())
            {
                tenders = session.CreateCriteria<Tender>()
                    .List<Tender>()
                    .Where(x => x.UserId == this.Auth.CurrentUser.Id)
                    .ToList();
            }

            if (tenders != null)
            {
                return View(tenders);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SellerMessages(int? tenderId, int? messageId)
        {
            if (tenderId.HasValue && messageId.HasValue)
            {
                TenderMessage message = null;
                Tender tender = null;
                var listMessages = new List<TenderMessage>();

                using (var session = DBHelper.OpenSession())
                {
                    message = session.CreateCriteria<TenderMessage>()
                        .List<TenderMessage>()
                        .Where(x => x.Id == messageId)
                        .FirstOrDefault();
                }

                using (var session = DBHelper.OpenSession())
                {
                    tender = session.CreateCriteria<Tender>()
                        .List<Tender>()
                        .Where(x => x.Id == tenderId)
                        .FirstOrDefault();
                }

                using (var session = DBHelper.OpenSession())
                {
                    listMessages = session.CreateCriteria<TenderMessage>()
                        .List<TenderMessage>()
                        .Where(x => x.ParentMessageId == messageId)
                        .Where(x => x.IsSubMessage == true)
                        .OrderByDescending(x => x.CreationTime)
                        .ToList();
                }

                if (message != null)
                {
                    var subMessages = new TenderMessageDetails()
                    {
                        Tender = tender,
                        Message = message,
                        SubMessages = listMessages
                    };

                    return View(subMessages);   
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SellerMessages(PostMessageInfo model)
        {
            if (model != null)
            {
                var newMessage = new TenderMessage() 
                { 
                    FromUserId = this.Auth.CurrentUser.Id,
                    CreationTime = DateTime.Now,
                    Message = model.Message,
                    ParentMessageId = model.ParentMessageId,
                    IsSubMessage = true
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

            return RedirectToAction("SellerMessages", new { tenderId = model.TenderId, messageId = model.ParentMessageId});
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
            model.ListRegions = this.GetRegions();
            model.ListCitys = new List<string>();
            model.AllowWriteMe = true;
            model.ListPhoto = new List<HttpPostedFileBase>().ToArray();
            
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTender(NewTenderView model)
        {
            if (this.Auth.CurrentUser == null)
            {
                return RedirectToAction("Registration", "Account");
            }

            if (!model.IsValid())
            {
                var themes = new DictionaryThemes();
                model.IconTheme = themes.List.Where(x => x.Key == model.Theme).FirstOrDefault().Image;
                model.IconWidth = themes.List.Where(x => x.Key == model.Theme).FirstOrDefault().Width;
                model.IconHeight = themes.List.Where(x => x.Key == model.Theme).FirstOrDefault().Heigth;
                model.ListSubThemes = themes.List.Where(x => x.Key == model.Theme).FirstOrDefault().SubThemes;
                model.ListCitys = new List<string>();
                model.ListRegions = this.GetRegions();

                return View(model);
            }

            var tender = new Tender()
            {
                Region = model.Region,
                City = model.City,
                UserId = this.Auth.CurrentUser.Id,
                Message = model.Message,
                Title = model.Title,
                DateCreation = DateTime.Now,
                ActiviteTime = model.ActiveTime == null ? "3 дня" : model.ActiveTime,
                Theme = model.Theme,
                SubTheme = model.SubTheme,
                AllowWriteMe = model.AllowWriteMe,
                Cost = model.Coste,
                TypeTender = model.TenderType,
                IsClose = false
            };

            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(tender);
                    transaction.Commit();
                }
            }

            if (model.ListPhoto != null)
            {
                this.SaveImages(model.ListPhoto, tender.Id);
            }

            return RedirectToAction("MyTenders", "Buyer");
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

        [HttpGet]
        public ActionResult CloseTender(int? tenderId, int? userId)
        {
            if (tenderId.HasValue)
            {
                this.CloseTend(tenderId.Value, userId.Value);
            }

            return RedirectToAction(string.Format("Details/{0}", tenderId));
        }

        private void CloseTend(int tenderId, int winnerId)
        {
            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var tender = session.CreateCriteria<Tender>()
                        .List<Tender>()
                        .Where(x => x.Id == tenderId)
                        .FirstOrDefault();

                    var winner = session.CreateCriteria<UserSystem>()
                        .List<UserSystem>()
                        .Where(x => x.Id == winnerId)
                        .FirstOrDefault();

                    if (tender != null && winner != null)
                    {
                        tender.IsClose = true;
                        tender.Winner = winner.Id;

                        session.SaveOrUpdate(tender);
                        transaction.Commit();
                    }
                }
            }
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
        [ValidateInput(false)]
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
                        //Coste = model.Cost,
                        Message = model.Message,
                        FromUserId = user.Id,
                        TenderId = model.TenderId,
                        IsSubMessage = false
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

        private SellerTenderDetailsView GetMessages(int messageId)
        {
            SellerTenderDetailsView messageInfo = null;

            using (var session = DBHelper.OpenSession())
            {
                var tender = session.CreateCriteria<Tender>()
                    .List<Tender>()
                    .Where(x => x.Id == messageId)
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

        private void SaveImages(HttpPostedFileBase[] files, int parentId)
        {
            var i = 0;

            foreach (var file in files)
            {
                if (file == null)
                {
                    continue;
                }

                if (i == 3) 
                {
                    continue;
                }

                if (file != null && file.ContentLength == 0)
                {
                    continue;
                }

                if (file.ContentLength > 400000)
                {
                    continue;
                }

                var ms = new MemoryStream(400000);
                file.InputStream.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var newImage = new Image()
                {
                    Name = file.FileName,
                    Type = file.ContentType,
                    ParentId = parentId,
                    Data = ms.ToArray(),
                    TypeFor = 0
                };

                using (var session = DBHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(newImage);
                        transaction.Commit();
                    }
                }

                i++;
            }
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
                        .Where(x => x.ParentId == parentId && x.TypeFor == 0).ToList();

                    if (obj != null)
                    {
                        list.AddRange(obj.Select(x => x.ID));
                    }
                }
                else
                {
                    var obj = new Image();

                    obj = session.CreateCriteria<Image>().List<Image>()
                        .Where(x => x.ID == parentId && x.TypeFor == 0)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        list.Add(obj.ID);
                    }
                }
            }

            return list.ToArray();
        }

        public class PostMessageInfo
        {
            public int TenderId { get; set; }

            public int ParentMessageId { get; set; }

            public string Message { get; set; }
        }

        public List<string> GetRegions()
        {
            var list = new List<string>();

            using (var session = DBHelper.OpenSession())
            {
                list = session.CreateCriteria<Region>()
                    .List<Region>()
                    .Where(x => x.Country_id == 3159)
                    //.Where(x => x.Country_id == 9908)
                    .Select(x => x.Name)
                    .OrderBy(x => x)
                    .ToList();
            }

            return list;
        }

        [HttpGet]
        public JsonResult GetListCityes(string name)
        {
            var list = new List<string>();
            var list2 = new object();

            if (!string.IsNullOrEmpty(name.Trim()))
            {
                using (var session = DBHelper.OpenSession())
                {
                    var region = session.CreateCriteria<Region>()
                        .List<Region>()
                        .Where(x => x.Name == name)
                        .FirstOrDefault();

                    if (region != null)
                    {
                        list = session.CreateCriteria<City>()
                            .List<City>()
                            .Where(x => x.Region_id == region.Region_id)
                            .OrderBy(x => x.Name)
                            .Select(x => x.Name)
                            .ToList();
                    }
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
	}
}