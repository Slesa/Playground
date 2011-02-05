using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (StockMapperMap))]
    public class When_checking_persistence_specs_of_stockmapper : InMemoryDatabaseSpecs<StockMapperMap>
    {
        static PersistenceSpecification<StockMapper> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<StockMapper>(Session);
                var recipeUnitType = new UnitType();
                spec.TransactionalSave(recipeUnitType);
                var unit = new Unit {UnitType = recipeUnitType};
                spec.TransactionalSave(unit);
                var purchaseItem = new PurchaseItem { RecipeUnit = unit };
                spec.TransactionalSave(purchaseItem);
                var stock = new Stock();
                spec.TransactionalSave(stock);

                _check = spec
                    .CheckProperty(c => c.Costcenter, 42)
                    .CheckReference(c => c.RecipeableItem, purchaseItem)
                    .CheckReference(c => c.Stock, stock);
            };

        It should_be_verified = () => _check.VerifyTheMappings();
    }
}