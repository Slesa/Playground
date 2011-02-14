using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockItemMap : ClassMap<StockItem>
    {
        public StockItemMap()
        {
            //Table("StockItems");
            Id(d => d.Id).GeneratedBy.HiLo("10");
            References(d => d.Stock);
            References(d => d.RecipeableItem);
            Map(d => d.Quantity);
            References(d => d.Unit);
        }
    }
}