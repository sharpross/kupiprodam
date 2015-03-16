using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaProdayu2.Models
{
    public class ContactsMap : ClassMap<Contacts>
    {
        public ContactsMap()
        {
            Id(x => x.Id);
            Map(x => x.UserId);
            Map(x => x.ContactId);
            Table("Contacts");
        }
    }
}