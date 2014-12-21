using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaProdayu2.Y2System.Utils;
using YaProdayu2.Y2System;

namespace YaProdayu2.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var list = new DictionaryThemes().List;

            return View(list);
        }
    }
}