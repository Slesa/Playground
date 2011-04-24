using FluentNHibernate.Mapping;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Mapping
{
    public class SalesFamilyMap : ClassMap<SalesFamily>
    {
        public SalesFamilyMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);

            Version(d => d.Version);
        }
    }
}