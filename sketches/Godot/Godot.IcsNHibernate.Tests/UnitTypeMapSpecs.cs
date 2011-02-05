using System;
using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof(UnitTypeMap))]
    public class When_checking_persistence_specs_of_unittype : InMemoryDatabaseSpecs<UnitTypeMap>
    {
        static PersistenceSpecification<UnitType> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<UnitType>(Session);

                _check = spec
                    .CheckProperty(c => c.Name, "A Unit Type");
            };

        It should_be_verified = () => _check.VerifyTheMappings();
    }

    [Subject(typeof(UnitTypeMap))]
    public class When_editing_unittype_in_background : InFileDatabaseSpecs<UnitTypeMap>
    {
        static int _id;
        static string _name;
        static Exception _exception;

        Establish context = () =>
            {
                var session = SessionFactory.OpenSession();
                var unitType = new UnitType { Name = "UnitType" };
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(unitType);
                    transaction.Commit();
                }
                _id = unitType.Id;
                session.Close();
            };

        Because of = () =>
            {
                var sessionForeground = SessionFactory.OpenSession();
                var unitTypeForeground = sessionForeground.Load<UnitType>(_id);
                unitTypeForeground.Name.ShouldEqual("UnitType");

                var sessionBackground = SessionFactory.OpenSession();
                var unitTypeBackground = sessionBackground.Load<UnitType>(_id);

                using (var transaction = sessionBackground.BeginTransaction())
                {
                    unitTypeBackground.Name = "Background";
                    sessionBackground.SaveOrUpdate(unitTypeBackground);
                    transaction.Commit();
                }
                sessionBackground.Close();

                using (var transaction = sessionForeground.BeginTransaction())
                {
                    unitTypeForeground.Name = "Foreground";
                    sessionForeground.SaveOrUpdate(unitTypeForeground);
                    _exception = Catch.Exception(transaction.Commit);
                }
                sessionForeground.Close();

                using (var session = SessionFactory.OpenSession())
                {
                    var unitType = session.Load<UnitType>(_id);
                    _name = unitType.Name;
                }
            };

        It should_have_the_right_name = () => _name.ShouldEqual("Background");
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof(StaleObjectStateException));
    }
}