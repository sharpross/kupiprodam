using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models
{
    public class MessageModel
    {
        public UserSystem User { get; set; }

        public int UnReaded { get; set; }
    }
}