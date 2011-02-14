using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMoveItemMap : ClassMap<StockMoveItem>
    {
        public StockMoveItemMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Quantity);
            References(d => d.RecipeableItem).Not.Nullable();
            References(d => d.Unit).Not.Nullable();
            References(d => d.StockMovement);
        }
    }
}