using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupiProdam.Utils
{
    public class Breadcrumbs
    {
        public List<BreadcrumbsItem> GetBreadcrumbs(string controller, string method)
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

                    break;
            }

            return result;
        }
    }
}
