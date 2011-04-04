using FluentNHibernate.Mapping;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);

            Version(d => d.Version);
        }
    }
}
