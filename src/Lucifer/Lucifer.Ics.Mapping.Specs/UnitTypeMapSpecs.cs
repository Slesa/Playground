using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(UnitTypeMap))]
    public class When_checking_persistence_specs_of_unittype : InMemoryDatabaseSpecs<UnitTypeMap>
    {
        Because of = () =>
            {
                var spec = new PersistenceSpecification<UnitType>(Session);
                _check = spec
                    .CheckProperty(c => c.Name, "A Unit Type")
                    .CheckProperty(c => c.Version, 1);
            };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<UnitType> _check;
    }

}
