using FluentNHibernate.Mapping;

namespace YaProdayu2.Models.UserMessages
{
    public class UserMessageMap : ClassMap<UserMessage>
    {
        public UserMessageMap()
        {
            Id(x => x.Id);
            Map(x => x.UserId);
            Map(x => x.ToUserId);
            Map(x => x.Message);
            Map(x => x.DateCreation);
            Table("UserMessages");
        }
    }
}