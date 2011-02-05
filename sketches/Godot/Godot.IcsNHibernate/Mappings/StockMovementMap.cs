using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMovementMap : ClassMap<StockMovement>
    {
        public StockMovementMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.ExecutedAt).Not.Nullable();
            References(d => d.OfStock);
            HasMany(d => d.MoveItems)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan();

            DiscriminateSubClassesOnColumn("type");
        }

    }
}