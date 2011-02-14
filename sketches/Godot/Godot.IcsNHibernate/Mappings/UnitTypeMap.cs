using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
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