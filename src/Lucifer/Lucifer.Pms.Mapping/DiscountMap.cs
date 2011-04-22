using FluentNHibernate.Mapping;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Mapping
{
    public class DiscountMap : ClassMap<Discount>
    {
        public DiscountMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);
            Map(d => d.Rate).Default("1.0");

            Version(d => d.Version);
        }
    }
}