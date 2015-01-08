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

        public bool HaveMyMessages { get; set; }

        public List<MessageInfo> Messages { get; set; }

        public SellerTenderDetailsView()
        {
            this.Messages = new List<MessageInfo>();
        }
    }

    public class MessageInfo
    {
        public UserSystem User { get; set; }

        public TenderMessage Messages { get; set; }
    }
}