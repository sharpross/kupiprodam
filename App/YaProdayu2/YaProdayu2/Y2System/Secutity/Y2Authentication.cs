using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using YaProdayu2.Models.Entities;
using YaProdayu2.Y2System.Security;
using YaProdayu2.Y2System;
using YaProdayu2.Y2System.Utils;

namespace YaProdayu2.Y2System.Secutity
{
    public class Y2Authentication : IAuthentication
    {
        private UserSystem CurrentSystemUser { get; set; }

        private const string CookieName = "__AUTH_COOKIE";

        public HttpContextBase HttpContext { get; set; }

        public UserSystem Login(string login, string password, bool isPersistent = false)
        {
            UserSystem user = null;

            using (var session = DBHelper.OpenSession())
            {
                var md5Password = MD5Helper.GetMD5String(password);

                user = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Login == login && rec.Password == md5Password)
                    .FirstOrDefault();

                if (user != null)
                {
                    this.CreateCookie(login, isPersistent);
                }
            }

            return user;
        }

        public UserSystem Login(string login)
        {
            UserSystem user = null;

            using (var session = DBHelper.OpenSession())
            {
                user = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Login == login)
                    .FirstOrDefault();

                if (user != null)
                {
                    this.CreateCookie(login);
                }
            }

            return user;
        }

        public void LogOut()
        {
            var httpCookie = this.HttpContext.Response.Cookies[CookieName];

            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
                FormsAuthentication.SignOut();
            }
        }

        public UserSystem CurrentUser
        {
            get
            {
                if (this.CurrentSystemUser == null && this.HttpContext != null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(CookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            using (var session = DBHelper.OpenSession())
                            {
                                
                                this.CurrentSystemUser = session.CreateCriteria<UserSystem>().List<UserSystem>()
                                    .Where(rec => rec.Login == ticket.Name)
                                    .FirstOrDefault();
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return this.CurrentSystemUser;
            }
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var issuer = DateTime.Now.AddDays(90);
            //return (long)elapsedTime.TotalSeconds;

            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  issuer,
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(new TimeSpan(90, 0, 0, 0))
            };

            this.HttpContext.Response.Cookies.Set(AuthCookie);

            //FormsAuthentication.SetAuthCookie(userName, false);
        }
    }
}