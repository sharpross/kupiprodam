using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models;
using YaProdayu2.Models.Entities;
using YaProdayu2.Y2System;

namespace YaProdayu2.Controllers
{
    public class CutawayController : BaseController
    {
        // GET: Cutaway
        public ActionResult Index()
        {
            var contacts = new ContactsService().GetAll()
                .Where(x => x.UserId == this.Auth.CurrentUser.Id);

            var list = new List<UserSystem>();

            foreach (var item in contacts)
            {
                var user = new UserSystemService().Get(item.ContactId);

                if (user != null)
                {
                    list.Add(user);
                }
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult Add(int id)
        {
            var service = new ContactsService();

            var exist = service
                .GetAll()
                .Where(x => x.UserId == this.Auth.CurrentUser.Id)
                .Where(x => x.ContactId == id)
                .FirstOrDefault();

            if (exist == null)
            {
                service.Save(new Contacts()
                {
                    UserId = this.Auth.CurrentUser.Id,
                    ContactId = id
                });
            }

            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var service = new ContactsService();

            service.Delete(id);

            return null;
        }
    }
}