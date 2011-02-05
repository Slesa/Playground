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
    [Subject(typeof (RecipeItemMap))]
    public class When_checking_persistence_specs_of_recipe_item : InMemoryDatabaseSpecs<RecipeItemMap>
    {
        static PersistenceSpecification<RecipeItem> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<RecipeItem>(Session);
                var unitType = new UnitType();
                spec.TransactionalSave(unitType);
                var unit = new Unit { UnitType = unitType };
                spec.TransactionalSave(unit);
                var item = new ProductionItem {RecipeUnit = unit};
                spec.TransactionalSave(item);

                _check = spec
                    .CheckProperty(c => c.Quantity, 1.42m)
                    .CheckReference(c => c.Recipe, new Recipe())
                    .CheckReference(c => c.RecipeableItem, item)
                    .CheckReference(c => c.Unit, unit);
            };

        It should_be_verified = () => _check.VerifyTheMappings();

    }


    [Subject(typeof(RecipeItemMap))]
    public class When_recipeitem_references_nonpersisted_recipe : InMemoryDatabaseSpecs<RecipeItemMap>
    {
        static Exception _exception;
        static RecipeItem _recipeItem;

        Establish context = () =>
            {
                var unitType = new UnitType();
                Session.Save(unitType);
                var unit = new Unit { UnitType = unitType };
                Session.Save(unit);
                var productionItem = new ProductionItem {RecipeUnit = unit};
                Session.Save(productionItem);

                _recipeItem = new RecipeItem {Quantity = 42m, RecipeableItem = productionItem, Unit = unit};
            };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            {
                using (var txn = Session.BeginTransaction())
                {
                    _recipeItem.Recipe = new Recipe();
                    Session.SaveOrUpdate(_recipeItem);
                    txn.Commit();
                }
            });
        };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<TransientObjectException>();
    }
}