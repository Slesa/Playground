using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
{
    public class RecipeItemMap : ClassMap<RecipeItem>
    {
        public RecipeItemMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            References(d => d.Recipe);
            References(d => d.RecipeableItem);
            Map(d => d.Quantity);
            References(d => d.Unit);
        }
    }
}