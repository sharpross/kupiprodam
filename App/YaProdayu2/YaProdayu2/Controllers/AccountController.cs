using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views;
using YaProdayu2.Y2System;
using YaProdayu2.Y2System.Utils;

namespace YaProdayu2.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Registration");
        }

        public ActionResult Confirm(string k)
        {


            var user = new UserSystem();
            this.Auth.Login(user.Login, user.Password);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Profile()
        {
            
            var user = this.Auth.CurrentUser;

            if (user != null)
            {
                var listSubsciptions = new List<Subsciptions>();

                using (var session = DBHelper.OpenSession())
                {
                    listSubsciptions = session.CreateCriteria<Subsciptions>().List<Subsciptions>()
                        .Where(x => x.UserId == this.Auth.CurrentUser.Id)
                        .ToList();
                }

                var profile = new ProfileView() 
                { 
                    Id = user.Id,
                    About = user.About,
                    Email = user.Email,
                    Facebook = user.Facebook,
                    Name = user.Name,
                    Organization = user.Organization,
                    Portfolio = user.Portfolio,
                    Phone = user.Phone,
                    Post = user.Post,
                    Site = user.Site,
                    Skype = user.Skype,
                    VKontakte = user.VKontakte,
                    ImageId = user.ImageId,
                    IsSeller = user.IsSeller,
                    ListSubsciptions = listSubsciptions,
                    ListThemes = new DictionaryThemes().List,
                    ListImages = new List<int>(this.GetImagesFirst(user.Id, true))
                };

                return View(profile);
            }
            
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult UpdateSubsciptions(List<string> subs)
        {
            var subsciptions = new List<int>();
            using (var session = DBHelper.OpenSession())
            {
                subsciptions = session.CreateCriteria<Subsciptions>().List<Subsciptions>()
                    .Where(rec => rec.UserId == this.Auth.CurrentUser.Id)
                    .Select(x => x.Id)
                    .ToList();
            }

            using (var session = DBHelper.OpenSession())
            {
                if (subsciptions != null)
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        foreach(var sub in subsciptions)
                        {
                            session.Delete(new Subsciptions() { Id = sub });
                            
                        }

                        transaction.Commit();
                    }
                }
            }

            if (subs != null)
            {
                using (var session = DBHelper.OpenSession())
                {
                    foreach (var item in subs)
                    {
                        var sub = new Subsciptions()
                        {
                            UserId = this.Auth.CurrentUser.Id,
                            Theme = item
                        };

                        using (var transaction = session.BeginTransaction())
                        {
                            session.SaveOrUpdate(sub);
                            transaction.Commit();
                        }
                    }
                }
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public int UpdateAvatar()
        {
            var i = 0;

            if (this.Request.Files.Count > 0)
            {
                var file = this.Request.Files[0];

                var ms = new MemoryStream(400000);
                file.InputStream.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var newImage = new Image()
                {
                    Name = file.FileName,
                    Type = file.ContentType,
                    ParentId = 0,
                    Data = ms.ToArray(),
                    TypeFor = 1
                };

                using (var session = DBHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(newImage);
                        transaction.Commit();
                    }
                }

                using (var session = DBHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        this.Auth.CurrentUser.ImageId = newImage.ID;
                        session.Update(this.Auth.CurrentUser);
                        transaction.Commit();
                    }
                }

                i = newImage.ID;
            }

            return i;
        }

        [HttpPost]
        public ActionResult UpdateProfile(ProfileView model)
        {
            using (var session = DBHelper.OpenSession())
            {
                var user = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Login == this.Auth.CurrentUser.Login)
                    .FirstOrDefault();

                user.Name = model.Name;
                user.Organization = model.Organization;
                user.Post = model.Post;

                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Site = model.Site;

                user.VKontakte = model.VKontakte;
                user.Facebook = model.Facebook;
                user.Skype = model.Skype;

                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public ActionResult UpdatePortfolio(ProfileView model)
        {
            if (model.NewImages != null && model.NewImages.Count() > 0)
            {
                this.SaveImages(model.NewImages, model.Id);
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAbout(string about)
        {
            using (var session = DBHelper.OpenSession())
            {
                var user = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Login == this.Auth.CurrentUser.Login)
                    .FirstOrDefault();

                user.About = about;

                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public JsonResult UpdateImages()
        {


            return Json(new
            {
                Success = true,
                Data = "null"
            });
        }

        /// <summary>
        /// Визитка
        /// </summary>
        /// <returns></returns>
        public ActionResult Card(string user)
        {
            UserSystem userSystem = null;

            using (var session = DBHelper.OpenSession())
            {
                userSystem = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Login == user)
                    .FirstOrDefault();
            }

            if (userSystem == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //userSystem.ListImages = new List<int>(this.GetImagesFirst(userSystem.Id, true));

            return View(userSystem);
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
                Login = model.Email,
                Password = MD5Helper.GetMD5String(model.Password),
                Organization = model.Organization,
                Post = model.Post,
                Phone = model.Phone,
                IsSeller = true,
                CreationTime = DateTime.Now,
                
            };

            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }

            var message = "Здравствуйте, {username}!\nДля подтверждения регистрации, пожалуйста перейдите по сылке ниже: \n{url}{key}";
            var mailParams = new Dictionary<string, string>();
            mailParams.Add("{username}", user.Name);
            mailParams.Add("{url}", "");
            mailParams.Add("{key}", "");

            this.Auth.Login(user.Login, user.Password);

            return RedirectToAction("Profile");
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
                Login = model.Email,
                Password = MD5Helper.GetMD5String(model.Password),
                IsSeller = false,
                CreationTime = DateTime.Now,
                ImageId = 0
            };

            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }

            this.Auth.Login(user.Login, user.Password);

            return RedirectToAction("Profile");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string login, string password, bool? remembeMe)
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
                Data = "Логин или пароль указаны не корректно."
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

        private int[] GetImagesFirst(int parentId, bool all)
        {
            var list = new List<int>();

            using (var session = DBHelper.OpenSession())
            {
                if (all)
                {
                    var obj = new List<Image>();

                    obj = session.CreateCriteria<Image>().List<Image>()
                        .Where(x => x.ParentId == parentId && x.TypeFor == 1).ToList();

                    if (obj != null)
                    {
                        list.AddRange(obj.Select(x => x.ID));
                    }
                }
                else
                {
                    var obj = new Image();

                    obj = session.CreateCriteria<Image>().List<Image>()
                        .Where(x => x.ID == parentId && x.TypeFor == 1)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        list.Add(obj.ID);
                    }
                }
            }

            return list.ToArray();
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

                if (file == null || file.ContentLength == 0)
                {
                    continue;
                }

                if (file.ContentLength > 1500000)
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
                    TypeFor = 1
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
	}
}