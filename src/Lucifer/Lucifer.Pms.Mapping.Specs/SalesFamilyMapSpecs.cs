using FluentNHibernate.Testing;
using Lucifer.Pms.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Pms.Mapping.Specs
{
    [Subject(typeof(SalesFamilyMap))]
    public class When_checking_persistence_specs_of_sales_family : InMemoryDatabaseSpecs<SalesFamilyMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<SalesFamily>(Session);
            _check = spec
                .CheckProperty(c => c.Name, "A sales family")
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<SalesFamily> _check;

    }
}