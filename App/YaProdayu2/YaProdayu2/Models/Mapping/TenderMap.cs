using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Mapping
{
    public class TenderMap : ClassMap<Tender>
    {
        public TenderMap()
        {
            Id(x => x.Id);
            Map(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Message);
            Map(x => x.From);
            Map(x => x.Theme);
            Map(x => x.SubTheme);
            Map(x => x.DateCreation);
            Map(x => x.City);
            Map(x => x.ActiviteTime);
            Map(x => x.AllowWriteMe);
            Map(x => x.Cost);
            Map(x => x.TypeTender);
            Table("Tender");
        }
    }
}