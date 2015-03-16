namespace YaProdayu2.Models.Mapping
{
    using FluentNHibernate.Mapping;
    using YaProdayu2.Models.Entities;

    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Id(x => x.City_id);
            Map(x => x.Name);
            Map(x => x.Region_id);
            Map(x => x.Country_id);
            Table("City");
        }
    }
}