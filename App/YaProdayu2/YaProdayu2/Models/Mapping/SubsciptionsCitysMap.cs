namespace YaProdayu2.Models.Mapping
{
    using FluentNHibernate.Mapping;
    using YaProdayu2.Models.Entities;

    public class SubsciptionsCitysMap : ClassMap<SubsciptionsCitys>
    {
        public SubsciptionsCitysMap()
        {
            Id(x => x.Id);
            Map(x => x.UserId);
            Map(x => x.CityId);
            Table("Subsciptions_cytis");
        }
    }
}