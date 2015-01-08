using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class Tender
    {
        public virtual int Id { get; set; }

        public virtual int UserId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime DateCreation { get; set; }

        public virtual int Status { get; set; }

        public virtual string Theme { get; set; }

        public virtual string SubTheme { get; set; }

        public virtual string City { get; set; }

        public virtual string ActiviteTime { get; set; }

        public virtual bool AllowWriteMe { get; set; }

        public virtual string TypeTender { get; set; }

        public virtual int Cost { get; set; }

        public virtual int[] Photos { get; set; }

    }
}