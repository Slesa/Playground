using FluentNHibernate.Mapping;
using Godot.PmsModel.Entities;

namespace Godot.PmsNHibernate.Mappings
{
    public class CostcenterMap : ClassMap<Costcenter>
    {
        public CostcenterMap()
        {
            Table("Costcenters");
            Id(d => d.Id).GeneratedBy.Identity();
            //Map(d => d.MatrixId);
            Map(d => d.Name).Length(50);
        }
    }
}