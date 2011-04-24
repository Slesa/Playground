using FluentNHibernate.Testing;
using Lucifer.Pms.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Pms.Mapping.Specs
{
    [Subject(typeof(SalesItemMap))]
    public class When_checking_persistence_specs_of_sales_item : InMemoryDatabaseSpecs<SalesItemMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<SalesItem>(Session);
            var salesFamily = new SalesFamily();
            spec.TransactionalSave(salesFamily);
            _check = spec
                .CheckProperty(c => c.Name, "A sales item")
                .CheckReference(c => c.SalesFamily, salesFamily)
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<SalesItem> _check;

    }
}