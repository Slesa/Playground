using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using Godot.PmsMatrix.Persistence;
using Godot.PmsModel.Entities;

namespace Godot.PmsMatrix.Mappings
{
    public class CostcenterMap : ClassMap<Costcenter>
    {
        public CostcenterMap()
        {
            Tuplizer(TuplizerMode.Poco, typeof(MatrixFileTuplizer));

            Table("Costcenters");
            Id(d => d.Id).GeneratedBy.Assigned();

            /*
            Id(d => d.Id).GeneratedBy.Identity();
            Map(d => d.MatrixId);
            Map(d => d.Name).Length(50);
             * */
        }
    }
}