using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class Image
    {
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual Byte[] Data { get; set; }

        public virtual string Type { get; set; }

        public virtual int ParentId { get; set; }

        /// <summary>
        /// 0 - тендер, 1 - портфоли
        /// </summary>
        public virtual int TypeFor { get; set; }
    }
}