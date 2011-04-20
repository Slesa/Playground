using FluentNHibernate.Testing;
using Lucifer.Pms.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Pms.Mapping.Specs
{
    [Subject(typeof(CurrencyMap))]
    public class When_checking_persistence_specs_of_currency : InMemoryDatabaseSpecs<CurrencyMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<Currency>(Session);
            _check = spec
                .CheckProperty(c => c.Name, "A currency")
                .CheckProperty(c => c.Contraction, "curr")
                .CheckProperty(c => c.Rate, 1.42m)
                .CheckProperty(c => c.DecimalPosition, 2)
                .CheckProperty(c => c.DecimalChar, ',')
                .CheckProperty(c => c.ThousandChar, '.')
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<Currency> _check;

    }
}
