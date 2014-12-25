using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Views
{
    public class TenderAddMessageView
    {
        public int UserID { get; set; }

        public int TenderId { get; set; }

        public decimal Cost { get; set; }

        public string Message { get; set; }
    }
}