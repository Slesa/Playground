using FluentNHibernate.Mapping;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Mapping
{
    public class UserRoleMap : ClassMap<UserRole>
    {
        public UserRoleMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);

            Version(d => d.Version);
        }
    }
}