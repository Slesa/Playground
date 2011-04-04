using FluentNHibernate.Mapping;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Mapping
{
    public class UserRoleItemMap : ClassMap<UserRoleItem>
    {
        public UserRoleItemMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Program).Length(40);
            Map(d => d.Module).Length(40);
            Map(d => d.Function).Length(40);

            Version(d => d.Version);
        }

    }
}