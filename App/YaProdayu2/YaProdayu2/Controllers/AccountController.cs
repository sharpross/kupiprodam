using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views;
using YaProdayu2.Y2System;
using YaProdayu2.Y2System.System;

namespace YaProdayu2.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Registration");
        }

        [HttpGet]
        public ActionResult Profile(string login)
        {
            return View(this.Auth.CurrentUser);
        }

        [HttpPost]
        public ActionResult Profile(UserSystem userData)
        {


            return View(this.Auth.CurrentUser);
        }

        /// <summary>
        /// Визитка
        /// </summary>
        /// <returns></returns>
        public ActionResult Card(string email)
        {
            UserSystem user = null;

            using (var session = DBHelper.OpenSession())
            {
                user = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Email == email)
                    .FirstOrDefault();
            }

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View(this.Auth.CurrentUser);
        }

        [HttpPost]
        public ActionResult Edit(int? model)
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
            if (this.Auth.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            if (this.Auth.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            if (this.Auth.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = new UserSystem() {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Organization = model.Organization,
                Post = model.Post,
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
            if (this.Auth.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            if (this.Auth.CurrentUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

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

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(string login, string password, bool? remembeMe)
        {
            UserSystem user = null;

            if (!string.IsNullOrEmpty(login) &&
                !string.IsNullOrEmpty(password))
            {
                user = this.Auth.Login(login, password, remembeMe == null ? false : (bool)remembeMe);

                if (user != null)
                {
                    return Json(new
                    {
                        Success = true,
                        Data = "null"
                    });
                }
            }

            var result = new
            {
                Success = false,
                Data = "Логин или пароль указаны не карректно."
            };

            return Json(result);
        }

        public JsonResult Logout()
        {
            this.Auth.LogOut();

            return Json(new
            {
                Success = true,
                Data = "null"
            });
        }
	}
}