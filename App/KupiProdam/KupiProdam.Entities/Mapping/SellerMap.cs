using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using KupiProdam.Entities.Entites;

namespace KupiProdam.Entities.Mapping
{
    public class SellerMap : ClassMap<Seller>
    {
        public SellerMap()
        {
            Id(x => x.Id);
            Map(x => x.Name)
                .Not
                .Nullable();
            Map(x => x.Password)
                .Not
                .Nullable();
            Map(x => x.About);
            Map(x => x.Email)
                .Not
                .Nullable(); ;
            Map(x => x.MainPhone);
            Map(x => x.Photo);
            Map(x => x.Site);
            Map(x => x.Skype);
            Map(x => x.VKontakte);
            Map(x => x.Facebook);
        }
    }
}
