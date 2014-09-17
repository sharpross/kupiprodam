using KupiProdam.Core;
using KupiProdam.Entities.Entites;
using KupiProdam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KupiProdam.Entities;
using System.Web.Security;

namespace KupiProdam.Controllers
{
    public class AccountController : Controller, IBaseController
    {
        public  string Title
        {
            get { return Constants.Cotrollers.Title_Account; }
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            var sellRepo = new RepoSeller();
            var user = sellRepo.GetAll().Where(x => x.Name == login && x.Password == password).FirstOrDefault();

            if (user != null)
            {
                return Redirect("Index");
            }

            return Redirect("Registration");
        }

        //[Authorize]
        [OutputCache(Duration=10)]
        public ActionResult Index(bool? isSeller)
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

            return Redirect("Registration");
        }

        [HttpPost]
        public ActionResult Update(object profileData)
        {
            if (profileData is Seller)
            {
                var repo = new RepoSeller();

                repo.Update(profileData as Seller);
            }

            return null;
        }

        [AllowAnonymous]
        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult Registration()
        {
            this.ViewBag.Titile = this.Title;

            return View();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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
            var repo = new RepoSeller();
            return repo.GetAll().Where(x => x.Id == 1).FirstOrDefault();
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