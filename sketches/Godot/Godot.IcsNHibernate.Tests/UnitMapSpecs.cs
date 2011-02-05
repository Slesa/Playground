using System;
using System.Linq;
using FluentNHibernate.Testing;
using Godot.IcsModel.Entities;
using Godot.IcsNHibernate.Mappings;
using Godot.Tests.Core;
using Machine.Specifications;
using NHibernate;

namespace Godot.IcsNHibernate.Tests
{
    [Subject(typeof (UnitMap))]
    public class When_checking_persistence_specs_of_unit : InMemoryDatabaseSpecs<UnitMap>
    {
        static PersistenceSpecification<Unit> _check;

        Because of = () =>
            {
                var spec = new PersistenceSpecification<Unit>(Session);
                var unitType = new UnitType();
                spec.TransactionalSave(unitType);
                var parent = new Unit {UnitType = unitType};
                spec.TransactionalSave(parent);

                _check = spec
                    .CheckProperty(c => c.Name, "Kilogramm")
                    .CheckProperty(c => c.Contraction, "kg")
                    .CheckReference(c => c.UnitType, unitType)
                    .CheckReference(c => c.Parent, parent)
                    .CheckProperty(c => c.FactorToParent, 1.42m)
                    .CheckProperty(c => c.Purchasing, true)
                    .CheckProperty(c => c.Reciping, true);
            };

        It should_be_verified = () => _check.VerifyTheMappings();
    }

    [Subject(typeof (UnitMap))]
    public class When_editing_unit_in_background : InFileDatabaseSpecs<UnitMap>
    {
        static int _id;
        static string _name;
        static Exception _exception;

        Establish context = () =>
            {
                var session = SessionFactory.OpenSession();
                var unitType = new UnitType();
                var unit = new Unit {Name = "Unit", UnitType = unitType};
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(unitType);
                    session.Save(unit);
                    transaction.Commit();
                }
                _id = unit.Id;
                session.Close();
            };

        Because of = () =>
            {
                var sessionForeground = SessionFactory.OpenSession();
                var unitForeground = sessionForeground.Load<Unit>(_id);
                unitForeground.Name.ShouldEqual("Unit");

                var sessionBackground = SessionFactory.OpenSession();
                var unitBackground = sessionBackground.Load<Unit>(_id);

                using (var transaction = sessionBackground.BeginTransaction())
                {
                    unitBackground.Name = "Background";
                    sessionBackground.SaveOrUpdate(unitBackground);
                    transaction.Commit();
                }
                sessionBackground.Close();

                using (var transaction = sessionForeground.BeginTransaction())
                {
                    unitForeground.Name = "Foreground";
                    sessionForeground.SaveOrUpdate(unitForeground);
                    _exception = Catch.Exception(transaction.Commit);
                }
                sessionForeground.Close();

                using (var session = SessionFactory.OpenSession())
                {
                    var unit = session.Load<Unit>(_id);
                    _name = unit.Name;
                }
            };

        It should_have_the_right_name = () => _name.ShouldEqual("Background");
        It should_not_save_the_foreground_values = () => _exception.ShouldBeOfType(typeof (StaleObjectStateException));
    }

    [Subject(typeof (UnitMap))]
    public class When_unit_references_nonpersisted_unittype : InMemoryDatabaseSpecs<UnitMap>
    {
        static Exception _exception;
        static Unit _unit;
        Establish context = () => { _unit = new Unit {Name = "Unit", Contraction = "u"}; };

        Because of = () =>
            {
                _exception = Catch.Exception(() =>
                    {
                        using (var txn = Session.BeginTransaction())
                        {
                            _unit.UnitType = new UnitType();
                            Session.SaveOrUpdate(_unit);
                            txn.Commit();
                        }
                    });
            };

        It should_fail = () => _exception.ShouldNotBeNull();

        It should_fail_because_of_referencing_a_transient_object =
            () => _exception.ShouldBeOfType<PropertyValueException>(); //<TransientObjectException>();
    }

    [Subject(typeof (UnitMap))]
    public class When_setting_parent_of_unit : InMemoryDatabaseSpecs<UnitMap>
    {
        static Unit _unit;
        static Unit _parent;

        Establish context = () =>
            {
                _parent = new Unit {Name = "Parent", Contraction = "p"};
                _unit = new Unit {Name = "Unit", Contraction = "u"};
            };

        Because of = () =>
            {
                _unit.Parent = _parent;
            };

        It should_be_child_of_parent = () => _parent.Children.ShouldContain(_unit);
    }

    [Subject(typeof (UnitMap))]
    public class When_changing_parent_of_unit : InMemoryDatabaseSpecs<UnitMap>
    {
        static Unit _unit;
        static Unit _oldParent;
        static Unit _newParent;
        static bool _wasChildOfOldParent;

        Establish context = () =>
            {
                _oldParent = new Unit {Name = "Old parent", Contraction = "op"};
                _newParent = new Unit {Name = "New parent", Contraction = "np"};
                _unit = new Unit {Name = "Unit", Contraction = "u", Parent = _oldParent};
            };

        Because of = () =>
            {
                var found = _oldParent.Children.FirstOrDefault(x=>x==_unit);
                _wasChildOfOldParent = found!=null;
                _unit.Parent = _newParent;
            };

        It should_have_been_child_of_old_parent = () => _wasChildOfOldParent.ShouldBeTrue();
        It should_not_be_child_of_old_parent = () => _oldParent.Children.ShouldNotContain(_unit);
        It should_be_child_of_new_parent = () => _newParent.Children.ShouldContain(_unit);
    }
}