using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using Godot.PmsMatrix.Persistence;
using Godot.PmsModel.Entities;

namespace Godot.PmsMatrix.Mappings
{
    public class SalesItemMap : ClassMap<SalesItem>
    {
        public SalesItemMap()
        {
            Tuplizer(TuplizerMode.Poco, typeof(MatrixFileTuplizer));

            Table("Articles");
            Id(d => d.Id).GeneratedBy.Assigned();
/*            Map(d => d.Plu);
            Map(d => d.Name).Length(50);
*/

            //Id(x => x.Id).GeneratedBy.HiLo("10");
            //.Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore);
        }
    }
}