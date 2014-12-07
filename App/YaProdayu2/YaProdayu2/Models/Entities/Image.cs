using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class Image
    {
        public virtual int ID { get; set; }

        public virtual string Title { get; set; }

        public virtual Byte[] Data { get; set; }
    }
}