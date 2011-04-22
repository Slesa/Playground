using FluentNHibernate.Testing;
using Lucifer.Ics.Model.Entities;
using Lucifer.Testing;
using Machine.Specifications;

namespace Lucifer.Ics.Mapping.Specs
{
    [Subject(typeof(StockItemMap))]
    public class When_checking_persistence_specs_of_stock_item_map : InMemoryDatabaseSpecs<StockItemMap>
    {
        static PersistenceSpecification<StockItem> _check;

        Because of = () =>
        {
            var spec = new PersistenceSpecification<StockItem>(Session);
            var unitType = new UnitType();
            spec.TransactionalSave(unitType);
            var unit = new Unit { UnitType = unitType };
            spec.TransactionalSave(unit);
            var item = new ProductionItem { RecipeUnit = unit };
            spec.TransactionalSave(item);

            _check = spec
                .CheckProperty(c => c.Quantity, 1.42m)
                .CheckReference(c => c.Stock, new Stock())
                .CheckReference(c => c.RecipeableItem, item)
                .CheckReference(c => c.Unit, unit);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

    }
}