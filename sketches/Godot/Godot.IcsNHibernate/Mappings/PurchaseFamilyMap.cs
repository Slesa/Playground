using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class PurchaseFamilyMap : ClassMap<PurchaseFamily>
    {
        public PurchaseFamilyMap()
        {
            //Table("PurchaseFamilies");
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(50);
            Version(d => d.Version);
        }
    }
}