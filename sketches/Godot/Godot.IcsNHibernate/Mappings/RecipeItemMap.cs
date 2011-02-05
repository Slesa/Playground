using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class RecipeItemMap : ClassMap<RecipeItem>
    {
        public RecipeItemMap()
        {
            //Table("RecipeItems");
            Id(d => d.Id).GeneratedBy.HiLo("10");
            References(d => d.Recipe);
            References(d => d.RecipeableItem);
            Map(d => d.Quantity);
            References(d => d.Unit);
        }
    }
}