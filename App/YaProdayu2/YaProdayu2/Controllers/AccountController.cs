using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        private string GetPayString()
        {
            // регистрационная информация (логин, пароль #1)
            // registration info (login, password #1)
            string sMrchLogin = "alfa-tender";
            string sMrchPass1 = "my-tend201";

            // номер заказа
            // number of order
            int nInvId = 0;

            // описание заказа
            // order description
            string sDesc = "Оплата подписки на Я-Прелагаю";

            // сумма заказа
            // sum of order
            string sOutSum = "100.00";

            // тип товара
            // code of goods
            string sShpItem = "1";

            // язык
            // language
            string sCulture = "ru";

            // кодировка
            // encoding
            string sEncoding = "utf-8";

            // формирование подписи
            // generate signature
            string sCrcBase = string.Format("{0}:{1}:{2}:{3}:shpItem={4}",
                                sMrchLogin, sOutSum, nInvId, sMrchPass1, sShpItem);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bSignature = md5.ComputeHash(Encoding.UTF8.GetBytes(sCrcBase));

            StringBuilder sbSignature = new StringBuilder();
            foreach (byte b in bSignature)
                sbSignature.AppendFormat("{0:x2}", b);

            string sCrc = sbSignature.ToString();

            // HTML-страница с кассой
            // ROBOKASSA HTML-page
            // ltKassa is System.Web.UI.WebControls.Literal;

            return "<script language=JavaScript " +
                                    "src=\"https://auth.robokassa.ru/Merchant/PaymentForm/FormMS.js?" +
                                    "MerchantLogin=" + sMrchLogin +
                                    "&OutSum=" + sOutSum +
                                    "&InvoiceID=" + nInvId +
                                    "&shpItem=" + sShpItem +
                                    "&SignatureValue=" + sCrc +
                                    "&Description=" + sDesc +
                                    "&Culture=" + sCulture +
                                    "&Encoding=" + sEncoding + "\"></script>";
        }

        private ProfileView GetProfile(int id)
        {
            var user = new UserSystemService().Get(id); // this.Auth.CurrentUser;

            //var prof = new ProfileView();

            var listSubsciptions = new List<Subsciptions>();

            using (var session = DBHelper.OpenSession())
            {
                listSubsciptions = session.CreateCriteria<Subsciptions>().List<Subsciptions>()
                    .Where(x => x.UserId == id)
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
                ListImages = new List<int>(this.GetImagesFirst(user.Id, true)),
                ListCountrys = this.GetCountrys(),
                ListRegions = new List<string>(),
                PayScript = this.GetPayString(),
                PayEnd = new DateTime(),
                PayHistory = new List<string>()
            };

            return profile;
        }

        [HttpGet]
        public ActionResult Profile()
        {
            var user = this.Auth.CurrentUser;

            if (user != null)
            {
                return View(this.GetProfile(this.Auth.CurrentUser.Id));
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

            var prof = this.GetProfile(userSystem.Id);

            if (prof != null)
            {
                return View(prof);
            }

            return RedirectToAction("Index", "Home");
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
                Login = model.Email.ToLower(),
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

            this.Auth.Login(user.Login);

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
                Login = model.Email.ToLower(),
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

            this.Auth.Login(user.Login);

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
                user = this.Auth.Login(login.ToLower(), password, remembeMe == null ? false : (bool)remembeMe);

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

        [HttpPost]
        public JsonResult SaveCitys(string[] citys)
        {
            if (citys != null && citys.Length > 0)
            {
                foreach (var city in citys)
                {
                    var listCity = new List<City>();

                    using (var session = DBHelper.OpenSession())
                    {
                        var objCity = session.CreateCriteria<City>().List<City>()
                            .Where(x => x.Name == city)
                            .FirstOrDefault();

                        if (objCity != null)
                        {
                            session.Save(new SubsciptionsCitys() {
                                UserId = this.Auth.CurrentUser.Id,
                                CityId = objCity.City_id
                            });
                        }
                    }
                }
                
                return Json(new
                {
                    Success = false,
                    Data = "null"
                });
            }

            return Json(new
            {
                Success = false,
                Data = "null"
            });
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

        public List<string> GetCountrys()
        {
            var list = new List<string>();
            list.Add("Россия");
            var tempList = new List<string>();

            using (var session = DBHelper.OpenSession())
            {
                tempList = session.CreateCriteria<Country>()
                    .List<Country>()
                    .Where(x => x.Country_id == 9908 ||
                        x.Country_id == 248 ||
                        x.Country_id == 9787 ||
                        x.Country_id == 1894 ||
                        x.Country_id == 1280 ||
                        x.Country_id == 81 ||
                        x.Country_id == 2788 ||
                        x.Country_id == 2303 ||
                        x.Country_id == 9575 ||
                        x.Country_id == 245 ||
                        x.Country_id == 9638)
                    //.Where(x => x.Country_id == 9908)
                    .Select(x => x.Name)
                    .OrderBy(x => x)
                    .ToList();
            }

            list.AddRange(list);

            return list;
        }

        private void SaveImages(HttpPostedFileBase[] files, int parentId)
        {
            var i = 0;

            foreach (var file in files)
            {
                if (i == 19)
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