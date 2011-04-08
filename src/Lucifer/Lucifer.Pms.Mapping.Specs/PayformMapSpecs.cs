using FluentNHibernate.Testing;
using Lucifer.Pms.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Pms.Mapping.Specs
{
    [Subject(typeof(CurrencyMap))]
    public class When_checking_persistence_specs_of_payform : InMemoryDatabaseSpecs<PayformMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<Payform>(Session);
            _check = spec
                .CheckProperty(c => c.Name, "A payform")
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<Payform> _check;

    }
}