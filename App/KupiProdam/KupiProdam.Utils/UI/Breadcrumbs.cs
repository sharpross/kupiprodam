using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupiProdam.Utils
{
    public static class Breadcrumbs
    {
        public static List<BreadcrumbsItem> Get(string controller, string method)
        {
            var index = "Index";

            var result = new List<BreadcrumbsItem>();

            switch(controller)
            {
                case "Home":
                    result.Add(new BreadcrumbsItem() 
                    {
                        Controller = controller,
                        Method = index,
                        Title = Constants.Cotrollers.Title_Home
                    });
                    switch (method)
                    {
                        case "About":
                            result.Add(new BreadcrumbsItem()
                            {
                                Controller = controller,
                                Method = "About",
                                Title = Constants.Cotrollers.Title_About,
                                IsActive = true
                            });
                            break;
                    }
                    break;
                case "Account":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = controller,
                        Method = index,
                        Title = Constants.Cotrollers.Title_Account
                    });
                    break;
                case "Seller":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = controller,
                        Method = index,
                        Title = Constants.Cotrollers.Title_Sallers
                    });
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = controller,
                        Method = "Index",
                        Title = Constants.Common.Catalog,
                        IsActive = true
                    });
                    break;
                case "Buyer":
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = controller,
                        Method = index,
                        Title = Constants.Cotrollers.Title_Buyers
                    });
                    result.Add(new BreadcrumbsItem()
                    {
                        Controller = controller,
                        Method = "Index",
                        Title = Constants.Common.Catalog,
                        IsActive = true
                    });
                    break;
            }

            return result;
        }
    }
}
