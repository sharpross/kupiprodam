using KupiProdam.Core;
using KupiProdam.Entities.Entites;
using KupiProdam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KupiProdam.Entities;

namespace KupiProdam.Controllers
{
    public class AccountController : Controller, IBaseController
    {
        public  string Title
        {
            get { return Constants.Cotrollers.Title_Account; }
        }

        [OutputCache(Duration=10)]
        public ActionResult Index(bool isSeller)
        {
            this.ViewBag.Titile = this.Title;
            //this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Account", "Index");
            this.ViewBag.IsSeller = isSeller;

            switch (isSeller)
            {
                case true:
                    var seller = this.GetSellerProfile();
                   return View(seller);
                case false:
                   var buyer = this.GetBuyerProfile();
                   return View(buyer);
            }

            return Redirect("Home/Index");
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult Registration()
        {
            this.ViewBag.Titile = this.Title;

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult BuyerRegistration()
        {
            this.ViewBag.Titile = this.Title;

            this.ViewBag.HideBreadcrumbs = true;
            this.ViewBag.HideRigthSide = true;
            this.ViewBag.HideLeftSide = true;

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult SellerRegistartion()
        {
            var seller = new Seller();

            this.ViewBag.Titile = string.Format("{0} - {1}", this.Title, Constants.Cotrollers.Title_Sallers);

            return View(seller);
        }

        [HttpPost]
        public ActionResult SellerRegistartion(Seller seller)
        {
            var repo = new RepoSeller();

            repo.Create(seller);

            return Redirect("Index");
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult BuyerRegistartion()
        {
            this.ViewBag.Titile = string.Format("{0} - {1}", this.Title, Constants.Cotrollers.Title_Buyers);
            this.ViewBag.HideRigthSide = true;

            return View();
        }

        private Seller GetSellerProfile()
        {
            return new Entities.Entites.Seller()
            {
                Id = 0,
                Name = "Фирмас",
                //Photo = "~/Content/images/fish/1.jpg",
                About = "Мы явно очень хорошее предприятие. У нас очень много довольных клиентов. Айдате к нам! Мы явно очень хорошее предприятие. У нас очень много довольных клиентов. Айдате к нам!",
                Email = "mail@mail.ru",
                MainPhone = "+9877543210",
                Site = "www.site.ru",
                Skype = "skype",
                VKontakte = "www.vk.ru",
                Facebook = "www.facebook.ru",
                Phones = new List<string>() { "+9876543210", "+9876543212" },
                Addresses = new List<string>() { "qw", "as" }
            };
        }

        private Buyer GetBuyerProfile()
        {
            return new Buyer() 
            {
                Id = 0,
                Name = "Иван",
                Email = "mail@mail2.ru"
            };
        }

        private void SetCurrentUser(object user)
        {

            if (user is Seller)
            {
                    
            }

            if (user is Buyer)
            {
                
            }
        }
    }
}