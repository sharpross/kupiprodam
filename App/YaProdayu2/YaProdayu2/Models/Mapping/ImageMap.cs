using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Mapping
{
    public class ImageMap : ClassMap<Image>
    {
        public ImageMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.Data);
            Map(x => x.ParentId);
            Map(x => x.Type);
            Table("Images");
        }
    }
}