using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMapperMap : ClassMap<StockMapper>
    {
        public StockMapperMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Costcenter);
            References(d => d.RecipeableItem);
            References(d => d.Stock);
        }
    }
}