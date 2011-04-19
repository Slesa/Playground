
using FluentNHibernate.Mapping;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Mapping
{
    public class CurrencyMap : ClassMap<Currency>
    {
        public CurrencyMap()
        {
            Id(d => d.Id).GeneratedBy.HiLo("10");
            Map(d => d.Name).Length(40);
            Map(d => d.Contraction).Length(10);

            Version(d => d.Version);
        }
    }
}
