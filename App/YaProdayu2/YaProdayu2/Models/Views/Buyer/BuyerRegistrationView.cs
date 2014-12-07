using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Views.Buyer
{
    public class BuyerRegistrationView
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAcceptRules { get; set; }

        public string Errors { get; set; }
    }
}