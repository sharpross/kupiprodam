using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Views
{
    public class TenderMessageDetails
    {
        public TenderMessage Message { get; set; }

        public List<TenderMessage> SubMessages { get; set; }
    }
}