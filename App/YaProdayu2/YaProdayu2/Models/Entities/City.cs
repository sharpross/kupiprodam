using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models.Entities
{
    public class City
    {
        public virtual int Region_id { get; set; }

        public virtual int Country_id { get; set; }

        public virtual int City_id { get; set; }

        public virtual string Name { get; set; }
    }
}