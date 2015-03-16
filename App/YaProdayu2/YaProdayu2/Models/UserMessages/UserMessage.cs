using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.UserMessages
{
    public class UserMessage
    {
        public virtual int Id {get; set;}

        public virtual int UserId {get; set;}

        public virtual int ToUserId {get; set;}

        public virtual DateTime DateCreation {get; set;}

        public virtual string Message { get; set; }

        public virtual UserSystem User { get; set; }

        public virtual UserSystem ToUser { get; set; }

        public virtual bool IsRead { get; set; }
    }
}