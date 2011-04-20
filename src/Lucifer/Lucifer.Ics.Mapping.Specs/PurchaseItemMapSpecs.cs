using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(PurchaseItemMap))]
    public class When_checking_persistence_specs_of_purchase_item : InMemoryDatabaseSpecs<PurchaseFamilyMap>
    {
        Because of = () =>
        {
            var spec = new PersistenceSpecification<PurchaseItem>(Session);
            var unitType = new UnitType();
            spec.TransactionalSave(unitType);
            var recipeUnit = new Unit { UnitType = unitType };
            spec.TransactionalSave(recipeUnit);
            var purchaseUnit = new Unit { UnitType = unitType };
            spec.TransactionalSave(purchaseUnit);
            var family = new PurchaseFamily();
            spec.TransactionalSave(family);

            _check = spec
                .CheckProperty(c => c.Name, "A purchase item")
                .CheckProperty(c => c.RecipeUnit, recipeUnit)
                .CheckReference(c => c.PurchaseUnit, purchaseUnit)
                .CheckProperty(c => c.PurchaseFamily, family)
                .CheckProperty(c => c.Version, 1);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

        static PersistenceSpecification<PurchaseItem> _check;

    }
}