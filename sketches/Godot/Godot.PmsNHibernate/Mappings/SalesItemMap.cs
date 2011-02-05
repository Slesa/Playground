using FluentNHibernate.Mapping;
using Godot.PmsModel.Entities;

namespace Godot.PmsNHibernate.Mappings
{
    public class SalesItemMap : ClassMap<SalesItem>
    {
        public SalesItemMap()
        {
            Table("SalesItems");
            Id(d => d.Id).GeneratedBy.Identity();
            //Map(d => d.Plu);
            Map(d => d.Name).Length(50);
        }
    }
}