using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof (EditUnit))]
    public class When_creating_invalid_unit
    {
        static EditUnit _unit;
        static string _nameError;
        static string _contractionError;
        static string _unitTypeError;

        Establish context = () => { _unit = EditUnit.CreateUnit(); };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _contractionError = errorInfo["Contraction"];
                _unitTypeError = errorInfo["UnitType"];
            };

        It should_not_be_valid = () => _unit.IsValid.ShouldBeFalse();

        It should_provide_contraction_error =
            () => _contractionError.ShouldEqual(Strings.Model_EditUnit_Contraction_is_missing);

        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.Model_EditUnit_Name_is_missing);

        It should_provide_unittype_error = () => _unitTypeError.ShouldEqual(Strings.Model_EditUnit_UnitType_is_missing);
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_unit_with_recursion_in_parent
    {
        static EditUnit _unit;
        static string _parentError;
        static Unit _parent;

        Establish context = () =>
            {
                _parent = new Unit();
                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.Parent = _parent;
                _parent.Parent = _unit.Unit;
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _parentError = errorInfo["Parent"];
            };

        It should_not_be_valid = () => _unit.IsValid.ShouldBeFalse();

        It should_provide_parent_error =
            () => _parentError.ShouldEqual(Strings.Model_EditUnit_Parent_recursion_detected);
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_unit_with_missing_parent_factor
    {
        static EditUnit _unit;
        static string _factorError;
        static string _parentError;

        Establish context = () =>
            {
                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.Parent = new Unit();
                _unit.FactorToParent = ""; // wird zwar im ctor auf "0" gesetzt, aber just 2 be sure
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _factorError = errorInfo["FactorToParent"];
                _parentError = errorInfo["Parent"];
            };

        It should_not_be_valid = () => _unit.IsValid.ShouldBeFalse();
        It should_not_provide_parent_error = () => _parentError.ShouldBeEmpty();
        It should_provide_factor_error = () => _factorError.ShouldEqual(Strings.Model_EditUnit_FactorToParent_is_missing);
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_unit_with_invalid_parent_factor
    {
        static EditUnit _unit;
        static string _factorError;
        static string _parentError;

        Establish context = () =>
            {
                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.Parent = new Unit();
                _unit.FactorToParent = "0"; // wird im ctor auf "0" gesetzt
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _factorError = errorInfo["FactorToParent"];
                _parentError = errorInfo["Parent"];
            };

        It should_not_be_valid = () => _unit.IsValid.ShouldBeFalse();
        It should_not_provide_parent_error = () => _parentError.ShouldBeEmpty();
        It should_provide_factor_error = () => _factorError.ShouldEqual(Strings.Model_EditUnit_FactorToParent_is_invalid);
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_valid_unit
    {
        static EditUnit _unit;
        static string _nameError;
        static string _parentError;
        static string _contractionError;
        static string _factorError;

        Establish context = () =>
            {
                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.UnitType = new UnitType();
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _contractionError = errorInfo["Contraction"];
                _unitTypeError = errorInfo["UnitType"];
                _parentError = errorInfo["Parent"];
                _factorError = errorInfo["FactorToParent"];
            };

        It should_be_valid = () => _unit.IsValid.ShouldBeTrue();
        It should_not_provide_name_error = () => _nameError.ShouldBeEmpty();
        It should_not_provide_contraction_error = () => _contractionError.ShouldBeEmpty();
        It should_not_provide_unittype_error = () => _unitTypeError.ShouldBeEmpty();
        It should_not_provide_parent_error = () => _parentError.ShouldBeEmpty();
        It should_not_provide_factor_error = () => _factorError.ShouldBeEmpty();

        static string _unitTypeError;
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_unit_with_parent_of_other_unittype
    {
        static EditUnit _unit;
        static string _parentError;
        static string _factorError;

        Establish context = () =>
            {
                var parent = EditUnit.CreateUnit();
                parent.UnitType = SetId(new UnitType(), 1);

                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.UnitType = SetId(new UnitType(), 2);
                _unit.Parent = parent.Unit;
                _unit.FactorToParent = "1.0";
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _unitTypeError = errorInfo["UnitType"];
                _parentError = errorInfo["Parent"];
                _factorError = errorInfo["FactorToParent"];
            };

        It should_be_invalid = () => _unit.IsValid.ShouldBeFalse();
        It should_provide_unittype_error = () => _unitTypeError.ShouldEqual(Strings.Model_EditUnit_InvalidParentType);
        It should_provide_parent_error = () => _parentError.ShouldEqual(Strings.Model_EditUnit_InvalidParentType);
        It should_not_provide_factor_error = () => _factorError.ShouldBeEmpty();

        private static UnitType SetId(UnitType unitType, int id)
        {
            var field = typeof(UnitType).GetProperty("Id"); //, BindingFlags.Instance | BindingFlags.SetProperty);
            if (field != null)
                field.SetValue(unitType, id, null);
            return unitType;
        }
        static string _unitTypeError;
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_unit_with_parent_with_unittype_null
    {
        static EditUnit _unit;
        static string _parentError;
        static string _factorError;

        Establish context = () =>
            {
                var parent = EditUnit.CreateUnit();

                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.UnitType = new UnitType();
                _unit.Parent = parent.Unit;
                _unit.FactorToParent = "1.0";
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _unitTypeError = errorInfo["UnitType"];
                _parentError = errorInfo["Parent"];
                _factorError = errorInfo["FactorToParent"];
            };

        It should_be_invalid = () => _unit.IsValid.ShouldBeFalse();
        It should_provide_unittype_error = () => _unitTypeError.ShouldEqual(Strings.Model_EditUnit_InvalidParentType);
        It should_provide_parent_error = () => _parentError.ShouldEqual(Strings.Model_EditUnit_InvalidParentType);
        It should_not_provide_factor_error = () => _factorError.ShouldBeEmpty();

        static string _unitTypeError;
    }

    [Subject(typeof (EditUnit))]
    public class When_creating_valid_unit_with_parent
    {
        static EditUnit _unit;
        static string _parentError;
        static string _factorError;

        Establish context = () =>
            {
                var unitType = new UnitType();
                var parent = EditUnit.CreateUnit();
                parent.UnitType = unitType;

                _unit = EditUnit.CreateUnit();
                _unit.Name = "Unit";
                _unit.Contraction = "Contraction";
                _unit.UnitType = unitType;
                _unit.Parent = parent.Unit;
                _unit.FactorToParent = "1.0";
            };

        Because of = () =>
            {
                var errorInfo = _unit as IDataErrorInfo;
                _unitTypeError = errorInfo["UnitType"];
                _parentError = errorInfo["Parent"];
                _factorError = errorInfo["FactorToParent"];
            };

        It should_be_valid = () => _unit.IsValid.ShouldBeTrue();
        It should_not_provide_unittype_error = () => _unitTypeError.ShouldBeEmpty();
        It should_not_provide_parent_error = () => _parentError.ShouldBeEmpty();
        It should_not_provide_factor_error = () => _factorError.ShouldBeEmpty();

        static string _unitTypeError;
    }

}