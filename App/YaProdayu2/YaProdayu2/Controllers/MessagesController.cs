using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.UserMessages;
using YaProdayu2.Y2System;

namespace YaProdayu2.Controllers
{
    public class MessagesController : BaseController
    {
        // GET: Messages
        public ActionResult Index()
        {
            /*var messagModel = new List<MessageModel>();

            var contacts = new ContactsService().GetAll()
                .Where(x => x.UserId == this.Auth.CurrentUser.Id);

            //var list = new List<UserSystem>();

            var messageService = new UserMessageService();

            var messages = messageService
                .GetAll()
                .Where(x => x.ToUserId == this.Auth.CurrentUser.Id)
                .ToList();

            foreach (var item in contacts)
            {
                var user = new UserSystemService().Get(item.ContactId);

                if (user != null)
                {
                    var cnt = messages
                        .Where(x => x.UserId == item.ContactId)
                        .Count();

                    ///list.Add(user);
                    messagModel.Add(new MessageModel() { 
                        User = user,
                        UnReaded = cnt
                    });
                }
            }

            foreach (var msg in messageService.GetAll().Where(x => x.ToUserId == this.Auth.CurrentUser.Id))
            {
                var contains = messagModel.Where(x => x.User.Id == msg.ToUserId).FirstOrDefault();

                /*if (contains == null)
                {
                    messagModel.Add(new MessageModel()
                    {
                        User = new UserSystemService().Get(msg.UserId),
                        UnReaded = messageService.GetAll().Where(x => !x.IsRead && x.ToUserId == this.Auth.CurrentUser.Id).Count()
                    });
                }
            }*/

            var messages = new UserListMessagesView(this.Auth.CurrentUser.Id);

            return View(messages);
        }

        [HttpGet]
        public ActionResult Write(int? toUserId)
        {
            var messages = new UserMessageModel(this.Auth.CurrentUser.Id, toUserId.Value);

            return View(messages);
        }

        [HttpPost]
        public ActionResult Write(UserMessageModel model)
        {
            model.Save();

            return RedirectToAction("Write", new { @toUserId = model.ToUserId });
        }

    }
}