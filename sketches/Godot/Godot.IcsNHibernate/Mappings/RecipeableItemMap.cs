using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class RecipeableItemMap : ClassMap<RecipeableItem>
    {
        public RecipeableItemMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(50);
            References(d => d.RecipeUnit).Not.Nullable();
            Version(d => d.Version);

            DiscriminateSubClassesOnColumn("type");
        }
    }
}