using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class PayInfo
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual DateTime DateBegin { get; set; }

        public virtual DateTime DateEnd { get; set; }

        public virtual string TempData { get; set; }
    }
}