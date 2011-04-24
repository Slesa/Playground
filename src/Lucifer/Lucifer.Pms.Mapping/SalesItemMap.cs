using FluentNHibernate.Mapping;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Mapping
{
    public class SalesItemMap : ClassMap<SalesItem>
    {
        public SalesItemMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);
            References(d => d.SalesFamily).Not.Nullable();

            Version(d => d.Version);
            
        }
    }
}