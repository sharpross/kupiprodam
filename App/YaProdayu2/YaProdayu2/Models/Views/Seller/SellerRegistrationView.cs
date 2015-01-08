using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Views
{
    public class SellerRegistrationView
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Organization { get; set; }

        public string Post { get; set; }

        public bool IsAcceptRules { get; set; }
    }
}