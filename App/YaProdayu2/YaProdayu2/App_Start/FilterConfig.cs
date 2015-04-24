using FluentSecurity;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YaProdayu2.App_Start;
using YaProdayu2.Controllers;
using YaProdayu2.Models.Entities;
using YaProdayu2.Models.Views;
using YaProdayu2.Y2System;

namespace YaProdayu2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            SecurityConfigurator.Configure(configuration =>
            {
                //configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                configuration.GetAuthenticationStatusFrom(() => IsAuth(HttpContext.Current));

                configuration.ForAllControllers().DenyAnonymousAccess();

                configuration.For<AccountController>().DenyAnonymousAccess();
                configuration.For<BuyerController>().DenyAnonymousAccess();
                configuration.For<SellerController>().DenyAnonymousAccess();
                configuration.For<CutawayController>().DenyAnonymousAccess();

                configuration.For<ImageController>().AllowAny();

                configuration.For<AccountController>(ac => ac.Login(null, null, null)).Ignore();
                //configuration.For<AccountController>(ac => ac.Logout()).Ignore();
                configuration.For<AccountController>(ac => ac.Registration()).Ignore();
                configuration.For<AccountController>(ac => ac.RegSeller()).Ignore();
                configuration.For<AccountController>(ac => ac.RegBuyer()).Ignore();
                configuration.For<AccountController>(ac => ac.UpdateAvatar()).Ignore();
                configuration.For<AccountController>(ac => ac.Card(string.Empty)).Ignore();

                configuration.For<ImageController>(ac => ac.Get(new int())).Ignore();

                configuration.For<BuyerController>(ac => ac.CreateTender(string.Empty)).Ignore();
                configuration.For<BuyerController>(ac => ac.GetListCityes(string.Empty)).Ignore();
                configuration.For<HomeController>().Ignore();
                configuration.For<ErrorController>().Ignore();

                configuration.ResolveServicesUsing(type =>
                {
                    if (type == typeof(IPolicyViolationHandler))
                    {
                        var types = Assembly
                            .GetAssembly(typeof(MvcApplication))
                            .GetTypes()
                            .Where(x => typeof(IPolicyViolationHandler).IsAssignableFrom(x)).ToList();

                        var handlers = types.Select(t => Activator.CreateInstance(t) as IPolicyViolationHandler).ToList();

                        return handlers;
                    }
                    return Enumerable.Empty<object>();
                });
            });

            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleSecurityAttribute(), 0);
        }

        private static bool IsAuth(HttpContext context)
        {
            HttpCookie authCookie = context.Request.Cookies.Get("__AUTH_COOKIE");
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var exist = false;

                using (var session = DBHelper.OpenSession())
                {
                    exist = session.CreateCriteria<UserSystem>().List<UserSystem>()
                        .Where(rec => rec.Login == ticket.Name)
                        .FirstOrDefault() != null;
                }

                return exist;
            }

            return false;
        }
    }
}
