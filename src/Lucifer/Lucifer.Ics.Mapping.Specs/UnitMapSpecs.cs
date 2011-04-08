using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(UnitMap))]
    public class When_checking_persistence_specs_of_unit : InMemoryDatabaseSpecs<UnitMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<Unit>(Session);
            var unitType = new UnitType();
            spec.TransactionalSave(unitType);
            var parent = new Unit { UnitType = unitType };
            spec.TransactionalSave(parent);

            _check = spec
                .CheckProperty(c => c.Name, "A Unit Type")
                .CheckProperty(c => c.Contraction, "kg")
                .CheckReference(c => c.UnitType, unitType)
                .CheckReference(c => c.Parent, parent)
                .CheckProperty(c => c.FactorToParent, 1.42m)
                .CheckProperty(c => c.Version, 1)
                .CheckProperty(c => c.Purchasing, true)
                .CheckProperty(c => c.Reciping, true);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<Unit> _check;
    }
}