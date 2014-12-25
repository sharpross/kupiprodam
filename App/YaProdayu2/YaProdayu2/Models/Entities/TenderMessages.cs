using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class TenderMessage
    {
        public virtual int Id { get; set; }

        public virtual int TenderId { get; set; }

        public virtual int FromUserId { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual decimal Coste { get; set; }
    }
}