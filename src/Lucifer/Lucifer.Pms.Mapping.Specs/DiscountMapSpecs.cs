using FluentNHibernate.Testing;
using Lucifer.Pms.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Pms.Mapping.Specs
{
    [Subject(typeof(DiscountMap))]
    public class When_checking_persistence_specs_of_discount : InMemoryDatabaseSpecs<DiscountMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<Discount>(Session);
            _check = spec
                .CheckProperty(c => c.Name, "A discount")
                .CheckProperty(c => c.Rate, 1.42m)
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<Discount> _check;

    }
}