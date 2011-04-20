using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
{
    public class UnitMap : ClassMap<Unit>
    {
        public UnitMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);
            Map(d => d.Contraction).Length(15);
            References(d => d.UnitType).Not.Nullable();
            References(d => d.Parent);
            Map(d => d.FactorToParent);
            Map(d => d.Purchasing);
            Map(d => d.Reciping);

            //HasMany(d => d.Children)
            //    .Access.CamelCaseField(Prefix.Underscore)
            //    .Cascade.AllDeleteOrphan();
            
            Version(d => d.Version);

        }
    }
}
