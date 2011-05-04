using System.ComponentModel;
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
                _parent_Error = errorInfo["Parent"];
            };

        It should_provide_parent_error = () => _parent_Error.ShouldEqual(Strings.UnitModel_InvalidParentType);

        static string _parent_Error; 
    }

    [Subject(typeof(UnitModel))]
    public class When_creating_valid_unit : WithSubject<UnitModel>
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
}