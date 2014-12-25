using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Views
{
    public class SellerTenderDetailsView
    {
        public Tender TenderInfo { get; set; }

        public UserSystem User { get; set; }

        public TenderMessage Message { get; set; }
    }
}