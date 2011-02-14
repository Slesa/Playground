using System;
using FluentNHibernate.Testing;
using FluentNHibernate.Utils;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (ProductionItemMap))]
    public class When_checking_persistence_specs_of_productionitem : InMemoryDatabaseSpecs<ProductionItemMap>
    {
        static PersistenceSpecification<ProductionItem> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<ProductionItem>(Session);
                var recipeUnitType = new UnitType();
                spec.TransactionalSave(recipeUnitType);
                var unit = new Unit {UnitType = recipeUnitType};
                spec.TransactionalSave(unit);
                var productionItem = new ProductionItem { RecipeUnit = unit };
                spec.TransactionalSave(productionItem);

                var recipeItems = new[]
                    {
                        new RecipeItem(0.42m, productionItem), 
                        new RecipeItem(0.84m, productionItem), 
                    };

                _check = spec
                    .CheckProperty(c => c.Name, "Production Item")
                    .CheckReference(c => c.RecipeUnit, unit)
                    .CheckList(c=>c.RecipeItems, recipeItems, (rec, items) => items.Each(rec.AddRecipeItem));
            };

        It should_be_verified = () => _check.VerifyTheMappings();
    }

    [Subject(typeof (ProductionItemMap))]
    public class When_editing_productionitem_in_background : InFileDatabaseSpecs<ProductionItemMap>
    {
        static int _id;
        static string _name;
        static Exception _exception;

        Establish context = () =>
            {
                var session = SessionFactory.OpenSession();
                var recipeUnitType = new UnitType();
                var recipeUnit = new Unit {UnitType = recipeUnitType};
                var productionItem = new ProductionItem {Name = "ProductionItem", RecipeUnit = recipeUnit};
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(recipeUnitType);
                    session.Save(recipeUnit);
                    session.Save(productionItem);
                    transaction.Commit();
                }
                _id = productionItem.Id;
                session.Close();
            };

        Because of = () =>
            {
                var sessionForeground = SessionFactory.OpenSession();
                var productionItemForeground = sessionForeground.Load<ProductionItem>(_id);
                productionItemForeground.Name.ShouldEqual("ProductionItem");

                var sessionBackground = SessionFactory.OpenSession();
                var productionItemBackground = sessionBackground.Load<ProductionItem>(_id);

                using (var transaction = sessionBackground.BeginTransaction())
                {
                    productionItemBackground.Name = "Background";
                    sessionBackground.SaveOrUpdate(productionItemBackground);
                    transaction.Commit();
                }
                sessionBackground.Close();

                using (var transaction = sessionForeground.BeginTransaction())
                {
                    productionItemForeground.Name = "Foreground";
                    sessionForeground.SaveOrUpdate(productionItemForeground);
                    _exception = Catch.Exception(transaction.Commit);
                }
                sessionForeground.Close();

                using (var session = SessionFactory.OpenSession())
                {
                    var productionItem = session.Load<ProductionItem>(_id);
                    _name = productionItem.Name;
                }
            };

        It should_have_the_right_name = () => _name.ShouldEqual("Background");
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof (StaleObjectStateException));
    }

    [Subject(typeof (ProductionItemMap))]
    public class When_productionitem_references_nonpersisted_recipeunit : InMemoryDatabaseSpecs<ProductionItemMap>
    {
        static Exception _exception;
        static ProductionItem _productionItem;
        static UnitType _unitType;

        Establish context = () =>
            {
                _productionItem = new ProductionItem {Name = "ProductionItem"};
                _unitType = new UnitType();
                using (var txn = Session.BeginTransaction())
                {
                    Session.Save(_unitType);
                    txn.Commit();
                }
            };

        Because of = () =>
            {
                _exception = Catch.Exception(() =>
                    {
                        using (var txn = Session.BeginTransaction())
                        {
                            _productionItem.RecipeUnit = new Unit {UnitType = _unitType};
                            Session.SaveOrUpdate(_productionItem);
                            txn.Commit();
                        }
                    });
            };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<PropertyValueException>(); //TransientObjectException>();
    }
}