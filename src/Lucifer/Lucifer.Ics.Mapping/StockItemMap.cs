using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
{
    public class StockItemMap : ClassMap<StockItem>
    {
        public StockItemMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            References(d => d.Stock);
            References(d => d.RecipeableItem);
            Map(d => d.Quantity);
            References(d => d.Unit);
        }
    }
}