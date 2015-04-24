namespace YaProdayu2.Models.Mapping
{
    using FluentNHibernate.Mapping;
    using YaProdayu2.Models.Entities;

    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Country_id);
            Map(x => x.Name);
            Map(x => x.City_id);
            Table("Country");
        }
    }
}