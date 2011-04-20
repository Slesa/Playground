using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(PurchaseFamilyMap))]
    public class When_checking_persistence_specs_of_purchase_family : InMemoryDatabaseSpecs<PurchaseFamilyMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<PurchaseFamily>(Session);
            _check = spec
                .CheckProperty(c => c.Name, "A purchase family")
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<PurchaseFamily> _check;
        
    }
}