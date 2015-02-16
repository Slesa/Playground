using Database.Models;
using FluentNHibernate.Mapping;

namespace Database.Persistence
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");

            Map(d => d.Name).Length(40);
            References(d => d.UserRole).Not.Nullable();

            Version(d => d.Version);
        }         
    }
}