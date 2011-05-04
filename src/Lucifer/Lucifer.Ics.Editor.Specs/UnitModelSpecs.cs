using System.ComponentModel;
using System.Globalization;
using System.Threading;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(UnitModel))]
    public class When_creating_invalid_unit : WithSubject<UnitModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _contractionError = errorInfo["Contraction"];
                _unitTypeError = errorInfo["UnitType"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.UnitModel_Name_missing);
        It should_provide_contraction_error = () => _contractionError.ShouldEqual(Strings.UnitModel_Contraction_missing);
        It should_provide_unit_type_error = () => _unitTypeError.ShouldEqual(Strings.UnitModel_UnitType_missing);

        static string _nameError;
        static string _contractionError;
        static string _unitTypeError;
    }

    [Subject(typeof(UnitModel))]
    public class When_creating_unit_with_parent_of_wrong_type : WithSubject<UnitModel>
    {
        Establish context = () =>
            {
                Subject.UnitType = new UnitType();
                Subject.Parent = new Unit {UnitType = new UnitType()};
                Subject.FactorToParent = "1.0";
            };

        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _parentError = errorInfo["Parent"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_parent_error = () => _parentError.ShouldEqual(Strings.UnitModel_InvalidParentType);

        static string _parentError; 
    }

    [Subject(typeof(UnitModel))]
    public class When_creating_unit_with_parent_but_w_o_factor : WithSubject<UnitModel>
    {
        Establish context = () =>
            {
                var unitType = An<UnitType>();
                Subject.Name = "Unit";
                Subject.Contraction = "Contraction";
                Subject.UnitType = unitType;
                Subject.Parent = new Unit { UnitType = unitType };
            };

        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _factorError = errorInfo["FactorToParent"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_parent_error = () => _factorError.ShouldEqual(Strings.UnitModel_FactorToParent_invalid);

        static string _factorError; 
    }

    [Subject(typeof(UnitModel))]
    public class When_creating_unit_with_recursion_in_parent : WithSubject<UnitModel>
    {
        Establish context = () =>
            {
                var unitType = An<UnitType>();
                var grandParent = new Unit {Parent=Subject.Unit, UnitType = unitType};
                var parent = new Unit {UnitType = unitType, Parent = grandParent};
            Subject.Name = "Unit";
            Subject.Contraction = "Contraction";
                Subject.UnitType = unitType;
                Subject.Parent = parent;
            };

        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _parentError = errorInfo["Parent"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_parent_error = () => _parentError.ShouldEqual(Strings.UnitModel_Parent_recursion_detected);

        static string _parentError; 
    }

    [Subject(typeof(UnitModel))]
    public class When_creating_valid_unit_w_o_parent : WithSubject<UnitModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Unit";
                Subject.Contraction = "Contraction";
                Subject.UnitType = new UnitType();
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }

    [Subject(typeof(UnitModel))]
    public class When_creating_valid_unit_with_parent : WithSubject<UnitModel>
    {
        Establish context = () =>
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var unitType = An<UnitType>();
                Subject.Name = "Unit";
                Subject.Contraction = "Contraction";
                Subject.UnitType = unitType;
                Subject.Parent = new Unit {UnitType = unitType};
                Subject.FactorToParent = "1.0";
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}