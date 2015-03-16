using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models
{
    public class Contacts
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual int ContactId { get; set; }
    }
}