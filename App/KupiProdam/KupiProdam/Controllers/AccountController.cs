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
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace KupiProdam.Controllers
{
    public class AccountController : Controller, IBaseController
    {
        public  string Title
        {
            get { return ConstantsKP.Cotrollers.Title_Account; }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string email, string password)
        {
            return Redirect("Registration");
        }

        [OutputCache(Duration=10)]
        public ActionResult Index(bool? isSeller)
        {
            this.ViewBag.Titile = this.Title;
            
            this.ViewBag.IsSeller = isSeller;

            return Redirect("Account/Registration");
        }

        public ActionResult BuyerProfile()
        {
            var user = new User() 
            {
                Name = "Петрович",
                About = "Я просто очень хороший покупатель",
                Email = "moy@mail.ru",
                Facebook = "wwww.mail.ru",
                Site = "moysite.ru",
                VKontakte = "vk.com",
                Skype = "pozvonimne",
                MainPhone = "+7987654321"
            };

            return View(user);
        }

        public ActionResult SellerProfile()
        {
            var user = new User()
            {
                Name = "Петрович",
                About = "Я просто очень хороший покупатель",
                Email = "moy@mail.ru",
                Facebook = "wwww.mail.ru",
                Site = "moysite.ru",
                VKontakte = "vk.com",
                Skype = "pozvonimne",
                MainPhone = "+7987654321"
            };

            return View(user);
        }

        [HttpPost]
        public ActionResult Update(object profileData)
        {
            if (profileData is User)
            {
                var repo = new RepoSeller();

                repo.Update(profileData as User);
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
            var seller = new User();

            this.ViewBag.Titile = string.Format("{0} - {1}", this.Title, ConstantsKP.Cotrollers.Title_Sallers);

            return View(seller);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SellerRegistartion(User seller)
        {
            

            return Redirect("Index");
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult BuyerRegistartion()
        {
            this.ViewBag.Titile = string.Format("{0} - {1}", this.Title, ConstantsKP.Cotrollers.Title_Buyers);
            this.ViewBag.HideRigthSide = true;

            return View();
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

            if (user is User)
            {
                    
            }

            if (user is Buyer)
            {
                
            }
        }

        public class LogInModel
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }
    }
}