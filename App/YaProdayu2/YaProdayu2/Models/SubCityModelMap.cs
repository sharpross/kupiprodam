using FluentNHibernate.Mapping;

namespace YaProdayu2.Models
{
    public class SubCityModelMap : ClassMap<SubCityModel>
    {
        public SubCityModelMap()
        {
            Table("Subsciptions_cytis");
            Id(x => x.Id);
            Map(x => x.CityId_1);
            Map(x => x.CityId_2);
            Map(x => x.CityId_3);
            Map(x => x.CityId_4);
            Map(x => x.CityId_5);
        }
    }
}
