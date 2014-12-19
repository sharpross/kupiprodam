using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views.Account;
using YaProdayu2.Models.Views.Buyer;
using YaProdayu2.Models.Views.Seller;
using YaProdayu2.Models.Views.Tender;
using YaProdayu2.Models.Views.Theme;
using YaProdayu2.Sysyem.System;

namespace YaProdayu2.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Registration");
        }

        /// <summary>
        /// Профиль продавца
        /// </summary>
        /// <returns></returns>
        public ActionResult Seller()
        {
            return View();
        }

        /// <summary>
        /// Профиль покупателя
        /// </summary>
        /// <returns></returns>
        public ActionResult Buyer()
        {
            return View();
        }

        /// <summary>
        /// Общая страница регистрации
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Регистрация продавца
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegSeller()
        {
            var model = new SellerRegistrationView();

            return View(model);
        }

        /// <summary>
        /// Регисрация продавца
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegSeller(SellerRegistrationView model)
        {
            var user = new UserSystem() {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                IsSeller = true,
                CreationTime = DateTime.Now
            };

            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Регистрация покупателя
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegBuyer()
        {
            var model = new BuyerRegistrationView();

            return View(model);
        }

        /// <summary>
        /// Регистрация покупателя
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegBuyer(BuyerRegistrationView model)
        {
            var user = new UserSystem() {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                IsSeller = false,
                CreationTime = DateTime.Now
            };

            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /*[AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }*/

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginView loginData)
        {


            return View();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return View();
        }
	}
}