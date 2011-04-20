using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
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