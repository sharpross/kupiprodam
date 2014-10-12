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
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public  string Title
        {
            get { return ConstantsKP.Cotrollers.Title_Account; }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string email, string password)
        {
           

            /*var sellRepo = new RepoSeller();
            var user = sellRepo.GetAll().Where(x => x.Email == email && x.Password == password).FirstOrDefault();

            if (user != null)
            {

                FormsAuthentication.SetAuthCookie(user.Email, false);

                return RedirectToAction("Index", "Home");;
            }*/

            return Redirect("Registration");
        }

        [OutputCache(Duration=10)]
        public ActionResult Index(bool? isSeller)
        {
            this.ViewBag.Titile = this.Title;
            //this.ViewBag.Breadcrumbs = Breadcrumbs.Get("Account", "Index");
            this.ViewBag.IsSeller = isSeller;

            /*switch (isSeller)
            {
                case true:
                    var seller = this.GetSellerProfile();
                   return View(seller);
                case false:
                   var buyer = this.GetBuyerProfile();
                   return View(buyer);
            }*/

            return Redirect("Registration");
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
            var user = new User { UserName = seller.Email, Password = seller.Password, Name = seller.Name };
            var result = await UserManager.CreateAsync(user, seller.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Home");
            }

            /*var repo = new RepoSeller();

            repo.Create(seller);*/

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

        /*private User GetSellerProfile()
        {
            var repo = new RepoSeller();
            return repo.GetAll().Where(x => x.Id == 1).FirstOrDefault();
        }*/

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