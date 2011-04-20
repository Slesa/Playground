using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
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