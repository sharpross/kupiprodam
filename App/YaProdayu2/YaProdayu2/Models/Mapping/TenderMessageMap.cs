using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Mapping
{
    public class TenderMessageMap : ClassMap<TenderMessage>
    {
        public TenderMessageMap()
        {
            Id(x => x.Id);
            Map(x => x.TenderId);
            Map(x => x.FromUserId);
            Map(x => x.Message);
            ///Map(x => x.Coste);
            Map(x => x.CreationTime);
            Map(x => x.IsSubMessage);
            Map(x => x.ParentMessageId);
            Table("TenderMessage");
        }
    }
}