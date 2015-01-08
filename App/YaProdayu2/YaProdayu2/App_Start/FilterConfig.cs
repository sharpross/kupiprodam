using FluentSecurity;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.App_Start;
using YaProdayu2.Controllers;
using YaProdayu2.Models.Views;

namespace YaProdayu2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            SecurityConfigurator.Configure(configuration =>
            {
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                
                configuration.ForAllControllers().DenyAnonymousAccess();

                configuration.For<AccountController>().DenyAnonymousAccess();
                configuration.For<BuyerController>().DenyAnonymousAccess();
                configuration.For<SellerController>().DenyAnonymousAccess();
                configuration.For<ImageController>().AllowAny();

                configuration.For<AccountController>(ac => ac.Login(null, null, null)).Ignore();
                //configuration.For<AccountController>(ac => ac.Logout()).Ignore();
                configuration.For<AccountController>(ac => ac.Registration()).Ignore();
                configuration.For<AccountController>(ac => ac.RegSeller()).Ignore();
                configuration.For<AccountController>(ac => ac.RegBuyer()).Ignore();
                configuration.For<BuyerController>(ac => ac.CreateTender(string.Empty)).Ignore();
                configuration.For<HomeController>().Ignore();
                configuration.For<ErrorController>().Ignore();

                /*configuration.ResolveServicesUsing(type =>
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
                });*/

                configuration.DefaultPolicyViolationHandlerIs<ExceptionPolicyViolationHandler>();
            });

            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleSecurityAttribute(), 0);
        }
    }
}
