using FluentNHibernate.Mapping;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Mapping
{
    public class UnitTypeMap : ClassMap<UnitType>
    {
        public UnitTypeMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);

            Version(d => d.Version);
        }
    }
}