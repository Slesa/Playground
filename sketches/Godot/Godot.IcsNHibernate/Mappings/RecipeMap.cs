using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class RecipeMap : ClassMap<Recipe>
    {
        public RecipeMap()
        {
            //Table("Recipes");
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Plu);
            HasMany(d => d.RecipeItems)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan();
                //.Cascade.All();
            Version(d => d.Version);
        }
    }
}