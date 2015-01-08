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
            model.ListPhoto = new List<HttpPostedFileBase>().ToArray();

            return View(model);
        }

        [HttpPost]
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
                model.Citys = new DictionaryCitys().List;

                return View(model);
            }

            var tender = new Tender()
            {
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

        [HttpPost]
        public ActionResult RemoveTender(int? id)
        {
            return View();
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
                    Data = ms.ToArray()
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