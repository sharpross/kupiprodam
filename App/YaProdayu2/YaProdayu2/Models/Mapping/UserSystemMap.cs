using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Models.Mapping
{
    public class UserSystemMap : ClassMap<UserSystem>
    {
        public UserSystemMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Password);
            Map(x => x.Phone);
            Map(x => x.Email);
            Map(x => x.VKontakte);
            Map(x => x.Facebook);
            Map(x => x.About);
            Map(x => x.IsSeller);
            Map(x => x.Site);
            Map(x => x.Skype);
            Map(x => x.Organization);
            Map(x => x.CreationTime);
            Map(x => x.Post);
            Table("Users");
        }
    }
}