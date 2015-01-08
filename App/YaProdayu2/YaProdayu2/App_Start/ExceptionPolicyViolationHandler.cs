using FluentSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YaProdayu2.App_Start
{
    public class ExceptionPolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            return new RedirectResult("~/Account//Registration");
        }
    }
}