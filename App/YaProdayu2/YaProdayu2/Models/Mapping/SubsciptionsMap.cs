namespace YaProdayu2.Models.Mapping
{
    using FluentNHibernate.Mapping;
    using YaProdayu2.Models.Entities;

    public class SubsciptionsMap : ClassMap<Subsciptions>
    {
        public SubsciptionsMap()
        {
            Id(x => x.Id);
            Map(x => x.UserId);
            Map(x => x.Theme);
            Table("Subsciptions");
        }
    }
}