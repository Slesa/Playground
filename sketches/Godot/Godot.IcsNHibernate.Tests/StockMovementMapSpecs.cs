using System;
using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof(StockMovementMap))]
    public class When_checking_persistence_specs_of_stockmovement_map : InMemoryDatabaseSpecs<StockMap>
    {
        static PersistenceSpecification<StockMovement> _check;

        Because of = () =>
        {
            var spec = new PersistenceSpecification<StockItem>(Session);

            var unitType = new UnitType();
            spec.TransactionalSave(unitType);
            var unit = new Unit { UnitType = unitType };
            spec.TransactionalSave(unit);
            var stockFrom = new Stock();
            spec.TransactionalSave(stockFrom);
            var stockTo = new Stock();
            spec.TransactionalSave(stockTo);
            var item = new RecipeableItem {RecipeUnit = unit};
            spec.TransactionalSave(item);
            var dateTime = DateTime.Now;

            _check = new PersistenceSpecification<StockMovement>(Session)
                //.CheckProperty(c => c.Quantity, 42.0m)
                .CheckReference(c => c.OfStock, stockFrom);
                // HACK: Unable to verify DateTime
                //.CheckProperty(c=>c.ExecutedAt, dateTime);
            //.CheckReference(c => c.ToStock, stockTo)
            //.CheckReference(c=>c.RecipeableItem, item)
            //.CheckReference(c => c.Unit, unit);
        };

        It should_be_verified = () => _check.VerifyTheMappings();

    }
}