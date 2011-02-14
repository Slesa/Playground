using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class ProductionItemMap : SubclassMap<ProductionItem>
    {
        public ProductionItemMap()
        {
            //References(d => d.Recipe).Cascade.All();
            HasMany(d => d.RecipeItems)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan();
        }
    }
}