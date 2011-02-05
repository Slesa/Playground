using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class PurchaseItemMap : SubclassMap<PurchaseItem>
    {
        public PurchaseItemMap()
        {
            References(d => d.PurchaseFamily);
            References(d => d.PurchaseUnit);
        }
    }
}