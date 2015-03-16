using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Mapping
{
    public class RegionMap : ClassMap<Region>
    {
        public RegionMap()
        {
            Id(x => x.Region_id);
            Map(x => x.Name);
            Map(x => x.City_id);
            Map(x => x.Country_id);
            Table("Region");
        }
    }
}