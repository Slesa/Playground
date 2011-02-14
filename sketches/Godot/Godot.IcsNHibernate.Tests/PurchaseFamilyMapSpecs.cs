using System;
using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (PurchaseFamilyMap))]
    public class When_checking_persistence_specs_of_purchase_family : InMemoryDatabaseSpecs<PurchaseFamilyMap>
    {
        static PersistenceSpecification<PurchaseFamily> _check;
        static PersistenceSpecification<PurchaseFamily> _checkLength;

        Because of = () =>
            {
                _check = new PersistenceSpecification<PurchaseFamily>(Session)
                    .CheckProperty(c => c.Name, "Purchase Family");
                _checkLength = new PersistenceSpecification<PurchaseFamily>(Session)
                    .CheckProperty(c => c.Name, new string('x', 60));
            };

        It should_be_verified = () => _check.VerifyTheMappings();
        It should_have_correct_name_length = () => _checkLength.VerifyTheMappings();
    }

    [Subject(typeof (PurchaseFamilyMap))]
    public class When_editing_purchasefamily_in_background : InFileDatabaseSpecs<PurchaseFamilyMap>
    {
        static int _id;
        static string _name;
        static Exception _exception;

        Establish context = () =>
            {
                var session = SessionFactory.OpenSession();
                var purchaseFamily = new PurchaseFamily {Name = "PurchaseFamily"};
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(purchaseFamily);
                    transaction.Commit();
                }
                _id = purchaseFamily.Id;
                session.Close();
            };

        Because of = () =>
            {
                var sessionForeground = SessionFactory.OpenSession();
                var purchaseFamilyForeground = sessionForeground.Load<PurchaseFamily>(_id);
                purchaseFamilyForeground.Name.ShouldEqual("PurchaseFamily");

                var sessionBackground = SessionFactory.OpenSession();
                var purchaseFamilyBackground = sessionBackground.Load<PurchaseFamily>(_id);

                using (var transaction = sessionBackground.BeginTransaction())
                {
                    purchaseFamilyBackground.Name = "Background";
                    sessionBackground.SaveOrUpdate(purchaseFamilyBackground);
                    transaction.Commit();
                }
                sessionBackground.Close();

                using (var transaction = sessionForeground.BeginTransaction())
                {
                    purchaseFamilyForeground.Name = "Foreground";
                    sessionForeground.SaveOrUpdate(purchaseFamilyForeground);
                    _exception = Catch.Exception(transaction.Commit);
                }
                sessionForeground.Close();

                using (var session = SessionFactory.OpenSession())
                {
                    var purchaseFamily = session.Load<PurchaseFamily>(_id);
                    _name = purchaseFamily.Name;
                }
            };

        It should_have_the_right_name = () => _name.ShouldEqual("Background");
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof (StaleObjectStateException));
    }
}