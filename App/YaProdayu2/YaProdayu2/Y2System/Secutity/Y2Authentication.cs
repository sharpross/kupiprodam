using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using YaProdayu2.Models.Entities;
using YaProdayu2.Y2System.Security;
using YaProdayu2.Y2System.System;

namespace YaProdayu2.Y2System.Secutity
{
    public class Y2Authentication : IAuthentication
    {
        private const string CookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }

        public UserSystem Login(string login, string password, bool isPersistent)
        {
            UserSystem user = null;

            using (var session = DBHelper.OpenSession())
            {
                var md5Password = password;

                user = session.CreateCriteria<UserSystem>().List<UserSystem>()
                    .Where(rec => rec.Email == login && rec.Password == md5Password)
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
                    .Where(rec => rec.Email == login)
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
            throw new NotImplementedException();
        }

        public UserSystem CurrentUser
        {
            get
            {
                if (this.CurrentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(CookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            using (var session = DBHelper.OpenSession())
                            {
                                
                                return session.CreateCriteria<UserSystem>().List<UserSystem>()
                                    .Where(rec => rec.Email == ticket.Name)
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
                        
                    }
                }

                return this.CurrentUser;
            }
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(AuthCookie);
        }
    }
}