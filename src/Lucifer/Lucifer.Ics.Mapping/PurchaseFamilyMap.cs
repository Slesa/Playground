using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
{
    public class PurchaseFamilyMap : ClassMap<PurchaseFamily>
    {
        public PurchaseFamilyMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);

            Version(d => d.Version);
        }
    }
}