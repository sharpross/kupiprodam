using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class Subsciptions
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual string Theme { get; set; }
    }
}