using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YaProdayu2.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Unauthorised()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}