using System;
using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (PurchaseItemMap))]
    public class When_checking_persistence_specs_of_purchaseitem : InMemoryDatabaseSpecs<PurchaseItemMap>
    {
        static PersistenceSpecification<PurchaseItem> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<PurchaseItem>(Session);
                var unitType = new UnitType();
                spec.TransactionalSave(unitType);
                var recipeUnit = new Unit {UnitType = unitType};
                spec.TransactionalSave(recipeUnit);
                var purchaseUnit = new Unit {UnitType = unitType};
                spec.TransactionalSave(purchaseUnit);
                var family = new PurchaseFamily();
                spec.TransactionalSave(family);

                _check = spec
                    .CheckProperty(c => c.Name, "Purchase Item")
                    .CheckProperty(c => c.RecipeUnit, recipeUnit)
                    .CheckReference(c => c.PurchaseUnit, purchaseUnit)
                    .CheckProperty(c => c.PurchaseFamily, family);
            };

        It should_be_verified = () => _check.VerifyTheMappings();
    }

    [Subject(typeof (PurchaseItemMap))]
    public class When_editing_purchaseitem_in_background : InFileDatabaseSpecs<PurchaseItemMap>
    {
        static int _id;
        static string _name;
        static Exception _exception;

        Establish context = () =>
            {
                var session = SessionFactory.OpenSession();
                var unitType = new UnitType();
                var recipeUnit = new Unit {UnitType = unitType};
                var purchaseItem = new PurchaseItem {Name = "PurchaseItem", RecipeUnit = recipeUnit};
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(unitType);
                    session.Save(recipeUnit);
                    session.Save(purchaseItem);
                    transaction.Commit();
                }
                _id = purchaseItem.Id;
                session.Close();
            };

        Because of = () =>
            {
                var sessionForeground = SessionFactory.OpenSession();
                var purchaseItemForeground = sessionForeground.Load<PurchaseItem>(_id);
                purchaseItemForeground.Name.ShouldEqual("PurchaseItem");

                var sessionBackground = SessionFactory.OpenSession();
                var purchaseItemBackground = sessionBackground.Load<PurchaseItem>(_id);

                using (var transaction = sessionBackground.BeginTransaction())
                {
                    purchaseItemBackground.Name = "Background";
                    sessionBackground.SaveOrUpdate(purchaseItemBackground);
                    transaction.Commit();
                }
                sessionBackground.Close();

                using (var transaction = sessionForeground.BeginTransaction())
                {
                    purchaseItemForeground.Name = "Foreground";
                    sessionForeground.SaveOrUpdate(purchaseItemForeground);
                    _exception = Catch.Exception(transaction.Commit);
                }
                sessionForeground.Close();

                using (var session = SessionFactory.OpenSession())
                {
                    var purchaseItem = session.Load<PurchaseItem>(_id);
                    _name = purchaseItem.Name;
                }
            };

        It should_have_the_right_name = () => _name.ShouldEqual("Background");
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof (StaleObjectStateException));
    }

    [Subject(typeof (PurchaseItemMap))]
    public class When_purchaseitem_references_nonpersisted_recipeunit : InMemoryDatabaseSpecs<PurchaseItemMap>
    {
        static Exception _exception;
        static PurchaseItem _purchaseItem;
        static Unit _purchaseUnit;
        static PurchaseFamily _purchaseFamily;
        static UnitType _unitType;

        Establish context = () =>
            {
                _unitType = new UnitType();
                Session.Save(_unitType);
                _purchaseItem = new PurchaseItem {Name = "PurchaseItem"};
                _purchaseUnit = new Unit {UnitType = _unitType};
                Session.Save(_purchaseUnit);
                _purchaseFamily = new PurchaseFamily();
                Session.Save(_purchaseFamily);
            };

        Because of = () =>
            {
                _exception = Catch.Exception(() =>
                    {
                        using (var txn = Session.BeginTransaction())
                        {
                            _purchaseItem.RecipeUnit = new Unit {UnitType = _unitType};
                            _purchaseItem.PurchaseUnit = _purchaseUnit;
                            _purchaseItem.PurchaseFamily = _purchaseFamily;
                            Session.SaveOrUpdate(_purchaseItem);
                            txn.Commit();
                        }
                    });
            };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<PropertyValueException>(); //TransientObjectException>();
    }

    [Subject(typeof (PurchaseItemMap))]
    public class When_purchaseitem_references_nonpersisted_purchaseunit : InMemoryDatabaseSpecs<PurchaseItemMap>
    {
        static Exception _exception;
        static PurchaseItem _purchaseItem;
        static Unit _recipeUnit;
        static PurchaseFamily _purchaseFamily;
        static UnitType _unitType;

        Establish context = () =>
            {
                _unitType = new UnitType();
                Session.Save(_unitType);
                _recipeUnit = new Unit {UnitType = _unitType};
                Session.Save(_recipeUnit);
                _purchaseFamily = new PurchaseFamily();
                Session.Save(_purchaseFamily);
                _purchaseItem = new PurchaseItem {Name = "PurchaseItem"};
            };

        Because of = () =>
            {
                _exception = Catch.Exception(() =>
                    {
                        using (var txn = Session.BeginTransaction())
                        {
                            _purchaseItem.RecipeUnit = _recipeUnit;
                            _purchaseItem.PurchaseUnit = new Unit {UnitType = _unitType};
                            _purchaseItem.PurchaseFamily = _purchaseFamily;
                            Session.SaveOrUpdate(_purchaseItem);
                            txn.Commit();
                        }
                    });
            };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<TransientObjectException>();
    }

    [Subject(typeof (PurchaseItemMap))]
    public class When_purchaseitem_references_nonpersisted_purchasefamily : InMemoryDatabaseSpecs<PurchaseItemMap>
    {
        static Exception _exception;
        static PurchaseItem _purchaseItem;
        static Unit _recipeUnit;
        static Unit _purchaseUnit;

        Establish context = () =>
            {
                _purchaseItem = new PurchaseItem {Name = "PurchaseItem"};
                var unitType = new UnitType();
                Session.Save(unitType);
                _recipeUnit = new Unit {UnitType = unitType};
                Session.Save(_recipeUnit);
                _purchaseUnit = new Unit {UnitType = unitType};
                Session.Save(_purchaseUnit);
            };

        Because of = () =>
            {
                _exception = Catch.Exception(() =>
                    {
                        using (var txn = Session.BeginTransaction())
                        {
                            _purchaseItem.RecipeUnit = _recipeUnit;
                            _purchaseItem.PurchaseUnit = _purchaseUnit;
                            _purchaseItem.PurchaseFamily = new PurchaseFamily();
                            Session.SaveOrUpdate(_purchaseItem);
                            txn.Commit();
                        }
                    });
            };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<TransientObjectException>();
    }
}