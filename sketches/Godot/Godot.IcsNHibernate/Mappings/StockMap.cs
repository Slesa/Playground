using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMap : ClassMap<Stock>
    {
        public StockMap()
        {
            //Table("Stocks");
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(50);
            Map(d => d.IsMainStock);
            HasMany(d => d.StockItems)
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.AllDeleteOrphan();
                
                //.Cascade.All();
            Version(d => d.Version);
        }
    }
}