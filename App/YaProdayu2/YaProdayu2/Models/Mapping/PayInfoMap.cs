using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Mapping
{
    public class PayInfoMap : ClassMap<PayInfo>
    {
        public PayInfoMap()
        {
            Table("payinfo");
            Id(x => x.Id);
            Map(x => x.UserId);
            Map(x => x.DateBegin);
            Map(x => x.DateEnd);
        }
    }
}