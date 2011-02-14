using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof(StockMapperMap))]
    public class When_checking_persistence_specs_of_stock_mapper_map : InMemoryDatabaseSpecs
    {
        Establish context = () => { _session = Configuration.BuildSessionFactory().OpenSession(); };

        Because of = () =>
        {
            _check = new PersistenceSpecification<StockMapper>(_session)
                .CheckProperty(c => c.Costcenter, 43)
                .CheckReference(c => c.ProductionItem, new ProductionItem {Name="Production item"})
                .CheckReference(c => c.Stock, new Stock { Name = "Stock" });
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<StockMapper> _check;
        static ISession _session;
    }
}