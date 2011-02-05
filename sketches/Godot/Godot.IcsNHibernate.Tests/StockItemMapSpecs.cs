using System;
using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;
using NHibernate.Cfg;

namespace Godot.IcsNHibernate.Tests
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
                var item = new ProductionItem {RecipeUnit = unit};
                spec.TransactionalSave(item);

                _check = spec
                    .CheckProperty(c => c.Quantity, 1.42m)
                    .CheckReference(c => c.Stock, new Stock())
                    .CheckReference(c => c.RecipeableItem, item)
                    .CheckReference(c => c.Unit, unit);
            };

        It should_be_verified = () => _check.VerifyTheMappings();

    }

    [Subject(typeof(StockItemMap))]
    public class When_stockitem_references_nonpersisted_stock : InMemoryDatabaseSpecs<StockItemMap>
    {
        static Exception _exception;
        static StockItem _stockItem;

        Establish context = () =>
        {
            var unitType = new UnitType();
            Session.Save(unitType);
            var unit = new Unit { UnitType = unitType };
            Session.Save(unit);
            var productionItem = new ProductionItem { RecipeUnit = unit };
            Session.Save(productionItem);

            _stockItem = new StockItem { Quantity = 42m, RecipeableItem = productionItem, Unit = unit };
        };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            {
                using (var txn = Session.BeginTransaction())
                {
                    _stockItem.Stock = new Stock();
                    Session.SaveOrUpdate(_stockItem);
                    txn.Commit();
                }
            });
        };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<TransientObjectException>();
    }
}